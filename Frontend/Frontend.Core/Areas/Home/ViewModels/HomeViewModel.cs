using Frontend.Business.Settings;
using Frontend.Business.Usuarios;
using Frontend.Commons.Commons;
using Frontend.Commons.Commons.Errors;
using Frontend.Core.Areas.Home.IViewModels;
using Frontend.Core.Areas.Home.Models;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Views;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel, IHomeViewModel
    {
        public ICommand SyncInicialCommand { get; set; }
        public ICommand SyncCommand { get; set; }
        public ICommand SyncOnDemandCommand { get; set; }

        public ICommand AlertCommand { get; set; }
        public ICommand GoToCrearInventarioCommand { get; set; }
        public ICommand GoToCrearInventarioMasivoCommand { get; set; }
        public ICommand GoToCrearCambioUbicacionCommand { get; set; }
        public ICommand GoToConsultaInventarioCommand { get; set; }
        public ICommand GoToAprobarInventarioCommand { get; set; }
        public ICommand GoToAprobacionMasivaCommand { get; set; }
        public ICommand GoToRecuentoInventarioCommand { get; set; }
        public ICommand SincronizarCommand { get; set; }
        public ICommand GoToListaInventarioCommand { get; set; }
        public ICommand GoToIngresoCompraCommand { get; set; }
        public ICommand GoToDevolucionMaterialCommand { get; set; }
        public ICommand GoToSalidaMaterialCommand { get; set; }
        public ICommand GoToCrearTrasladoCommand { get; set; }
        public ICommand GoToSalidaInternaCommand { get; set; }
        public ICommand GoToSalidaTraspasoCommand { get; set; }
        public ICommand CambiarCentroCommand { get; set; }
        public ICommand GoToLogCommand { get; set; }
        public ICommand GoToReservaMaterialCommand { get; set; }
        public ICommand RefreshSettingsCommand { get; set; }

        private readonly INavigationService navigationService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly ISyncService syncService;
        private readonly ISettingsService settingsService;
        private readonly IUsuarioService usuarioService;
        private readonly IInventarioService inventarioService;
        private readonly INetworkConnection networkConnection;
        private bool aux = false;

        public string AppName { get; set; }

        public string flagSync { get; set; }


        private string _centroActivo;
        public string CentroActivo
        {
            get { return _centroActivo; }
            set
            {
                SetProperty(ref _centroActivo, value);
            }
        }
        public Setting settings { get; set; }


        private bool _hasSyncError;
        public bool HasSyncError
        {
            get { return _hasSyncError; }
            set
            {
                SetProperty(ref _hasSyncError, value);
            }
        }

        private bool _isPendingToSync;
        public bool IsPendingToSync
        {
            get { return _isPendingToSync; }
            set
            {
                SetProperty(ref _isPendingToSync, value);
            }
        }
        private readonly Dictionary<int, HomeModel> MenuItemDictionary;

        public ObservableRangeCollection<HomeModel> Items { get; set; }

        public HomeViewModel(INavigationService navigationService, IDisplayAlertService displayAlertService, IInventarioService inventarioService, ISyncService syncService,
            ISettingsService settingsService, IUsuarioService usuarioService)
        {
            this.navigationService = navigationService;
            this.displayAlertService = displayAlertService;
            this.syncService = syncService;
            this.settingsService = settingsService;
            this.usuarioService = usuarioService;
            this.inventarioService = inventarioService;
            networkConnection = DependencyService.Get<INetworkConnection>();

            MenuItemDictionary = new Dictionary<int, HomeModel>();
            Init();
        }

        private async void Init()
        {
            AppName = ApplicationConstants.ApplicationName;
            Items = new ObservableRangeCollection<HomeModel>();
            AlertCommand = new Command(Alert);
            GoToCrearInventarioCommand = new Command(GoToCrearInventario);
            GoToCrearInventarioMasivoCommand = new Command(GoToCrearInventarioMasivo);
            GoToCrearCambioUbicacionCommand = new Command(GoToCrearCambioUbicacion);
            SincronizarCommand = new Command(async () => await Sincronizar());
            GoToListaInventarioCommand = new Command(GoToListaInventario);
            GoToConsultaInventarioCommand = new Command(GoToConsultaInventario);
            GoToAprobarInventarioCommand = new Command(GoToAprobarInventario);
            GoToAprobacionMasivaCommand = new Command(GoToAprobacionMasiva);
            GoToRecuentoInventarioCommand = new Command(GoToRecuentoInventario);
            SyncCommand = new Command(async () => await Sync());
            SyncOnDemandCommand = new Command(async () => await SyncOnDemand());
            GoToIngresoCompraCommand = new Command(GoToIngresoCompra);
            GoToReservaMaterialCommand = new Command(GoToReservaMaterial);
            GoToDevolucionMaterialCommand = new Command(GoToDevolucionMaterial);
            GoToSalidaMaterialCommand = new Command(GoToSalidaMaterial);
            GoToCrearTrasladoCommand = new Command(GoToCrearTraslado);
            GoToSalidaInternaCommand = new Command(GoToSalidaInterna);
            GoToSalidaTraspasoCommand = new Command(GoToSalidaTraspaso);
            CambiarCentroCommand = new Command(GoToCambiarCentro);
            GoToLogCommand = new Command(GoToLog);
            RefreshSettingsCommand = new Command(RefreshSettings);


            SyncInicialCommand = new Command(SyncInicial);

        }





        private async void RefreshSettings()
        {
            await GetSettings();
            await InitialSync();
        }

        private async Task GetSettings()
        {
            settings = await settingsService.GetWithChildren();
            CompleteSettings(settings);
            FillMenuItemDictionary();
            GetFuncionalidades();
        }

        private async Task RefreshFuncionalidades()
        {
            await usuarioService.UpdateFuncionalidades(settings.UsuarioActivo, settings.CentroActivoId);
            FillMenuItemDictionary();
            GetFuncionalidades();
        }

        private void CompleteSettings(Setting settings)
        {
            HasSyncError = settings.HasSyncWithError;
            IsPendingToSync = settings.IsPendingToSync;
            Title = "Centro " + settings.CentroActivo.Codigo;
        }

        private async void SyncInicial()
        {
            await InitialSync();
        }



        public async Task SyncOnDemand()
        {
            IsBusyOnDemand = true;
            try
            {
                networkConnection.CheckConnection();
                if (!networkConnection.IsConnected)
                {
                    displayAlertService.Show("Sin Conexión", "Intente obtener una conexión estable de datos 3G, 4G o WIFI para poder realizar la validación", "Cerrar");
                    IsBusyOnDemand = false;
                    return;
                }
                await this.syncService.SyncDataParcial();
                CompleteSettings(await settingsService.SetPendingToSync(false));
                await RefreshFuncionalidades();
                displayAlertService.Show("Sincronización", "Sincronización terminada", "Aceptar");
            }
            catch (Exception e)
            {
                HandlerException(e);
            }
            finally
            {
                IsBusyOnDemand = false;
            }
        }
        public async Task Sync()
        {
            await StartSpinner("Sincronizando...");
            try
            {
                networkConnection.CheckConnection();
                if (!networkConnection.IsConnected)
                {
                    displayAlertService.Show("Sin Conexión", "Intente obtener una conexión estable de datos 3G, 4G o WIFI para poder realizar la validación", "Cerrar");
                    await StopSpinner();
                    return;
                }
                await this.syncService.SyncDataParcial();
                CompleteSettings(await settingsService.SetPendingToSync(false));
                await RefreshFuncionalidades();
                displayAlertService.Show("Sincronización", "Sincronización terminada", "Aceptar");
            }
            catch (Exception e)
            {
                HandlerException(e);
            }
            finally
            {
                await StopSpinner();
            }
        }

        public async Task InitialSync()
        {
            try
            {
                if (settingsService.ValidateInitialSync(settings))
                {
                    await StartSpinner("Sincronizando...");
                    networkConnection.CheckConnection();
                    if (!networkConnection.IsConnected)
                    {
                        displayAlertService.Show("Sin Conexión", "Intente obtener una conexión estable de datos 3G, 4G o WIFI para poder realizar la validación", "Cerrar");
                        await StopSpinner();
                        return;
                    }
                    await this.syncService.DropData();
                    await this.syncService.SyncData();
                    CompleteSettings(await settingsService.SetPendingToSync(false));
                    await RefreshFuncionalidades();
                    displayAlertService.Show("Sincronización", "Sincronización terminada", "Aceptar");
                }
            }
            catch (Exception e)
            {
                HandlerException(e);
            }
            finally
            {
                await StopSpinner();
            }
        }


        void HandlerException(Exception e)
        {
            string mensaje;
            if (e is BusinessException)
            {
                var be = e as BusinessException;
                mensaje = be.Mensaje;
                displayAlertService.Show("Sincronización", "No se pudo completar la sincronización. "
                    + be.Mensaje, "Aceptar");
            }
            else
            {
                mensaje = e.Message;
                displayAlertService.Show("Sincronización", "No se pudo completar la sincronización, intente de nuevo", "Aceptar");
            }

            Crashes.TrackError(e, new Dictionary<string, string>{
                        { "Title", "Error en sincronización" },
                        { "Message", mensaje }
                    });
        }

        private void FillMenuItemDictionary()
        {
            MenuItemDictionary.Clear();
            Items.Clear();

            MenuItemDictionary.Add(1, new HomeModel() { Title = "Crear inventario", Image = "outline_add_circle_outline_white_48", Command = GoToCrearInventarioCommand });
            MenuItemDictionary.Add(2, new HomeModel() { Title = "Listado inventario", Image = "round_list_alt_white_48", Command = GoToListaInventarioCommand });
            MenuItemDictionary.Add(3, new HomeModel() { Title = "Aprobación inventario", Image = "round_thumbs_up_down_white_48", Command = GoToAprobarInventarioCommand });
            MenuItemDictionary.Add(17, new HomeModel() { Title = "Aprobación masiva", Image = "baseline_playlist_add_check_white_48", Command = GoToAprobacionMasivaCommand });
            MenuItemDictionary.Add(4, new HomeModel() { Title = "Recuento inventario", Image = "baseline_edit_white_48", Command = GoToRecuentoInventarioCommand });
            MenuItemDictionary.Add(5, new HomeModel() { Title = "Consulta stock", Image = "round_search_white_48", Command = GoToConsultaInventarioCommand });
            MenuItemDictionary.Add(15, new HomeModel() { Title = "Crear inventario masivo", Image = "baseline_layers_white_48", Command = GoToCrearInventarioMasivoCommand });
            MenuItemDictionary.Add(16, new HomeModel() { Title = "Cambio de ubicación", Image = "baseline_wrap_text_white_48", Command = GoToCrearCambioUbicacionCommand });
            MenuItemDictionary.Add(8, new HomeModel() { Title = "Ingreso por pedido", Image = "baseline_note_add_white_48", Command = GoToIngresoCompraCommand });
            MenuItemDictionary.Add(18, new HomeModel() { Title = "Devolución/Salida de material", Image = "round_restore_page_white_48", Command = GoToReservaMaterialCommand });
            MenuItemDictionary.Add(9, new HomeModel() { Title = "Devolución de material", Image = "round_restore_page_white_48", Command = GoToDevolucionMaterialCommand });
            MenuItemDictionary.Add(10, new HomeModel() { Title = "Salida de materiales", Image = "baseline_exit_to_app_white_48", Command = GoToSalidaMaterialCommand });
            MenuItemDictionary.Add(12, new HomeModel() { Title = "Crear movimiento de traslado", Image = "baseline_low_priority_white_48", Command = GoToCrearTrasladoCommand });
            MenuItemDictionary.Add(13, new HomeModel() { Title = "Salida por venta interna", Image = "baseline_settings_backup_restore_white_48", Command = GoToSalidaInternaCommand });
            MenuItemDictionary.Add(14, new HomeModel() { Title = "Salida por pedido de traspaso", Image = "baseline_import_export_white_48", Command = GoToSalidaTraspasoCommand });
        }

        private void GetFuncionalidades()
        {
            var index = 0;
            aux = false;
            var enumerator = settings.UsuarioActivo.Funcionalidades.OrderBy(func => func.Orden).GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (MenuItemDictionary.ContainsKey(enumerator.Current.Id))
                {
                    AddItem(MenuItemDictionary[enumerator.Current.Id], index);
                    index++;
                }
            }
        }

        private async Task Sincronizar()
        {
            await StartSpinner("Sincronizando...");
            await syncService.SyncData();
            await StopSpinner();
        }

        public void Alert()
        {
            displayAlertService.Show("Informacion", "Mensaje", "Aceptar");
        }

        private void AddItem(HomeModel homeModel, int index)
        {
            homeModel.Color = aux ? (Color)Application.Current.Resources["Primary"] : (Color)Application.Current.Resources["Secondary"];
            Items.Add(homeModel);

            if (index % 2 == 0)
            {
                aux = !aux;
            }
        }

        private void GoToRecuentoInventario()
        {
            navigationService.PushAsync<HomeView, RecuentoView>();
        }

        private void GoToConsultaInventario()
        {
            navigationService.PushAsync<HomeView, ConsultaStockView>();
        }

        private void GoToAprobarInventario()
        {
            navigationService.PushAsync<HomeView, AprobacionInventarioView>();
        }
        private void GoToAprobacionMasiva()
        {
            navigationService.PushAsync<HomeView, ListadoDeMaterialesAprobacionView>();
        }

        private void GoToCrearInventario()
        {
            navigationService.PushAsync<HomeView, InformacionInventarioView>();
        }

        private void GoToCrearInventarioMasivo()
        {
            navigationService.PushAsync<HomeView, InventarioMasivoView>();
        }

        private void GoToCrearCambioUbicacion()
        {
            navigationService.PushAsync<HomeView, CrearCambioUbicacionView>();
        }

        private void GoToListaInventario()
        {
            navigationService.PushAsync<HomeView, ListaInventarioView>();
        }

        private void GoToIngresoCompra()
        {
            navigationService.PushAsync<HomeView, IngresoCompraView>();
        }

        private void GoToReservaMaterial(object obj)
        {
            navigationService.PushAsync<HomeView, ReservaView>();
        }

        private void GoToDevolucionMaterial()
        {
            navigationService.PushAsync<HomeView, DevolucionView>();
        }

        private void GoToSalidaMaterial()
        {
            navigationService.PushAsync<HomeView, SalidaView>();
        }

        private void GoToCrearTraslado()
        {
            navigationService.PushAsync<HomeView, CrearTrasladoView>();
        }

        private void GoToSalidaInterna()
        {
            navigationService.PushAsync<HomeView, SalidaPorVentaInternaView>();
        }

        private void GoToSalidaTraspaso()
        {
            navigationService.PushAsync<HomeView, SalidaPedidoTraspasoView>();
        }

        private void GoToCambiarCentro()
        {
            navigationService.PushAsync<HomeView, CambioCentroView>();
        }

        private void GoToLog()
        {
            navigationService.PushAsync<HomeView, LogView>();
        }
    }
}
