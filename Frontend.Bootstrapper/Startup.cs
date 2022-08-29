using Frontend.Azure.RestServices;
using Frontend.Business.Attributes;
using Frontend.Business.Commons;
using Frontend.Business.Synchronizer;
using Frontend.Commons.Bootstrapper;
using Frontend.Commons.Commons;
using Frontend.Core;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.IViewModels;
using Frontend.Core.ViewModels;
using Frontend.Data.Commons;
using Frontend.Data.DataAccess;
using Frontend.Data.Database;
using Frontend.Services.Services;
using SQLite;
using System;
using System.Linq;
using System.Reflection;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Xamarin.Forms;

namespace Frontend.Bootstrapper
{
    public class Startup : IBootstraperStartup
    {
        private static IUnityContainer container;

        public Startup()
        {
            container = ContainerManager.Container;
        }

        public void ConfigureContainer()
        {
            RegisterRepositories();
            RegisterDatabaseConnection();
            RegisterServices();
            RegisterCore();
            ConfigureDatabase();
        }

        private static void Register(Type type, string endsWith, Type lifetimeManagerType)
        {
            var assembly = type.GetTypeInfo().Assembly;
            var types = assembly.DefinedTypes.Where(t => t.IsClass && !t.IsGenericType && t.Name.EndsWith(endsWith));
            types.ToList().ForEach(aType => aType.ImplementedInterfaces.ToList().ForEach(typeInterface => container.RegisterType(typeInterface, aType.AsType(), Activator.CreateInstance(lifetimeManagerType) as LifetimeManager)));
        }

        private void RegisterCore()
        {
            container.RegisterType<INavigationService, MasterDetailNavigation>(new PerThreadLifetimeManager());
            container.RegisterType<IDisplayAlertService, DisplayAlertService>(new PerThreadLifetimeManager());
            container.RegisterType<IDatabaseManager, DatabaseManager>(new PerThreadLifetimeManager());

            Register(typeof(HomeViewModel), "ViewModel", typeof(PerResolveLifetimeManager));
            Register(typeof(HomeView), "View", typeof(PerResolveLifetimeManager));


            container.RegisterType<ISideBarViewModel, SideBarMenuViewModel>();
            container.RegisterType<IAppViewModel, AppViewModel>();
        }

        private void RegisterRepositories()
        {
            Register(typeof(AplicacionUsuarioRepository), "Repository", typeof(PerThreadLifetimeManager));
            container.RegisterType(typeof(IRepository<>), (typeof(Repository<>)));
        }

        private void RegisterDatabaseConnection()
        {
            container.RegisterType<IConnectionFactory, ConnectionFactory>();
        }

        private void RegisterServices()
        {
            Register(typeof(UsuarioService), "Service", typeof(PerThreadLifetimeManager));

            ////Azure Services 
            Register(typeof(UsuarioAzureRestService), "AzureRestService", typeof(PerThreadLifetimeManager));
        }

        private void ConfigureDatabase()
        {
            var databaseManager = (IDatabaseManager)Resolve(typeof(IDatabaseManager));
            databaseManager.InitDB();
        }

        public object Resolve(Type objectType)
        {
            return container.Resolve(objectType);
        }
    }
}
