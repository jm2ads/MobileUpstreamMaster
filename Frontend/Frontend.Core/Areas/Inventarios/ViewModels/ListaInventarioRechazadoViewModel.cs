using Frontend.Business.Inventarios;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.Areas.Inventarios.Models;
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
    public class ListaInventarioRechazadoViewModel : BaseViewModel, IListaInventarioRechazadoViewModel
    {
        private readonly IDisplayAlertService displayAlertService;
        private readonly INavigationService navigationService;
        private readonly IInventarioService inventarioService;

        public ICommand GetInventariosRechazadosCommand { get; set; }
        public ICommand VerComentarioCommnad { get; set; }
        public Task Refresh { get; set; }

        public ObservableRangeCollection<ListaInventarioRechazadoModel> ListaInventariosRechazados { get; set; }

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

        private ListaInventarioRechazadoModel inventarioSelected;
        public ListaInventarioRechazadoModel InventarioSelected
        {
            get { return inventarioSelected; }
            set
            {
                SetProperty(ref inventarioSelected, value);
                GoToDetalleInventario(inventarioSelected);
            }
        }
        public ListaInventarioRechazadoViewModel(IDisplayAlertService displayAlertService, INavigationService navigationService, IInventarioService inventarioService)
        {
            this.displayAlertService = displayAlertService;
            this.navigationService = navigationService;
            this.inventarioService = inventarioService;
            Init();
        }

        private void Init()
        {
            Title = "Rechazados";
            GetInventariosRechazadosCommand = new Command(async () => await RefreshInventariosRechazados());
            VerComentarioCommnad = new Command<ListaInventarioRechazadoModel>(VerComentario);
            ListaInventariosRechazados = new ObservableRangeCollection<ListaInventarioRechazadoModel>();
            Refresh = GetAllRechazados();
        }

        private async Task RefreshInventariosRechazados()
        {
            await GetAllRechazados();
        }

        private async void VerComentario(ListaInventarioRechazadoModel inventarioModel)
        {
            var inventarioWithChildren = await inventarioService.GetInventarioById(inventarioModel.Inventario.Id);
            navigationService.PushAsync<ListaInventarioRechazadoView, VisualizarInformacionInventarioView>(inventarioWithChildren);
        }

        private async Task GetAllRechazados()
        {
            if (IsRefreshing)
            {
                return;
            }
            IsRefreshing = true;
            var delay = Task.Delay(2000);
            ListaInventariosRechazados.Clear();
            ListaInventariosRechazados.AddRange((await inventarioService.GetAllRechazados()).Select(inventario => new ListaInventarioRechazadoModel(inventario)).OrderByDescending(x => x.Inventario.FechaModificacion));
            HasInventario = ListaInventariosRechazados.Count > 0;
            await delay;
            IsRefreshing = false;
        }

        private async Task GoToDetalleInventario(ListaInventarioRechazadoModel listaInventarioRechazadoModel)
        {
            if (listaInventarioRechazadoModel != null)
            {
                var inventarioWithChildren = await inventarioService.GetInventarioById(listaInventarioRechazadoModel.Inventario.Id);

                if (inventarioWithChildren.EsProvisorio)
                {
                    navigationService.PushAsync<ListaInventarioRechazadoView, ListaDetalleInventarioView>(inventarioWithChildren);
                }
                else
                {
                    navigationService.PushAsync<ListaInventarioRechazadoView, RecuentoDetalleInventarioView>(inventarioWithChildren);
                }

            }
        }
    }
}
