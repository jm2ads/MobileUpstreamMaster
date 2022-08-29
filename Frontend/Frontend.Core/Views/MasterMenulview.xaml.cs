using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Commons.CustomRenders;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Models;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MenuItem = Frontend.Core.Models.MenuItem;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMenulview : MasterDetailPage
    {
        private readonly INavigationService navigationService;

        public MasterMenulview(INavigationService navigationService)
        {
            InitializeComponent();
            MasterPage.MenuItems.ItemSelected += MenuItemsListViewOnItemSelected;
            this.navigationService = navigationService;
        }

        private void MenuItemsListViewOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var item = selectedItemChangedEventArgs.SelectedItem as MenuItem;
            if (item != null)
            {
                ExecItemListSelected(item);
            }
        }

        private async Task ExecItemListSelected(MenuItem item)
        {
            if (item.Type == MenuItemType.CloseApp)
            {
                DependencyService.Get<ICloseApplication>().CloseApp();
                MasterPage.MenuItems.SelectedItem = null;
                IsPresented = false;
                return;
            }

            if (item.Type == MenuItemType.Logout)
            {
                await StartSpinner();
                Application.Current.MainPage = new CustomNavigationPage(ContainerManager.Resolve<IngresoUsuarioView>());
                await StopSpinner();
                return;
            }

            if (item.Type == MenuItemType.MainPage)
            {
                var page = ContainerManager.Resolve(item.TargetType) as Page;
                Detail = new NavigationPage(page);
                MasterPage.MenuItems.SelectedItem = null;
                IsPresented = false;
            }

            if (item.Type == MenuItemType.DetailPage)
            {
                navigationService.PushFromAsync(typeof(HomeView), item.TargetType);
                MasterPage.MenuItems.SelectedItem = null;
                IsPresented = false;
            }

        }

        public async Task StartSpinner()
        {
            await PopupNavigation.PushAsync(new SpinnerView(), true);
        }

        public async Task StopSpinner()
        {
            await PopupNavigation.PopAsync();
        }
    }
}