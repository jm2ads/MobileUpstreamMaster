using Frontend.Business.Usuarios;
using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.AboutUs.Views;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Views;
using Frontend.Core.Commons.CustomRenders;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.IViewModels;
using Frontend.Core.Models;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MenuItem = Frontend.Core.Models.MenuItem;

namespace Frontend.Core.ViewModels
{
    public class SideBarMenuViewModel : BaseViewModel, ISideBarViewModel
    {
        private readonly INavigationService navigationService;
        private readonly ISettingsService settingsService;
        private readonly IUsuarioService usuarioService;

        private ObservableRangeCollection<HeaderItem> _headerItems;
        public ObservableRangeCollection<HeaderItem> HeaderItems { get; set; }

        private readonly Dictionary<int, MenuItem> MenuItemDictionary;

        public ICommand GetSettingsCommand { get; set; }
        public ICommand CollapseCommand { get; set; }
        public ICommand HeaderTapCommand { get; set; }


        private string idRed;
        public string IdRed
        {
            get { return idRed; }
            set { SetProperty(ref idRed, value); }
        }

        private string nombreUsuario;
        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set { SetProperty(ref nombreUsuario, value); }
        }

        public SideBarMenuViewModel(INavigationService navigationService, ISettingsService settingsService,
            IUsuarioService usuarioService)
        {
            this.navigationService = navigationService;
            this.settingsService = settingsService;
            this.usuarioService = usuarioService;
            this.GetSettingsCommand = new Command(async () => await ObtainUserName());
            CollapseCommand = new Command<HeaderItem>(Collapse);
            HeaderTapCommand = new Command<HeaderItem>(HeaderTap);

            MenuItemDictionary = new Dictionary<int, MenuItem>();
            _headerItems = new ObservableRangeCollection<HeaderItem>();
            HeaderItems = new ObservableRangeCollection<HeaderItem>();

            FillMenuItemDictionary();
        }

        private async void HeaderTap(HeaderItem headerItem)
        {
            if (headerItem.Type == MenuItemType.Collapse)
            {
                Collapse(headerItem);
                return;
            }

            (App.Current.MainPage as MasterDetailPage).IsPresented = false;

            if (headerItem.Type == MenuItemType.CloseApp)
            {
                DependencyService.Get<ICloseApplication>().CloseApp();
                return;
            }

            if (headerItem.Type == MenuItemType.Logout)
            {
                await StartSpinner();
                Application.Current.MainPage = new CustomNavigationPage(ContainerManager.Resolve<IngresoUsuarioView>());
                await StopSpinner();
                return;
            }

            if (headerItem.Type == MenuItemType.MainPage)
            {
                navigationService.PushFromRootAsync(headerItem.TargetType);
                return;
            }

            if (headerItem.Type == MenuItemType.DetailPage)
            {
                navigationService.PushFromAsync(typeof(HomeView), headerItem.TargetType);
                return;
            }
        }

        private void Collapse(HeaderItem menuItem)
        {
            var indexSelected = _headerItems.Select(x => x.Title).ToList().IndexOf(menuItem.Title);
            _headerItems[indexSelected].Expanded = !_headerItems[indexSelected].Expanded;
            UpdateList();
        }

        private void UpdateList()
        {
            HeaderItems.Clear();
            foreach (var item in _headerItems)
            {
                if (item.Count > 0 || item.Type != MenuItemType.Collapse)
                {
                    var newGroup = new HeaderItem(item.Title, item.HeaderIcon, item.Type, item.TargetType, item.Expanded, item.StateIconVisible);
                    if (item.Expanded)
                    {
                        foreach (var menuItem in item)
                        {
                            newGroup.Add(menuItem);
                        }

                    }
                    HeaderItems.Add(newGroup);
                }

            }
        }

