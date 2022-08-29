using Frontend.Business.DetallesInventario;
using Frontend.Business.Inventarios;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.Areas.Inventarios.Models;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Inventarios.ViewModels
{
    public class AprobacionDetalleInventarioProvisorioViewModel : BaseViewModel, IAprobacionDetalleInventarioProvisorioViewModel
    {
        private readonly IDisplayAlertService displayAlertService;
        private readonly INavigationService navigationService;
        private readonly IInventarioService inventarioService;
        private readonly IDetalleInventarioService detalleInventarioService;
        private readonly IInventarioLocalService inventarioLocalService;

        public ICommand GoToSearchMaterialCommand { get; set; }
        public ICommand FiltroPosicionCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteMaterialCommnad { get; set; }
        public ICommand FinalizarInventarioCommand { get; set; }
        public ICommand GoToInformacionInventarioCommand { get; set; }

        public ICommand AprobarCommand { get; set; }
        public ICommand RechazarCommand { get; set; }
        public ICommand ComentarioCommand { get; set; }
        public ICommand GetInventarioProvisorioCommand { get; set; }


        private AprobacionDetalleInventarioProvisorioModel detalleInventarioSelected;
        public AprobacionDetalleInventarioProvisorioModel DetalleInventarioSelected
        {
            get { return detalleInventarioSelected; }
            set
            {
                SetProperty(ref detalleInventarioSelected, value);
                GoToDetalleInventario(detalleInventarioSelected);
            }
        }

        public ObservableRangeCollection<AprobacionDetalleInventarioProvisorioModel> ListaDetallesDeInventario { get; private set; }
        public Inventario inventario { get; private set; }
        private IList<DetalleInventario> posicionesViewList;

        public AprobacionDetalleInventarioProvisorioViewModel(IDisplayAlertService displayAlertService, INavigationService navigationService,
            IInventarioService inventarioService, IInventarioLocalService inventarioLocalService, IDetalleInventarioService detalleInventarioService)
        {
            this.displayAlertService = displayAlertService;
            this.navigationService = navigationService;
            this.inventarioService = inventarioService;
            this.inventarioLocalService = inventarioLocalService;
            this.detalleInventarioService = detalleInventarioService;
            Init();
        }

        private void Init()
        {
            posicionesViewList = new List<DetalleInventario>();
            GoToSearchMaterialCommand = new Command(GoToSearchMaterial);
            DeleteMaterialCommnad = new Command<DetalleInventario>(async (detalleInventario) => await DeleteMaterial(detalleInventario));
            FinalizarInventarioCommand = new Command(async () => await FinalizarInventario());
            GoToInformacionInventarioCommand = new Command(GoToInformacionInventario);
            ComentarioCommand = new Command(Comentario);
            AprobarCommand = new Command(async () => await Aprobar());
            RechazarCommand = new Command(async () => await Rechazar());
            RefreshCommand = new Command(RefreshDetallesInventario);
            FiltroPosicionCommand = new Command<string>(Filtro);
            GetInventarioProvisorioCommand = new Command(GetInventarioProvisorio);
            inventario = navigationService.GetNavigationParams<AprobacionDetalleInventarioProvisorioView>() as Inventario;

            Title = "Inventario " + inventario.Codigo;
            ListaDetallesDeInventario = new ObservableRangeCollection<AprobacionDetalleInventarioProvisorioModel>();
            var init = InitListaDetalleInventario();
        }

        private async void GetInventarioProvisorio(object obj)
        {
            inventario = await inventarioService.GetInventarioById(inventario.Id);
        }

        private async Task InitListaDetalleInventario()
        {
            posicionesViewList = await detalleInventarioService.GetByIdInventario(inventario.Id);
            ListaDetallesDeInventario.AddRange(posicionesViewList.OrderBy(x => x.Posicion).Select(x => new AprobacionDetalleInventarioProvisorioModel()
            {
                DetalleInventario = x,
                IsSelected = false
            }));
        }

        private void RefreshDetallesInventario()
        {
            ListaDetallesDeInventario.Clear();
            ListaDetallesDeInventario.AddRange(posicionesViewList.Select(x => new AprobacionDetalleInventarioProvisorioModel()
            {
                DetalleInventario = x,
                IsSelected = false
            }));
        }


        private void Filtro(string filtro)
        {
            ListaDetallesDeInventario.Clear();
            ListaDetallesDeInventario.AddRange(posicionesViewList
                .Where(x => String.IsNullOrEmpty(filtro) || x.Posicion.ToString().Contains(filtro))
                .Select(x => new AprobacionDetalleInventarioProvisorioModel()
                {
                    DetalleInventario = x,
                    IsSelected = false
                }));
        }

        private async Task Aprobar()
        {
            IsBusy = true;
            var answer = await displayAlertService.Show("Aprobar inventario", "Se aprobará las posiciones seleccionadas, y rechazará las restantes. ¿Desea continuar?", "Continuar", "Cancelar");
            if (answer)
            {
                if (ListaDetallesDeInventario.All(x => x.IsSelected))
                {
                    await inventarioService.SetToAprobado(inventario);
                }
                else
                {
                    var noSeleccionados = ListaDetallesDeInventario.Where(x => !x.IsSelected).Select(x => x.DetalleInventario).ToList();
                    if (!ValidateComentarios(noSeleccionados))
                    {
                        Toast.ShowMessage("El inventario o las posiciones a rechazar deben tener un comentario de rechazo");
                        IsBusy = false;
                        return;
                    }
                    var listaAprobados = new List<DetalleInventario>(inventario.DetallesInventario.Where(x => ListaDetallesDeInventario.Where(z => z.IsSelected).Select(d => d.DetalleInventario.Id).Contains(x.Id)).ToList());
                    var listaDesaprobados = new List<DetalleInventario>(inventario.DetallesInventario.Where(x => ListaDetallesDeInventario.Where(z => !z.IsSelected).Select(d => d.DetalleInventario.Id).Contains(x.Id)).ToList());

                    await RechazoParcial(listaAprobados, listaDesaprobados);
                }

                #region ASOSA flagSync Aprobación inventarios (Provisorios y SAP)l
                Frontend.Core.Commons.Globals.flagSync = "AprobacionDetalleInventarioProvisorioViewModel";
                #endregion
                Toast.ShowMessage("Posiciones aprobadas");
                navigationService.PushFromAsync<HomeView, AprobacionInventarioView>();
            }

            IsBusy = false;
        }

        private void Comentario()
        {
            List<Inventario> listaInventario = new List<Inventario>();
            listaInventario.Add(inventario);
            AgregarComentarioModel comentarioModel = new AgregarComentarioModel()
            {
                EsGenerico = false,
                Inventarios = listaInventario,
                Retornar = true
            };
            navigationService.PushAsync<AprobacionDetalleInventarioProvisorioView, AgregarComentarioView>(comentarioModel);
        }

        private async Task Rechazar()
        {
            IsBusy = true;
            var answer = await displayAlertService.Show("Rechazar inventario", "Se rechazará las posiciones seleccionadas, y aprobará las restantes. ¿Desea continuar?", "Continuar", "Cancelar");
            if (answer)
            {
                var seleccionados = ListaDetallesDeInventario.Where(x => x.IsSelected).Select(x => x.DetalleInventario).ToList();
                if (!ValidateComentarios(seleccionados))
                {
                    Toast.ShowMessage("El inventario o todas las posiciones deben tener un comentario de rechazo");
                    IsBusy = false;
                    return;
                }
                if (ListaDetallesDeInventario.All(x => x.IsSelected)
                    || ListaDetallesDeInventario.All(x => !x.IsSelected))
                {
                    await RechazoCompleto();
                }
                else
                {
                    var listaAprobados = new List<DetalleInventario>(inventario.DetallesInventario.Where(x => ListaDetallesDeInventario.Where(z => !z.IsSelected).Select(d => d.DetalleInventario.Id).Contains(x.Id)).ToList());
                    var listaDesaprobados = new List<DetalleInventario>(inventario.DetallesInventario.Where(x => ListaDetallesDeInventario.Where(z => z.IsSelected).Select(d => d.DetalleInventario.Id).Contains(x.Id)).ToList());

                    await RechazoParcial(listaAprobados, listaDesaprobados);
                }

                Toast.ShowMessage("Posiciones rechazadas");
                navigationService.PushFromAsync<HomeView, AprobacionInventarioView>();
            }
            IsBusy = false;
        }

        private bool ValidateComentarios(List<DetalleInventario> seleccionados)
        {
            var inventarioComentado = !string.IsNullOrEmpty(inventario.ComentarioRechazo);
            var detallesComentados = seleccionados.All(x => !String.IsNullOrEmpty(x.Comentario));
            return inventarioComentado || detallesComentados;
        }

        private async Task RechazoParcial(List<DetalleInventario> listaAprobados, List<DetalleInventario> listaDesaprobados)
        {
            inventario.DetallesInventario = listaDesaprobados;
            await inventarioLocalService.SetToRechazadoParcial(inventario, inventario.ComentarioRechazo);

            inventario.DetallesInventario = listaAprobados;
            await inventarioService.SetToAprobadoParcial(inventario);
        }

        private async Task RechazoCompleto()
        {
            await inventarioService.SetToRechazado(inventario);
        }

        private void GoToInformacionInventario()
        {
            navigationService.PushAsync<AprobacionDetalleInventarioProvisorioView, VisualizarInformacionInventarioView>(inventario);
        }

        private async Task FinalizarInventario()
        {
            var answer = await displayAlertService.Show("Finalizar inventario", "¿Desea finalizar el inventario?", "Aceptar", "Cancelar");
            if (answer)
            {
                if (inventarioService.IsValidToFinish(inventario))
                {
                    await inventarioService.Delete(inventario);
                    navigationService.PushNextMasterDetailPage<HomeView, HomeView>();
                }
                else
                {
                    Toast.ShowMessage("El inventario no se puede finalizar. Complete los datos de cabecera");
                    navigationService.PushAsync<AprobacionDetalleInventarioProvisorioView, InformacionInventarioView>(inventario);
                }
            }
        }

        private async Task DeleteMaterial(DetalleInventario detalleInventario)
        {
            var answer = await displayAlertService.Show("Eliminar material", "¿Desea eliminar el material del inventario?", "Aceptar", "Cancelar");
            if (answer)
            {
                inventario.DetallesInventario.Remove(detalleInventario);
                posicionesViewList.Remove(detalleInventario);
                RefreshDetallesInventario();
                await inventarioService.UpdateWithChildren(inventario);

                if (inventario.DetallesInventario.Count == 0)
                {
                    await inventarioService.Delete(inventario);
                    navigationService.PushFromAsync<HomeView, SearchMaterialView>(inventario);
                }
            }
        }

        private void GoToSearchMaterial()
        {
            navigationService.PushAsync<AprobacionDetalleInventarioProvisorioView, SearchMaterialView>(inventario);
        }

        private void GoToDetalleInventario(AprobacionDetalleInventarioProvisorioModel aprobacionDetalleInventarioProvisorioModel)
        {
            if (aprobacionDetalleInventarioProvisorioModel != null)
            {
                var detalle = new DetalleInventarioModel()
                {
                    DetalleInventario = aprobacionDetalleInventarioProvisorioModel.DetalleInventario,
                    ShowComentario = true,
                    ShowCantidad = true
                };
                navigationService.PushAsync<AprobacionDetalleInventarioProvisorioView, VisualizarDetalleMaterialView>(detalle);
            }
        }

    }
}
