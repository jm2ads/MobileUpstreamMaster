using Frontend.Business.DetallesInventario;
using Frontend.Business.Inventarios;
using Frontend.Commons.Commons.Errors;
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
    public class RecuentoDetalleInventarioViewModel : BaseViewModel, IRecuentoDetalleInventarioViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly IInventarioService inventarioService;

        public ICommand GoToSearchMaterialCommand { get; set; }
        public ICommand DeleteMaterialCommnad { get; set; }
        public ICommand FinalizarInventarioCommand { get; set; }
        public ICommand GoToInformacionInventarioCommand { get; set; }
        public ICommand FiltroPosicionCommand { get; set; }
        private IList<DetalleInventario> posicionesViewList;

        public ObservableRangeCollection<DetalleInventario> ListaDetallesDeInventario { get; set; }

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

        public Inventario inventario { get; set; }

        public RecuentoDetalleInventarioViewModel(INavigationService navigationService, IDisplayAlertService displayAlertService, IInventarioService inventarioService)
        {
            this.navigationService = navigationService;
            this.displayAlertService = displayAlertService;
            this.inventarioService = inventarioService;

            Init();
        }

        private void Init()
        {
            posicionesViewList = new List<DetalleInventario>();
            GoToSearchMaterialCommand = new Command(GoToSearchMaterial);
            FinalizarInventarioCommand = new Command(async () => await FinalizarInventario());
            GoToInformacionInventarioCommand = new Command(GoToInformacionInventario);
            FiltroPosicionCommand = new Command<string>(Filtro);
            inventario = navigationService.GetNavigationParams<RecuentoDetalleInventarioView>() as Inventario;
            Title = "Inventario " + inventario.Codigo;
            ListaDetallesDeInventario = new ObservableRangeCollection<DetalleInventario>();
            InitListaDetalleInventario();

        }

        private async void InitListaDetalleInventario()
        {
            inventario = await inventarioService.GetInventarioById(inventario.Id);
            posicionesViewList = inventario.DetallesInventario.OrderBy(x => x.Posicion).ToList();
            ListaDetallesDeInventario.AddRange(posicionesViewList);
        }

        private void GoToInformacionInventario()
        {
            navigationService.PushAsync<RecuentoDetalleInventarioView, VisualizarInformacionInventarioView>(inventario);
        }

        private void Filtro(string filtro)
        {
            ListaDetallesDeInventario.Clear();
            ListaDetallesDeInventario.AddRange(posicionesViewList.Where(x => String.IsNullOrEmpty(filtro) || x.Posicion.ToString().Contains(filtro)));
        }

        private async Task FinalizarInventario()
        {
            IsBusy = true;
            var answer = await displayAlertService.Show("Finalizar inventario", "¿Desea finalizar el inventario?", "Aceptar", "Cancelar");
            if (answer)
            {
                try
                {
                    await inventarioService.SetToPendienteAprobacionSap(inventario);
                    navigationService.PushNextMasterDetailPage<HomeView, HomeView>();
                    Toast.ShowMessage("El recuento ha finalizado con éxito");
                }
                catch (BusinessException be)
                {
                    Toast.ShowMessage(be.Mensaje);
                }
                finally
                {
                    IsBusy = false;
                }
            }
            IsBusy = false;
        }

       
        private void GoToSearchMaterial()
        {
            navigationService.PushAsync<RecuentoDetalleInventarioView, SearchMaterialView>(inventario);
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
                navigationService.PushAsync<RecuentoDetalleInventarioView, RecuentoDetalleMaterialView>(detalle);
            }
        }
    }
}
