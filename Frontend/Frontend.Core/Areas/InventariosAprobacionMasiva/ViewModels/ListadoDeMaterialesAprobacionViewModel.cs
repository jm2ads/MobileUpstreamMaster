using Frontend.Business.DetallesInventario;
using Frontend.Business.Inventarios;
using Frontend.Commons.Commons.Errors;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Inventarios.Models;
using Frontend.Core.Areas.InventariosAprobacionMasiva.IViewModels;
using Frontend.Core.Areas.InventariosAprobacionMasiva.Models;
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
using AgregarComentarioModel = Frontend.Core.Areas.InventariosAprobacionMasiva.Models.AgregarComentarioModel;

namespace Frontend.Core.Areas.InventariosAprobacionMasiva.ViewModels
{
    public class ListadoDeMaterialesAprobacionViewModel : BaseViewModel, IListadoDeMaterialesAprobacionViewModel
    {
        public ICommand AprobarCommand { get; set; }
        public ICommand FiltroMaterialCommand { get; set; }
        public ICommand RechazarCommand { get; set; }
        public ICommand ComentarioCommand { get; set; }

        private readonly INavigationService navigationService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly IInventarioService inventarioService;
        private readonly IInventarioLocalService inventarioLocalService;

        public Task Refresh { get; set; }
        private bool _isRefreshing = false;
        public bool IsRefreshingListado
        {
            get { return _isRefreshing; }
            set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }

        private bool _hasInventario = true;
        public bool HasInventario
        {
            get { return _hasInventario; }
            set
            {
                SetProperty(ref _hasInventario, value);
            }
        }
        public ObservableRangeCollection<AprobacionMasivaDetalleModel> ListaDetallesInventarios { get; set; }
        public List<AprobacionMasivaInventarioModel> ListaInventarios { get; set; }
        private List<AprobacionMasivaDetalleModel> detallesList;

        private AprobacionMasivaDetalleModel detalleInventarioSelected;
        public AprobacionMasivaDetalleModel DetalleInventarioSelected
        {
            get { return detalleInventarioSelected; }
            set
            {
                SetProperty(ref detalleInventarioSelected, value);
                GoToDetalleInventario(detalleInventarioSelected);
            }
        }

        public ListadoDeMaterialesAprobacionViewModel(INavigationService navigationService, IDisplayAlertService displayAlertService, IInventarioService inventarioService, IInventarioLocalService inventarioLocalService)

        {
            this.navigationService = navigationService;
            this.displayAlertService = displayAlertService;
            this.inventarioService = inventarioService;
            this.inventarioLocalService = inventarioLocalService;
            Init();
        }

        private async void Init()
        {
            Title = "Aprobación masiva";
            ListaDetallesInventarios = new ObservableRangeCollection<AprobacionMasivaDetalleModel>();
            ListaInventarios = new List<AprobacionMasivaInventarioModel>();
            FiltroMaterialCommand = new Command<string>(FiltroMaterial);
            detallesList = new List<AprobacionMasivaDetalleModel>();
            AprobarCommand = new Command(async () => await Aprobar());
            RechazarCommand = new Command(async () => await Rechazar());
            ComentarioCommand = new Command(async () => await Comentar());
            Refresh = FillListaDetallesInventario();
        }

        private async Task Comentar()
        {
            var inventarios = GetSelected();
            if (inventarios.Count > 0)
            {
                navigationService.PushAsync<ListadoDeMaterialesAprobacionView, AgregarComentarioAprobacionMasivaView>(inventarios);
            }
            else
            {
                Toast.ShowMessage("Debe seleccionar al menos una posición");
                return;
            }
        }