        private void FillMenuItemDictionary()
        {
            MenuItemDictionary.Add(1, new MenuItem() { Title = "Crear inventario", Icon = "round_add_circle_outline_black_24", TargetType = typeof(InformacionInventarioView), Type = MenuItemType.DetailPage, GroupType = GroupType.Inventario });
            MenuItemDictionary.Add(2, new MenuItem() { Title = "Listado inventario", Icon = "round_list_alt_black_24", TargetType = typeof(ListaInventarioView), Type = MenuItemType.DetailPage, GroupType = GroupType.Inventario });
            MenuItemDictionary.Add(3, new MenuItem() { Title = "Aprobación inventario", Icon = "round_thumbs_up_down_black_24", TargetType = typeof(AprobacionInventarioView), Type = MenuItemType.DetailPage, GroupType = GroupType.Inventario });
            MenuItemDictionary.Add(4, new MenuItem() { Title = "Recuento inventario", Icon = "baseline_edit_black_24", TargetType = typeof(RecuentoView), Type = MenuItemType.DetailPage, GroupType = GroupType.Inventario });
            MenuItemDictionary.Add(5, new MenuItem() { Title = "Consulta stock", Icon = "baseline_search_black_24", TargetType = typeof(ConsultaStockView), Type = MenuItemType.DetailPage, GroupType = GroupType.Inventario });
            MenuItemDictionary.Add(8, new MenuItem() { Title = "Ingreso por pedido", Icon = "baseline_note_add_black_24", TargetType = typeof(IngresoCompraView), Type = MenuItemType.DetailPage, GroupType = GroupType.Movimiento });
            MenuItemDictionary.Add(9, new MenuItem() { Title = "Devolución de material", Icon = "round_restore_page_black_24", TargetType = typeof(DevolucionView), Type = MenuItemType.DetailPage, GroupType = GroupType.Movimiento });
            MenuItemDictionary.Add(10, new MenuItem() { Title = "Salida de materiales", Icon = "baseline_exit_to_app_black_24", TargetType = typeof(SalidaView), Type = MenuItemType.DetailPage, GroupType = GroupType.Movimiento });
            MenuItemDictionary.Add(12, new MenuItem() { Title = "Crear movimiento de traslado", Icon = "baseline_low_priority_black_24", TargetType = typeof(CrearTrasladoView), Type = MenuItemType.DetailPage, GroupType = GroupType.Movimiento });
            MenuItemDictionary.Add(13, new MenuItem() { Title = "Salida por venta interna", Icon = "baseline_settings_backup_restore_black_24", TargetType = typeof(SalidaPorVentaInternaView), Type = MenuItemType.DetailPage, GroupType = GroupType.Movimiento });
            MenuItemDictionary.Add(14, new MenuItem() { Title = "Salida por pedido de traspaso", Icon = "baseline_import_export_black_24", TargetType = typeof(SalidaPedidoTraspasoView), Type = MenuItemType.DetailPage, GroupType = GroupType.Movimiento });
            MenuItemDictionary.Add(15, new MenuItem() { Title = "Crear inventario masivo", Icon = "baseline_layers_black_24", TargetType = typeof(InformacionInventarioMasivoView), Type = MenuItemType.DetailPage, GroupType = GroupType.Inventario });
            MenuItemDictionary.Add(16, new MenuItem() { Title = "Cambio de ubicación", Icon = "baseline_wrap_text_black_24", TargetType = typeof(CrearCambioUbicacionView), Type = MenuItemType.DetailPage, GroupType = GroupType.Inventario });
            MenuItemDictionary.Add(17, new MenuItem() { Title = "Aprobación masiva", Icon = "baseline_playlist_add_check_black_24", TargetType = typeof(ListadoDeMaterialesAprobacionView), Type = MenuItemType.DetailPage, GroupType = GroupType.Inventario });
            MenuItemDictionary.Add(18, new MenuItem() { Title = "Devolución/Salida de material", Icon = "round_restore_page_black_24", TargetType = typeof(ReservaView), Type = MenuItemType.DetailPage, GroupType = GroupType.Movimiento });
        }

        public async Task ObtainUserName()
        {
            var setting = await settingsService.GetWithChildren();
            var usuario = await usuarioService.GetById(setting.UsuarioActivoId);
            IdRed = usuario.IdRed.ToUpper();
            NombreUsuario = usuario.Nombre;
            GetFuncionalidades(usuario);
        }

        private void GetFuncionalidades(Usuario usuario)
        {
            _headerItems.Clear();
            _headerItems.Add(new HeaderItem() { Title = "Inicio", HeaderIcon = "ic_home_black_24dp", TargetType = typeof(HomeView), Type = MenuItemType.MainPage });
            var inventarios = new HeaderItem()
            {
                Title = "Inventario",
                StateIconVisible = true,
                HeaderIcon = "baseline_store_black_24",
                Type = MenuItemType.Collapse
            };

            foreach (var funcionalidad in usuario.Funcionalidades.OrderBy(func => func.Orden))
            {
                if (MenuItemDictionary.ContainsKey(funcionalidad.Id)
                    && MenuItemDictionary[funcionalidad.Id].GroupType == GroupType.Inventario)
                {
                    inventarios.Add(MenuItemDictionary[funcionalidad.Id]);
                }
            }
            _headerItems.Add(inventarios);


            var movimientos = new HeaderItem()
            {
                Title = "Movimientos",
                StateIconVisible = true,
                HeaderIcon = "baseline_swap_horiz_black_24",
                Type = MenuItemType.Collapse
            };

            foreach (var funcionalidad in usuario.Funcionalidades.OrderBy(func => func.Orden))
            {
                if (MenuItemDictionary.ContainsKey(funcionalidad.Id)
                    && MenuItemDictionary[funcionalidad.Id].GroupType == GroupType.Movimiento)
                {
                    movimientos.Add(MenuItemDictionary[funcionalidad.Id]);
                }
            }
            _headerItems.Add(movimientos);

            _headerItems.Add(new HeaderItem() { Title = "Acerca de", HeaderIcon = "ic_contacts_black_24dp", TargetType = typeof(AboutUsView), Type = MenuItemType.DetailPage });
            _headerItems.Add(new HeaderItem() { Title = "Cerrar sesión", HeaderIcon = "ic_perm_identity_black_24dp", TargetType = typeof(LoginView), Type = MenuItemType.Logout });

            UpdateList();
        }
    }
}