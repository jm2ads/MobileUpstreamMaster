using Frontend.Commons.Bootstrapper;
using Frontend.Core.Commons.Container;
using Frontend.Core.IViewModels;
using Frontend.Core.Models;
using Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SideBarMenuView : ContentPage
    {
        private readonly ISideBarViewModel sideBarViewModel;
        public ListView MenuItems { get; set; }
        public ListView UserSettings { get; set; }

        public SideBarMenuView()
        {
            InitializeComponent();
            BindingContext = this.sideBarViewModel = ContainerManager.Resolve<ISideBarViewModel>();
            MenuItems = MenuItemsListView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.sideBarViewModel.GetSettingsCommand.Execute(null);
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            var button = ((Button)sender);
            sideBarViewModel.CollapseCommand.Execute(button.CommandParameter);
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            var tapGestureRecognizer = ((StackLayout)sender).GestureRecognizers[0] as TapGestureRecognizer;
            sideBarViewModel.HeaderTapCommand.Execute(tapGestureRecognizer.CommandParameter);
        }
    }
}