        public async Task FillListaDetallesInventario()
        {
            if (IsRefreshingListado)
            {
                return;
            }
            this.IsRefreshingListado = true;
            var delay = Task.Delay(2000);
            ListaDetallesInventarios.Clear();
            var inventarios = (await inventarioService.GetAllPendienteAprobacionWithChildren()).Where(x => x.EsProvisorio);
            foreach (var inventario in inventarios)
            {
                var inventarioModel = new AprobacionMasivaInventarioModel(inventario);
                ListaInventarios.Add(inventarioModel);
                ListaDetallesInventarios.AddRange(inventarioModel.Detalles);
            }
            detallesList.AddRange(ListaDetallesInventarios);
            HasInventario = ListaDetallesInventarios.Count > 0;
            await delay;
            this.IsRefreshingListado = false;
        }
        private async Task Aprobar()
        {
            IsBusy = true;
            var inventarios = GetSelected();
            if (inventarios.Count <= 0)
            {
                Toast.ShowMessage("Debe seleccionar al menos una posición");
                IsBusy = false;
                return;
            }
            var answer = await displayAlertService.Show("Aprobar inventarios", "Se aprobarán las posiciones seleccionadas y rechazarán las restantes de los inventarios correspondientes ¿Desea continuar?", "Aceptar", "Cancelar");
            if (answer)
            {
                var listAprobacionParcial = new List<int>();
                foreach (var inventario in inventarios)
                {
                    if (AllSelected(inventario))
                    {
                        try
                        {
                            await inventarioService.SetToAprobado(inventario);
                        }
                        catch (BusinessException be)
                        {
                            Toast.ShowMessage("Los inventarios seleccionados no se pueden aprobar.");
                            return;
                        }
                    }
                    else
                    {
                        if (ValidateTodosRechazadosConComentario(inventario))
                        {
                            var listaAprobados = ListaDetallesInventarios.Where(x => x.IsSelected && x.DetalleInventario.InventarioId == inventario.Id).Select(x => x.DetalleInventario).ToList();
                            var listaDesaprobados = ListaDetallesInventarios.Where(x => !x.IsSelected && x.DetalleInventario.InventarioId == inventario.Id).Select(x => x.DetalleInventario).ToList();
                            await RechazoParcial(inventario, listaAprobados, listaDesaprobados);
                        }
                        else
                        {
                            listAprobacionParcial.Add(inventario.Id);
                        }
                    }
                }

                if (listAprobacionParcial.Count != 0)
                {
                    var respuestaComentario = await displayAlertService.Show("Aprobar inventarios", "Se aprobarán/rechazarán las posiciones con comentarios pero no a las que no tengan, ¿Desea agregar los comentarios faltantes de forma genérica?", "Aceptar", "Cancelar");
                    if (respuestaComentario)
                    {
                        navigationService.PushAsync<ListadoDeMaterialesAprobacionView, AgregarComentarioAprobacionMasivaView>(
                            new AgregarComentarioModel()
                            {
                                EsAprobacion = true,
                                ListaDetalles = ListaDetallesInventarios.Where(x => listAprobacionParcial.Contains(x.DetalleInventario.InventarioId)).ToList()
                            });

                        IsBusy = false;
                        return;
                    }
                }

                Toast.ShowMessage("Las posiciones con comentario han sido aprobadas y/o rechazadas.");
                navigationService.PushFromAsync<HomeView, ListadoDeMaterialesAprobacionView>();
            }

            IsBusy = false;
        }

        private async Task Rechazar()
        {
            IsBusy = true;
            var inventarios = GetSelected();
            if (inventarios.Count <= 0)
            {
                Toast.ShowMessage("Debe seleccionar al menos una posición");
                IsBusy = false;
                return;
            }
            var answer = await displayAlertService.Show("Rechazar inventarios", "Se rechazarán las posiciones seleccionadas y aprobarán las restantes de los inventarios correspondientes ¿Desea continuar?", "Aceptar", "Cancelar");
            if (answer)
            {
                if (!ValidateTodosRechazadosConComentario(inventarios))
                {
                    var respuesta = await displayAlertService.Show("Agregar comentario genérico", "¿Desea agregar un comentario genérico para el/los inventario/s?", "Aceptar", "Cancelar");
                    if (respuesta)
                    {
                        navigationService.PushAsync<ListadoDeMaterialesAprobacionView, AgregarComentarioAprobacionMasivaView>(new AgregarComentarioModel() { EsAprobacion = false, ListaDetalles = ListaDetallesInventarios });
                    }
                    else
                    {
                        Toast.ShowMessage("El inventario o las posiciones a rechazar deben tener un comentario de rechazo");
                        IsBusy = false;
                        return;
                    }
                }
                else
                {
                    foreach (var inventario in inventarios)
                    {
                        if (AllSelected(inventario))
                        {
                            try
                            {
                                await inventarioService.SetToRechazado(inventario);
                            }
                            catch (BusinessException be)
                            {
                                Toast.ShowMessage("Los inventarios seleccionados no se pueden rechazar.");
                                IsBusy = false;
                                return;
                            }
                        }
                        else
                        {
                            var listaAprobados = new List<DetalleInventario>(ListaDetallesInventarios.Where(x => !x.IsSelected && x.DetalleInventario.InventarioId == inventario.Id).Select(d => d.DetalleInventario)).ToList();
                            var listaDesaprobados = new List<DetalleInventario>(ListaDetallesInventarios.Where(x => x.IsSelected && x.DetalleInventario.InventarioId == inventario.Id).Select(d => d.DetalleInventario)).ToList();
                            await RechazoParcial(inventario, listaAprobados, listaDesaprobados);
                        }
                    }

                    Toast.ShowMessage("Las posiciones con comentario han sido rechazadas y/o aprobadas.");
                    navigationService.PushFromAsync<HomeView, ListadoDeMaterialesAprobacionView>();
                }
            }
            IsBusy = false;
        }

