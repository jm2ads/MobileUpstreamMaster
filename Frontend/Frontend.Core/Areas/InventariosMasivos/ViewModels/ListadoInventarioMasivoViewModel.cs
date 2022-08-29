using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Frontend.Business.InventariosMasivos;
using Frontend.Core.Areas.InventariosMasivos.IViewModels;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using Xamarin.Forms;

namespace Frontend.Core.Areas.InventariosMasivos.ViewModels
{
    public class ListadoInventarioMasivoViewModel : BaseViewModel, IListadoInventarioMasivoViewModel
    {
        public ObservableRangeCollection<InventarioMasivo> ListaInventariosMasivos { get; set; }
        public ICommand DeleteInventarioMasivoCommnad { get; set; }
        public ICommand RefreshListCommnad { get; set; }

        private readonly IInventarioMasivoService inventarioMasivoService;
        private readonly INavigationService navigationService;
        private readonly IDisplayAlertService displayAlertService;
        private InventarioMasivo _inventarioMasivoSelected;
        public InventarioMasivo InventarioMasivoSelected
        {
            get { return _inventarioMasivoSelected; }
            set
            {
                SetProperty(ref _inventarioMasivoSelected, value);
                GoToDetalleInventarioMasivo(_inventarioMasivoSelected);
            }
        }

        private bool _hasInventario;
        public bool HasInventario
        {
            get { return _hasInventario; }
            set
            {
                SetProperty(ref _hasInventario, value);
            }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }

        public ListadoInventarioMasivoViewModel(IInventarioMasivoService inventarioMasivoService, INavigationService navigationService, IDisplayAlertService displayAlertService)
        {
            this.inventarioMasivoService = inventarioMasivoService;
            this.navigationService = navigationService;
            this.displayAlertService = displayAlertService;
            Init();
        }

        private async void Init()
        {
            Title = "Listado";
            DeleteInventarioMasivoCommnad = new Command<InventarioMasivo>(DeleteInventarioMasivo);
            RefreshListCommnad = new Command(RefreshList);
            ListaInventariosMasivos = new ObservableRangeCollection<InventarioMasivo>();
            await FillListadoInventarioMasivo();
        }

        private async Task FillListadoInventarioMasivo()
        {
            IsRefreshing = true;
            ListaInventariosMasivos.Clear();
            var listado = await inventarioMasivoService.GetAllProvisoriosWithChildren();
            if (listado.Count > 0)
            {
                HasInventario = true;
                ListaInventariosMasivos.AddRange(listado);
            }
            IsRefreshing = false;
        }

        private async void RefreshList(object obj)
        {
            await FillListadoInventarioMasivo();
        }

        private void GoToDetalleInventarioMasivo(InventarioMasivo inventarioMasivoSelected)
        {
            if (inventarioMasivoSelected != null)
            {
                navigationService.PushAsync<ListadoInventarioMasivoView, ListadoPosicionesInventarioMasivoView>(inventarioMasivoSelected);
            }
        }

        private async void DeleteInventarioMasivo(InventarioMasivo inventarioMasivo)
        {
            var answer = await displayAlertService.Show("Eliminar inventario masivo", "¿Desea eliminar el inventario masivo?", "Aceptar", "Cancelar");
            if (answer)
            {
                await inventarioMasivoService.Delete(inventarioMasivo);
                await FillListadoInventarioMasivo();
            }
        }
    }
}
