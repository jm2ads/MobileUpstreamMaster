
using System;
using Frontend.Commons.Bootstrapper;
using Frontend.Commons.Commons;
using Frontend.Commons.Commons.Errors;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Views;
using Xamarin.Forms;

namespace Frontend.Core.Commons.Exceptions
{
    public class ExceptionViewHandler
    {
        private readonly IDisplayAlertService alertService;
        private readonly INavigationService navigationService;
        private readonly IToastControl toastControl;

        public ExceptionViewHandler(IDisplayAlertService alertService, INavigationService navigationService)
        {
            this.alertService = alertService;
            this.navigationService = navigationService;
            toastControl = DependencyService.Get<IToastControl>();
        }

        /// <summary>
        /// Maneja excepciones y muestra un mensaje custom de usuario si existe alguna excepcion no controlada.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="defaultMessage"></param>
        public void Handle(Exception e, string defaultMessage, string idRed = null)
        {
            if (e is AuthenticationException)
            {
                var excepction = e as AuthenticationException;
                if (excepction.Codigo == BusinessErrorCode.ServicioSeguridadNoDisponible)
                {
                    alertService.Show(ApplicationMessages.AtentiontModalTitle, excepction.Mensaje, ApplicationMessages.Close);
                }
                else
                {
                    navigationService.PushAsync<IngresoUsuarioView, LoginView>(idRed);
                    toastControl.ShowMessage(excepction.Mensaje);
                }
                return;
            }
            if (e is BusinessException)
            {
                var excepction = e as BusinessException;
                alertService.Show(ApplicationMessages.AtentiontModalTitle, excepction.Mensaje, ApplicationMessages.Close);
                return;
            }
            toastControl.ShowMessage(defaultMessage);
        }
    }
}
