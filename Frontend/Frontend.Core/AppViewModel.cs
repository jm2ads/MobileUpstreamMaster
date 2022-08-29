using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Frontend.Commons.Bootstrapper;
using Frontend.Commons.Commons;
using Frontend.Core.Commons.CustomRenders;
using Frontend.Core.Commons.Exceptions;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.Core.IViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using Xamarin.Forms;

namespace Frontend.Core
{
    public class AppViewModel: IAppViewModel
    {
        private readonly IUsuarioService usuarioService;
        private readonly ISyncService syncService;
        private readonly INetworkConnection networkConnection;
        private readonly ExceptionViewHandler exceptionViewHandler;

        public ICommand ValidateUserSettingsCommand { get; set; }
        
        private readonly MasterMenulview masterMenulview;

        public AppViewModel(IUsuarioService usuarioService, ISyncService syncService, ExceptionViewHandler exceptionViewHandler)
        {
            this.usuarioService = usuarioService;
            this.syncService = syncService;
            this.exceptionViewHandler = exceptionViewHandler;
            this.networkConnection = networkConnection = DependencyService.Get<INetworkConnection>();


            Application.Current.MainPage = new CustomNavigationPage(ContainerManager.Resolve<IngresoUsuarioView>());
        }
    }
}
