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
    public class AprobacionDetalleInventarioSapViewModel : BaseViewModel, IAprobacionDetalleInventarioSapViewModel
    {
        private readonly IDisplayAlertService displayAlertService;
        private readonly INavigationService navigationService;
        private readonly IInventarioService inventarioService;
        private readonly IDetalleInventarioService detalleInventarioService;


        public ICommand GoToSearchMaterialCommand { get; set; }
        public ICommand GoToInformacionInventarioCommand { get; set; }

        public ICommand AprobarCommand { get; set; }
        public ICommand RechazarCommand { get; set; }
        public ICommand ComentarioCommand { get; set; }
        public ICommand GetInventarioSapCommand { get; set; }

        private DetalleInventario detalleInventarioSelected;
        public DetalleInventario DetalleInventarioSelected
        {
            get { return detalleInventarioSelected; }
            set
            {
                SetProperty(ref detalleInventarioSelected, value);
                GoToDetalleInventario(detalleInventarioSelected);
            }
        }

        public ObservableRangeCollection<DetalleInventario> ListaDetallesDeInventario { get; private set; }
        public Inventario inventario { get; private set; }

        public AprobacionDetalleInventarioSapViewModel(IDisplayAlertService displayAlertService, INavigationService navigationService,
            IInventarioService inventarioService, IDetalleInventarioService detalleInventarioService)
        {
            this.displayAlertService = displayAlertService;
            this.navigationService = navigationService;
            this.inventarioService = inventarioService;
            this.detalleInventarioService = detalleInventarioService;

            Init();
        }

        private void Init()
        {
            GoToSearchMaterialCommand = new Command(GoToSearchMaterial);
            GoToInformacionInventarioCommand = new Command(GoToInformacionInventario);
            AprobarCommand = new Command(async () => await Aprobar());
            RechazarCommand = new Command(async () => await Rechazar());
            ComentarioCommand = new Command(Comentario);
            GetInventarioSapCommand = new Command(GetInventarioSap);

            inventario = navigationService.GetNavigationParams<AprobacionDetalleInventarioSapView>() as Inventario;

            Title = "Inventario " + inventario.Codigo;
            ListaDetallesDeInventario = new ObservableRangeCollection<DetalleInventario>();
            ListaDetallesDeInventario.AddRange(inventario.DetallesInventario.Where(x => x.EsContado).OrderBy(x=>x.Posicion).ToList());
        }

        private async void GetInventarioSap()
        {
            inventario = await inventarioService.GetInventarioById(inventario.Id);
        }

        private async Task Aprobar()
        {
            IsBusy = true;
            var answer = await displayAlertService.Show("Aprobar inventario", "¿Confirma que desea aprobar el inventario?", "Aceptar", "Cancelar");
            if (answer)
            {
                await inventarioService.SetToAprobado(inventario);

                #region ASOSA flagSync Aprobación inventarios (Provisorios y SAP)l
                Frontend.Core.Commons.Globals.flagSync = "AprobacionDetalleInventarioSapViewModel";
                #endregion

                Toast.ShowMessage("Inventario aprobado");
                navigationService.PushFromAsync<HomeView, AprobacionInventarioView>();
            }
            IsBusy = false;
        }

        private async Task Rechazar()
        {
            IsBusy = true;
            var answer = await displayAlertService.Show("Rechazar inventario", "¿Confirma que desea rechazar el inventario?", "Aceptar", "Cancelar");
            if (answer)
            {
                if (!ValidateComentarios())
                {
                    Toast.ShowMessage("El inventario o todas las posiciones deben tener un comentario de rechazo");
                    IsBusy = false;
                    return;
                }
                await inventarioService.SetToRechazado(inventario);
                Toast.ShowMessage("Inventario rechazado");
                navigationService.PushFromAsync<HomeView, AprobacionInventarioView>();
            }

            IsBusy = false;
        }
        private bool ValidateComentarios()
        {
            return !string.IsNullOrEmpty(inventario.ComentarioRechazo) || ListaDetallesDeInventario.All(x => !String.IsNullOrEmpty(x.Comentario));
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
            navigationService.PushAsync<AprobacionDetalleInventarioSapView, AgregarComentarioView>(comentarioModel);
        }

        private void GoToInformacionInventario()
        {
            navigationService.PushAsync<AprobacionDetalleInventarioSapView, VisualizarInformacionInventarioView>(inventario);
        }

        private void GoToSearchMaterial()
        {
            navigationService.PushAsync<AprobacionDetalleInventarioSapView, SearchMaterialView>(inventario);
        }

        private void GoToDetalleInventario(DetalleInventario detalleInventario)
        {
            if (detalleInventario != null)
            {
                var detalle = new DetalleInventarioModel()
                {
                    DetalleInventario = detalleInventario,
                    ShowComentario = true,
                    ShowCantidad = true
                };
                navigationService.PushAsync<AprobacionDetalleInventarioSapView, VisualizarDetalleMaterialView>(detalle);
            }
        }

    }
}
