using Frontend.Business.DetallesInventario;
using Frontend.Business.Inventarios;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Inventarios.ViewModels
{
    public class VisualizarDetalleInventarioSapViewModel : BaseViewModel, IVisualizarDetalleInventarioSapViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly IInventarioService inventarioService;

        public ICommand GoToInformacionInventarioCommand { get; set; }

        public Inventario Inventario { get; set; }
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

        public VisualizarDetalleInventarioSapViewModel(INavigationService navigationService, IDisplayAlertService displayAlertService, IInventarioService inventarioService)
        {
            this.navigationService = navigationService;
            this.displayAlertService = displayAlertService;
            this.inventarioService = inventarioService;

            Init();
        }

        private void Init()
        {
            Inventario = navigationService.GetNavigationParams<VisualizarDetalleInventarioSapView>() as Inventario;
            Title = "Inventario " + Inventario.Codigo;

            ListaDetallesDeInventario = new ObservableRangeCollection<DetalleInventario>();
            ListaDetallesDeInventario.AddRange(Inventario.DetallesInventario);

            GoToInformacionInventarioCommand = new Command(GoToInformacionInventario);

        }

        private void GoToInformacionInventario()
        {
            navigationService.PushAsync<ListaDetalleInventarioView, VisualizarInformacionInventarioView>(Inventario);
        }

        private void GoToDetalleInventario(DetalleInventario detalleInventario)
        {
            if (detalleInventario != null)
            {
                var detalle = new DetalleInventarioModel()
                {
                    DetalleInventario = detalleInventario,
                    ShowComentario = false,
                    ShowCantidad = false
                };
                navigationService.PushAsync<ListaDetalleInventarioView, VisualizarDetalleMaterialView>(detalle);
            }
        }
    }
}
