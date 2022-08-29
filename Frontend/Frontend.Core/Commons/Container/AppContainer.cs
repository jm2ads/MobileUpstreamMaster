using Frontend.Azure.RestServices;
using Frontend.Business.Commons;
using Frontend.Business.IAzureRestServices;
using Frontend.Core.Areas.AboutUs.IViewModels;
using Frontend.Core.Areas.AboutUs.ViewModels;
using Frontend.Core.Areas.Home.IViewModels;
using Frontend.Core.Areas.Login.IViewModels;
using Frontend.Core.Areas.Login.ViewModel;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.IViewModels;
using Frontend.Core.ViewModels;
using Frontend.Data.Commons;
using Frontend.Data.Database;
using Frontend.IServices.IServices;
using Frontend.Services.Services;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Xamarin.Forms;

namespace Frontend.Core.Commons.Container
{
    public static class AppContainer
    {
        public static IUnityContainer container;

        public static void Start()
        {
            Init();
            RegisterCore();
            RegisterRepositories();
            RegisterDatabaseConnection();
            RegisterServices();
        }

        private static void Init()
        {
            container = new UnityContainer();
        }

        private static void RegisterCore()
        {
            container.RegisterType<ISideBarViewModel, SideBarMenuViewModel>();
            container.RegisterType<ILoginViewModel, LoginViewModel>();
            container.RegisterType<IHomeViewModel, HomeViewModel>();
            container.RegisterType<IAppViewModel, AppViewModel>();
            container.RegisterType<IAboutUsViewModel, AboutUsViewModel>();

            container.RegisterType<INavigationService, MasterDetailNavigation>(new PerThreadLifetimeManager());
            container.RegisterType<IDisplayAlertService, DisplayAlertService>(new PerThreadLifetimeManager());
        }

        public static void RegisterRepositories()
        {
            //container.RegisterType<ISettingRepository, SettingRepository>();
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
        }

        private static void RegisterDatabaseConnection()
        {
            var fileHelper = DependencyService.Get<IFileHelper>();
            var sqlLiteAsyncConnection = new SQLite.SQLiteAsyncConnection(fileHelper.GetLocalFilePath("YPF_MA.db"));
            container.RegisterType(typeof(Database<>), new InjectionConstructor(sqlLiteAsyncConnection));
        }

        private static void RegisterServices()
        {
            container.RegisterType<ISettingsService, SettingService>();
            
            //Azure Services
            container.RegisterType<ICentroAzureRestService, CentroAzureRestService>();
            container.RegisterType<IAlmacenAzureRestService, AlmacenAzureRestService>();
            container.RegisterType<IClaseDeValoracionAzureRestService, ClaseDeValoracionAzureRestService>();
            container.RegisterType<IMaterialAzureRestService, MaterialAzureRestService>();
            container.RegisterType<IUserAzureRestService, UsuarioAzureRestService>();
        }
    }
}
