using Frontend.Business.Commons;
using Frontend.Business.InventariosLocales;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Inventarios.ViewModels
{
    public class ListaInventarioProvisorioViewModel : BaseViewModel, IListaInventarioProvisorioViewModel
    {
        private readonly IDisplayAlertService displayAlertService;
        private readonly INavigationService navigationService;
        private readonly IInventarioLocalService inventarioLocalService;

        public ICommand GetInventariosProvisoriosCommand { get; set; }
        public ICommand DeleteInventarioCommnad { get; set; }
        public Task Refresh { get; set; }

        public ObservableRangeCollection<InventarioLocal> ListaInventariosProvisorios { get; set; }

        private bool _isRefreshing = false;
        public bool IsRefreshing
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

        private InventarioLocal inventarioSelected;
        public InventarioLocal InventarioSelected
        {
            get { return inventarioSelected; }
            set
            {
                SetProperty(ref inventarioSelected, value);
                GoToDetalleInventario(inventarioSelected);
            }
        }
        public ListaInventarioProvisorioViewModel(IDisplayAlertService displayAlertService, INavigationService navigationService, IInventarioLocalService inventarioLocalService)
        {
            this.displayAlertService = displayAlertService;
            this.navigationService = navigationService;
            this.inventarioLocalService = inventarioLocalService;
            Init();
        }

        private void Init()
        {
            Title = "Provisorios";
            GetInventariosProvisoriosCommand = new Command(async () => await RefreshInventariosProvisorios());
            DeleteInventarioCommnad = new Command<InventarioLocal>(async (inventario) => await DeleteInventario(inventario));
            ListaInventariosProvisorios = new ObservableRangeCollection<InventarioLocal>();
            Refresh = GetAllProvisorios();
        }

        private async Task DeleteInventario(InventarioLocal inventario)
        {
            var answer = await displayAlertService.Show("Eliminar inventario", "¿Desea eliminar el inventario?", "Aceptar", "Cancelar");
            if (answer)
            {
                await inventarioLocalService.Delete(inventario);
                await RefreshInventariosProvisorios();
            }
        }

        private async Task RefreshInventariosProvisorios()
        {
            await GetAllProvisorios();
        }

        private async Task GetAllProvisorios()
        {
            if (IsRefreshing)
            {
                return;
            }
            IsRefreshing = true;
            var delay = Task.Delay(2000);
            ListaInventariosProvisorios.Clear();
            ListaInventariosProvisorios.AddRange((await inventarioLocalService.GetAllProvisorios()).OrderByDescending(x => x.FechaModificacion));
            HasInventario = ListaInventariosProvisorios.Count > 0;
            await delay;
            IsRefreshing = false;
        }

        private async Task GoToDetalleInventario(InventarioLocal inventario)
        {
            if (inventario != null)
            {
                var inventarioWithChildren = await inventarioLocalService.GetInventarioById(inventario.Id);

                //Parse to inventario
                var inventarioToDetalle = Helper.MapToInventario(inventarioWithChildren);
                inventarioToDetalle.inventarioLocalId = inventario.Id;

                navigationService.PushAsync<ListaInventarioProvisorioView, ListaDetalleInventarioView>(inventarioToDetalle);
            }
        }
    }
}
