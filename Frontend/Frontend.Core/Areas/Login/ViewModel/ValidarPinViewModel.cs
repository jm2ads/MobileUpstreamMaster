using System;
using System.Windows.Input;
using Frontend.Business.Usuarios;
using Frontend.Commons.Bootstrapper;
using Frontend.Commons.Commons;
using Frontend.Core.Areas.Login.IViewModels;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Validations;
using Frontend.Core.Resources;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Login.ViewModel
{
    public class ValidarPinViewModel : BaseViewModel, IValidarPinViewModel
    {
        private readonly IDisplayAlertService alertService;
        private readonly IUsuarioService usuarioService;
        private readonly ISettingsService settingService;
        private readonly INavigationService navigationService;

        public Usuario Usuario { get; set; }
        public ICommand ValidarPinCommand { get; set; }
        public ICommand OlvidePinCommand { get; set; }

        private ValidatableObject<string> pin;
        public ValidatableObject<string> Pin
        {
            get { return pin; }
            set { SetProperty(ref pin, value); }
        }

        private string idRed;
        public string IdRed
        {
            get { return idRed; }
            set { SetProperty(ref idRed, value); }
        }

        public ValidarPinViewModel(IDisplayAlertService alertService, IUsuarioService usuarioService, ISettingsService settingService, 
            INavigationService navigationService)
        {
            this.alertService = alertService;
            this.usuarioService = usuarioService;
            this.settingService = settingService;
            this.navigationService = navigationService;

            Init();
        }

        private void Init()
        {
            Pin = new ValidatableObject<string>();
            ValidarPinCommand = new Command(ValidarPin);
            OlvidePinCommand = new Command(OlvidePin);
            Usuario = navigationService.GetNavigationParams<ValidarPinView>() as Usuario;
            IdRed = Usuario.IdRed;

            AddValidations();
        }

        private void OlvidePin(object obj)
        {
            navigationService.PushFromAsync<IngresoUsuarioView, LoginView>(Usuario.IdRed);
        }

        private async void ValidarPin(object obj)
        {
            if (!Validate())
            {
                return;
            }

            if (!await usuarioService.ValidatePin(Usuario, Pin.Value))
            {
                Toast.ShowMessage("Los valores usuario / PIN no coinciden o su token ha expirado, por favor, vuelva a ingresarlos");
            }
            else
            {
                var setting = await settingService.GetWithChildren();
                setting.UsuarioActivoId = Usuario.Id;
                await settingService.Update(setting);

                if (!settingService.ValidateInitialSync(setting))
                {
                    alertService.Show("Última sincronización", setting.LastSync.ToString("dd/MM/yyyy hh:mm"), "Aceptar");
                }
                Application.Current.MainPage = ContainerManager.Resolve<MasterMenulview>();
            }
            
        }

        private void AddValidations()
        {
            Pin.Validations.Clear();
            Pin.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = MessageText.FieldRequired
            });
        }

        private bool Validate()
        {
            bool isValidPin = ValidatePin();
            return isValidPin;
        }

        public bool ValidatePin()
        {
            return Pin.Validate();
        }

    }
}
