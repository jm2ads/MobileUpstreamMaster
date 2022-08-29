using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.Home.IViewModels;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Areas.Home.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        private readonly IHomeViewModel vm;

        public HomeView()
        {
            InitializeComponent();
            vm = ContainerManager.Resolve<IHomeViewModel>();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IHomeViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = vm;
        }
       
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.RefreshSettingsCommand.Execute(null);

            #region ASOSA SYNC
            if ( ! string.IsNullOrEmpty(Frontend.Core.Commons.Globals.flagSync))
            {
                vm.SyncOnDemandCommand.Execute(null);
                Frontend.Core.Commons.Globals.flagSync = null;
            }
            #endregion
        }
    }
}