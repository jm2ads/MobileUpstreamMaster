using Frontend.Business.Centros;
using Frontend.Business.Commons;
using Frontend.Commons.Commons;
using Frontend.Commons.Commons.Errors;
using Frontend.Commons.Enums;
using Frontend.Core.Areas.Login.IViewModels;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Exceptions;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.Commons.Validations;
using Frontend.Core.Resources;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Login.ViewModel
{
    public class LoginViewModel : BaseViewModel, ILoginViewModel
    {

        private ValidatableObject<string> password;
        public ValidatableObject<string> Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private ValidatableObject<string> username;
        public ValidatableObject<string> Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        private ValidatableObject<Centro> centro;
        public ValidatableObject<Centro> Centro
        {
            get { return centro; }
            set { SetProperty(ref centro, value); }
        }

        private ObservableRangeCollection<Centro> listaCentros;
        public ObservableRangeCollection<Centro> ListaCentros
        {
            get { return listaCentros; }
            set { SetProperty(ref listaCentros, value); }
        }

        public int CustomStyle { get; set; }

        public ICommand LoginCommand { get; set; }
        public ICommand SyncCentroCommand { get; set; }

        private readonly INetworkConnection networkConnection;
        private readonly ExceptionViewHandler exceptionViewHandler;
        private readonly IDisplayAlertService alertService;
        private readonly ISettingsService settingService;
        private readonly INavigationService navigationService;
        private readonly IUsuarioService usuarioService;
        private readonly ICentroService centroService;
        private readonly ISyncService syncService;
        private readonly IDatabaseManager databaseManager;

        public LoginViewModel(ExceptionViewHandler exceptionViewHandler, IDisplayAlertService alertService, ISettingsService settingService,
            INavigationService navigationService, IUsuarioService usuarioService, ICentroService centroService, ISyncService syncService,
            IDatabaseManager databaseManager)
        {
            this.exceptionViewHandler = exceptionViewHandler;
            this.alertService = alertService;
            this.settingService = settingService;
            this.navigationService = navigationService;
            this.usuarioService = usuarioService;
            this.centroService = centroService;
            this.syncService = syncService;
            this.databaseManager = databaseManager;
            networkConnection = DependencyService.Get<INetworkConnection>();

            Init();
        }

        private void Init()
        {
            Title = "";
            LoginCommand = new Command(async () => await DoLogin());
            SyncCentroCommand = new Command(SyncCentro);
            username = new ValidatableObject<string>();
            password = new ValidatableObject<string>();
            Centro = new ValidatableObject<Centro>();
            CustomStyle = (int)CustomStyleEnum.White;
            ListaCentros = new ObservableRangeCollection<Centro>();
            var idRed = navigationService.GetNavigationParams<LoginView>() as string;
            Username.Value = idRed;

            InitValidations();

            SyncCentroCommand.Execute(null);
        }

        private async void SyncCentro(object obj)
        {
            await StartSpinner();

            networkConnection.CheckConnection();
            if (!networkConnection.IsConnected)
            {
                alertService.Show("Sin Conexión", "Intente obtener una conexión estable de datos 3G, 4G o WIFI para poder realizar la validación", "Cerrar");
                navigationService.PopAsync<LoginView>();
                await StopSpinner();
                return;
            }
            else
            {
                try
                {
                    await FillCentros();
                }
                catch (Exception e)
                {
                    exceptionViewHandler.Handle(e, ApplicationMessages.ErrorInterno);
                    navigationService.PopAsync<LoginView>();
                }
            }


            await StopSpinner();
        }

        private async Task FillCentros()
        {
            var listCentros = await centroService.GetAllByIdRed(Username.Value);
            ListaCentros.AddRange(listCentros.OrderBy(x => x.Codigo).ToList());
            if (ListaCentros.Count == 0)
            {
                alertService.Show("Error", "El usuario ingresado no tiene centros asociados.", "Cerrar");
                navigationService.PopAsync<LoginView>();
                return;
            }
        }


        public void ValidateUsernameInput()
        {
            username.Validate();
        }

        public void ValidatePasswordInput()
        {
            password.Validate();
        }

        public void ValidateCentroInput()
        {
            Centro.Validate();
        }

        private void InitValidations()
        {
            var isNotNullOrEmptyRule = new IsNotNullOrEmptyRule<string>() { ValidationMessage = MessageText.FieldRequired };
            password.Validations.Add(isNotNullOrEmptyRule);
            username.Validations.Add(isNotNullOrEmptyRule);


            Centro.Validations.Clear();
            Centro.Validations.Add(new IsNotNullOrEmptyRule<Centro>
            {
                ValidationMessage = "El centro es obligatorio."
            });
        }

        private async Task DoLogin()
        {
            username.Validate();
            password.Validate();
            Centro.Validate();
            if (!password.IsValid || !username.IsValid || !Centro.IsValid) return;
            networkConnection.CheckConnection();
            if (!networkConnection.IsConnected)
            {
                alertService.Show("Sin Conexión", "Intente obtener una conexión estable de datos 3G, 4G o WIFI para poder realizar la validación", "Cerrar");
                return;
            }
            await Login();
        }

        private async Task Login()
        {
            await StartSpinner();
            try
            {
                await usuarioService.DoLogin(username.Value, password.Value);
                var setting = await settingService.GetWithChildren();
                if (setting.CentroActivoId != Centro.Value.Id)
                {
                    setting.LastSync = ApplicationConstants.DefaultDateSync;
                    await databaseManager.ResetDB();
                }
                await centroService.ReplaceAll(ListaCentros.ToList());
                setting.CentroActivoId = Centro.Value.Id;
                setting.CentroActivo = Centro.Value;
                await settingService.Update(setting);
                await usuarioService.UpdateFuncionalidades(setting.UsuarioActivo, setting.CentroActivoId);

                navigationService.PushAsync<LoginView, CrearPinView>(setting.UsuarioActivo);
            }
            catch (BusinessException businessException)
            {
                alertService.Show("Autenticación", businessException.Mensaje, "Cerrar");
            }

            await StopSpinner();
        }
    }
}
