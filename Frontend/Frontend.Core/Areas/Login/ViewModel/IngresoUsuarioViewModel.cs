using Frontend.Business.Usuarios;
using Frontend.Commons.Commons;
using Frontend.Core.Areas.Login.IViewModels;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Exceptions;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Validations;
using Frontend.Core.Resources;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Login.ViewModel
{
    public class IngresoUsuarioViewModel : BaseViewModel, IIngresoUsuarioViewModel
    {
        private readonly IDisplayAlertService displayAlertService;
        private readonly INavigationService navigationService;
        private readonly IUsuarioService usuarioService;
        private readonly ISettingsService settingsService;
        private readonly ExceptionViewHandler exceptionViewHandler;
        private readonly INetworkConnection networkConnection;

        private Usuario usuario { get; set; }

        public ICommand ValidarUsuarioCommand { get; set; }

        private ValidatableObject<string> idRed;
        public ValidatableObject<string> IdRed
        {
            get { return idRed; }
            set { SetProperty(ref idRed, value); }
        }

        public IngresoUsuarioViewModel(IDisplayAlertService displayAlertService, INavigationService navigationService, IUsuarioService usuarioService, ISettingsService settingsService,
            ExceptionViewHandler exceptionViewHandler)
        {
            this.displayAlertService = displayAlertService;
            this.navigationService = navigationService;
            this.usuarioService = usuarioService;
            this.settingsService = settingsService;
            this.exceptionViewHandler = exceptionViewHandler;
            this.networkConnection = DependencyService.Get<INetworkConnection>();

            Init();
        }

        private void Init()
        {
            Title = "";
            ValidarUsuarioCommand = new Command(ValidarUsuario);

            IdRed = new ValidatableObject<string>();

            InitValidations();

            GetUsuario();
        }

        private async void GetUsuario()
        {
            var settings = await settingsService.GetWithChildren();
            if (settings.UsuarioActivo != null)
            {
                IdRed.Value = settings.UsuarioActivo.IdRed;
            }
        }

        private async void ValidarUsuario()
        {
            await StartSpinner("Validando...");

            if (!Validate())
            {
                await StopSpinner();
                return;
            }

            usuario = await usuarioService.GetByIdRed(IdRed.Value.Trim().ToUpper());

            if (usuario == null)
            {
                await StopSpinner();
                navigationService.PushAsync<IngresoUsuarioView, LoginView>(IdRed.Value.Trim().ToUpper());
            }
            else
            {
                await ValidateUserSettings();
            }
        }

        private async Task ValidateUserSettings()
        {
            networkConnection.CheckConnection();
            try
            {
                if (networkConnection.IsConnected)
                {
                    if (await usuarioService.ValidateToken(usuario))
                    {
                        var settings = await settingsService.GetWithChildren();
                        settings.UsuarioActivoId = usuario.Id;
                        settings.UsuarioActivo = usuario;
                        await settingsService.Update(settings);
                        await usuarioService.UpdateFuncionalidades(usuario, settings.CentroActivoId);
                        if (usuario.Funcionalidades.Count == 0)
                        {
                            Toast.ShowMessage("El usuario ingresado no posee permisos en el centro activo.");
                            await StopSpinner();
                            navigationService.PushAsync<IngresoUsuarioView, LoginView>(usuario.IdRed);
                            return;
                        }

                        await StopSpinner();
                        navigationService.PushAsync<IngresoUsuarioView, ValidarPinView>(usuario);
                    }
                    else
                    {
                        await StopSpinner();
                        navigationService.PushAsync<IngresoUsuarioView, LoginView>(usuario.IdRed);
                    }

                }
                else
                {
                    await StopSpinner();
                    NextStep(await usuarioService.HasToken(IdRed.Value.Trim().ToUpper()));
                }
            }
            catch (Exception e)
            {
                exceptionViewHandler.Handle(e, ApplicationMessages.ErrorInterno, idRed: usuario.IdRed);
            }
            finally
            {
                await StopSpinner();
            }
        }

        private void NextStep(bool userTokenValid)
        {
            if (userTokenValid)
            {
                if (usuario.Funcionalidades.Count == 0)
                {
                    Toast.ShowMessage("El usuario ingresado no posee permisos en el centro activo.");
                    navigationService.PushAsync<IngresoUsuarioView, LoginView>(usuario.IdRed);
                    return;
                }
                navigationService.PushAsync<IngresoUsuarioView, ValidarPinView>(usuario);
            }
            else
            {
                navigationService.PushAsync<IngresoUsuarioView, CrearPinView>(usuario);
            }
        }

        private void InitValidations()
        {
            IdRed.Validations.Clear();
            IdRed.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = MessageText.FieldRequired
            });
        }

        private bool Validate()
        {
            bool isValidIdRed = ValidateIdRed();
            return isValidIdRed;
        }

        private bool ValidateIdRed()
        {
            return IdRed.Validate();
        }
    }
}
