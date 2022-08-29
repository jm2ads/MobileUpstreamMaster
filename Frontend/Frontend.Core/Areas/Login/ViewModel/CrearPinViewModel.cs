using Frontend.Business.Usuarios;
using Frontend.Core.Areas.Login.IViewModels;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Validations;
using Frontend.Core.Resources;
using Frontend.Core.ViewModels;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Login.ViewModel
{
    public class CrearPinViewModel : BaseViewModel, ICrearPinViewModel
    {
        private readonly IDisplayAlertService alertService;
        private readonly IUsuarioService usuarioService;
        private readonly INavigationService navigationService;
        private readonly INetworkConnection networkConnection;

        public ICommand CrearPinCommand { get; set; }

        private string idRed;
        public string IdRed
        {
            get { return idRed; }
            set { SetProperty(ref idRed, value); }
        }

        private ValidatableObject<string> pin;
        public ValidatableObject<string> Pin
        {
            get { return pin; }
            set { SetProperty(ref pin, value); }
        }

        private ValidatableObject<string> pinConfirmado;
        public ValidatableObject<string> PinConfirmado
        {
            get { return pinConfirmado; }
            set { SetProperty(ref pinConfirmado, value); }
        }

        private Usuario usuario;

        public CrearPinViewModel(IDisplayAlertService alertService, IUsuarioService usuarioService, INavigationService navigationService)
        {
            this.alertService = alertService;
            this.usuarioService = usuarioService;
            this.navigationService = navigationService;
            networkConnection = DependencyService.Get<INetworkConnection>();

            Init();
        }

        private void Init()
        {
            Pin = new ValidatableObject<string>();
            PinConfirmado = new ValidatableObject<string>();

            CrearPinCommand = new Command(CrearPin);

            usuario = navigationService.GetNavigationParams<CrearPinView>() as Usuario;
            IdRed = usuario.IdRed;

            AddValidations();
        }

        private async void CrearPin(object obj)
        {
            if (!CheckConnection())
            {
                alertService.Show("Sin Conexion", "Intente obtener una conexion estable de datos 3G, 4G o WIFI para poder realizar la validación", "Cerrar");
                return;
            }

            if (!Validate())
            {
                return;
            }

            if (Pin.Value != PinConfirmado.Value)
            {
                Toast.ShowMessage("Los valores del PIN no coinciden o son inválidos, por favor, vuelva a ingresarlos");
                return;
            }

            CompleteUsuario();

            await usuarioService.Update(usuario);

            navigationService.PushAsync<CrearPinView, ValidarPinView>(usuario);
        }

        private void CompleteUsuario()
        {
            usuario.Pin = Pin.Value;
        }

        private void AddValidations()
        {
            Pin.Validations.Clear();
            Pin.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = MessageText.FieldRequired
            });

            PinConfirmado.Validations.Clear();
            PinConfirmado.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = MessageText.FieldRequired
            });
        }

        private bool Validate()
        {
            bool isValidPin = ValidatePin();
            bool isValidPinConfirmado = ValidatePinConfirmado();
            return isValidPin && isValidPinConfirmado;
        }

        public bool ValidatePin()
        {
            return Pin.Validate();
        }

        public bool ValidatePinConfirmado()
        {
            return PinConfirmado.Validate();
        }

        public bool CheckConnection()
        {
            networkConnection.CheckConnection();
            return networkConnection.IsConnected;
        }
    }
}