        private bool ValidateTodosRechazadosConComentario(IList<Inventario> inventarios)
        {
            var todosComentados = inventarios.All(x => !String.IsNullOrWhiteSpace(x.ComentarioRechazo));
            var todosDetallesComentados = inventarios.All(x => (ListaDetallesInventarios
                                                     .Where(di => di.IsSelected && di.DetalleInventario.InventarioId == x.Id))
                                                     .All(di => !String.IsNullOrWhiteSpace(di.DetalleInventario.Comentario)));
            return todosComentados || todosDetallesComentados;
        }

        private bool ValidateTodosRechazadosConComentario(Inventario inventario)
        {
            var esComentado = !string.IsNullOrWhiteSpace(inventario.ComentarioRechazo); ;
            var todosDetallesComentados = (ListaDetallesInventarios.Where(x => !x.IsSelected && x.DetalleInventario.InventarioId == inventario.Id)).All(di => !String.IsNullOrWhiteSpace(di.DetalleInventario.Comentario));
            return esComentado || todosDetallesComentados;
        }

        private IList<Inventario> GetSelected()
        {
            return ListaDetallesInventarios.Where(x => x.IsSelected).Select(x => x.DetalleInventario.Inventario).Distinct().ToList();
        }

        private bool AllSelected(Inventario inventario)
        {
            return ListaDetallesInventarios.Where(x => x.IsSelected && x.DetalleInventario.InventarioId == inventario.Id)
                .Count() == inventario.DetallesInventario.Count();
        }

        private async Task RechazoParcial(Inventario inventario, List<DetalleInventario> listaAprobados, List<DetalleInventario> listaDesaprobados)
        {
            inventario.DetallesInventario = listaDesaprobados;
            try
            {
                await inventarioLocalService.SetToRechazadoParcial(inventario, inventario.ComentarioRechazo);
            }
            catch (BusinessException be)
            {
                Toast.ShowMessage("Los inventarios seleccionados no se pueden aprobar.");
                return;
            }

            inventario.DetallesInventario = listaAprobados;
            try
            {
                await inventarioService.SetToAprobadoParcial(inventario);
            }
            catch (BusinessException be)
            {
                Toast.ShowMessage("Los inventarios seleccionados no se pueden rechazar.");
                return;
            }
        }

        private void FiltroMaterial(string value)
        {
            ListaDetallesInventarios.ReplaceRange(detallesList.Where(x => string.IsNullOrWhiteSpace(value) || x.DetalleInventario.Stock.Material.Codigo.Contains(value)));
        }

        private void GoToDetalleInventario(AprobacionMasivaDetalleModel detalleInventarioSelected)
        {
            if (detalleInventarioSelected != null)
            {
                var detalle = new DetalleInventarioModel()
                {
                    DetalleInventario = detalleInventarioSelected.DetalleInventario,
                    ShowComentario = true,
                    ShowCantidad = true
                };
                navigationService.PushAsync<AprobacionDetalleInventarioProvisorioView, VisualizarDetalleMaterialView>(detalle);
            }
        }
    }
}
