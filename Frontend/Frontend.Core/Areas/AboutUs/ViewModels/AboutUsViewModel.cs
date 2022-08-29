using Frontend.Business.Commons;
using Frontend.Commons.Commons;
using Frontend.Core.Areas.AboutUs.IViewModels;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.AboutUs.ViewModels
{
    public class AboutUsViewModel : BaseViewModel, IAboutUsViewModel
    {
        private INavigationService navigationService;
        private readonly ISettingsService settingsService;
        private readonly IDatabaseManager databaseManager;
        private readonly IUsuarioService usuarioService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly INetworkConnection networkConnection;

        public string VersionNumber { get; set; }

        public string BuildNumber { get; set; }

        public string Enviroment { get; set; }

        public string EmailInfo { get; set; }

        public string ApplicationName { get; set; }

        private string applicationSecret;

        private int ClickCounter = 0;

        public string ApplicationSecret
        {
            get { return applicationSecret.Substring(0, 6); }
            set { applicationSecret = value; }
        }


        public string LinkMailDescription
        {
            get
            {
                return "Soporte " + ApplicationName;
            }
        }

        public ICommand OpenEmailAppCommand { get; set; }
        public ICommand RestablecerDatosCommand { get; set; }

        public AboutUsViewModel(INavigationService navigationService, ISettingsService settingsService, IDatabaseManager databaseManager,
            IUsuarioService usuarioService, IDisplayAlertService displayAlertService)
        {
            this.navigationService = navigationService;
            this.settingsService = settingsService;
            this.databaseManager = databaseManager;
            this.usuarioService = usuarioService;
            this.displayAlertService = displayAlertService;
            networkConnection = DependencyService.Get<INetworkConnection>();
            VersionNumber = DependencyService.Get<IApplicationInformation>().GetVersionNumber();
            BuildNumber = DependencyService.Get<IApplicationInformation>().GetBuildNumber();
            Enviroment = ApplicationConstants.Enviroment;
            EmailInfo = ApplicationConstants.EmailInfo;
            ApplicationName = ApplicationConstants.ApplicationName;
            ApplicationSecret = ApplicationConstants.ApplicationSecretAndroid;

            OpenEmailAppCommand = new Command(OpenEmailApp);
            RestablecerDatosCommand = new Command(RestablecerDatos);
        }

        private async void RestablecerDatos(object obj)
        {
            ClickCounter++;
            if (ClickCounter == 3)
            {
                ClickCounter = 0;
                var respuesta = await displayAlertService.Show("Restablecer datos", "Se perderá el trabajo no sincronizado, ¿Desea continuar?", "Continuar", "Cancelar");
                if (respuesta)
                {
                    await StartSpinner();
                    networkConnection.CheckConnection();
                    if (!networkConnection.IsConnected)
                    {
                        displayAlertService.Show("Sin Conexión", "Intente obtener una conexión estable de datos 3G, 4G o WIFI para poder realizar la validación", "Cerrar");
                        await StopSpinner();
                        return;
                    }
                    var setting = await settingsService.GetWithChildren();
                    setting.LastSync = ApplicationConstants.DefaultDateSync;
                    await databaseManager.ResetDB();
                    await settingsService.Update(setting);
                    await usuarioService.UpdateFuncionalidades(setting.UsuarioActivo, setting.CentroActivoId);
                    navigationService.PushFromRootAsync<HomeView>(setting.UsuarioActivo);
                    await StopSpinner();
                }
            }
            else
            {
                Toast.ShowMessage(string.Format("Restablecer datos {0} de 3 intentos.", ClickCounter));
            }
        }

        private void OpenEmailApp()
        {
            var version = VersionNumber;
            var release = BuildNumber;
            var enviroment = Enviroment;
            var email = EmailInfo;
            var appName = ApplicationName;

            var url = DependencyService.Get<IUrlEmailConverter>().GetUrl(email, appName, version, release, enviroment);
            try
            {
                Device.OpenUri(new Uri(url));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
