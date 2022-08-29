using Frontend.Business.Inventarios;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Inventarios.ViewModels
{
    public class ListaInventarioSapViewModel : BaseViewModel, IListaInventarioSapViewModel
    {
        private readonly IDisplayAlertService displayAlertService;
        private readonly INavigationService navigationService;
        private readonly IInventarioService inventarioService;

        public ICommand GetInventariosSapCommand { get; set; }

        public ObservableRangeCollection<Inventario> ListaInventariosSap { get; set; }
        public Task Refresh { get; set; }

        private bool _isRefreshingSap = false;
        public bool IsRefreshingSap
        {
            get { return _isRefreshingSap; }
            set
            {
                SetProperty(ref _isRefreshingSap, value);
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

        private Inventario inventarioSelected;
        public Inventario InventarioSelected
        {
            get { return inventarioSelected; }
            set
            {
                SetProperty(ref inventarioSelected, value);
                GoToDetalleInventario(inventarioSelected);
            }
        }

        public ListaInventarioSapViewModel(IDisplayAlertService displayAlertService, INavigationService navigationService, IInventarioService inventarioService)
        {
            this.displayAlertService = displayAlertService;
            this.navigationService = navigationService;
            this.inventarioService = inventarioService;
            Init();
        }

        private void Init()
        {
            Title = "SAP";
            GetInventariosSapCommand = new Command(async () => await RefreshInventariosSap());
            ListaInventariosSap = new ObservableRangeCollection<Inventario>();
            Refresh = GetAllSap();
        }
        
        private async Task RefreshInventariosSap()
        {
            await GetAllSap();
        }

        private async Task GetAllSap()
        {
            if (IsRefreshingSap)
            {
                return;
            }
            IsRefreshingSap = true;
            var delay = Task.Delay(2000);
            ListaInventariosSap.Clear();
            ListaInventariosSap.AddRange((await inventarioService.GetAllSap()).OrderByDescending(x=>x.FechaModificacion));
            HasInventario = ListaInventariosSap.Count > 0;
            await delay;
            IsRefreshingSap = false;
        }

        private async Task GoToDetalleInventario(Inventario inventario)
        {
            if (inventario != null)
            {
                var inventarioWithChildren = await inventarioService.GetInventarioById(inventario.Id);
                navigationService.PushAsync<ListaInventarioProvisorioView, VisualizarDetalleInventarioSapView>(inventarioWithChildren);
            }
        }
    }
}
