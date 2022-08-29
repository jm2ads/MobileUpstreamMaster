using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Exceptions;
using Frontend.Core.IViewModels;
using Frontend.IServices.IServices;
using Xamarin.Forms;

namespace Frontend.Core.ViewModels
{
    public class SettingsViewModel : BaseViewModel, ISettingsViewModel
    {
        public ICommand SincronizeCommand { get; set; }

        private readonly ISyncService syncService;

        private readonly ExceptionViewHandler exceptionViewHandler;

        private readonly IDisplayAlertService alertService;

        public ICommand DisplayMessageCommand { get; set; }

        public ICommand DisplayMessage3Command { get; set; }

        public bool syncServer;
        public bool SyncServer
        {
            get { return syncServer; }
            set
            {
                alertService.Show("Atencíon", "Funcionalidad no disponible","Cerrar");
                SetProperty(ref syncServer, value);
            } 
        }

        public SettingsViewModel(ISyncService syncService, 
                                 ExceptionViewHandler exceptionViewHandler,
                                 IDisplayAlertService alertService)
        {
            this.exceptionViewHandler = exceptionViewHandler;
            this.alertService = alertService;
            this.syncService = syncService;
            SincronizeCommand = new Command(async ()=> await Syncronize());
            DisplayMessageCommand = new Command(DisplayMessage);
            DisplayMessage3Command = new Command(DisplayMessage3);
        }

        async Task Syncronize()
        {
            IsBusy = true;
            try
            {
                await syncService.SyncData();
                IsBusy = false;
                Toast.ShowMessage("Sincronizacion terminada");
            }
            catch (Exception e)
            {
                exceptionViewHandler.Handle(e, "No se pudo completar la sincronizacion, intente de nuevo");
                IsBusy = false;
            }
        }

        void DisplayMessage()
        {
            Toast.ShowMessage("Accion de toolbar secundario");
        }

        void DisplayMessage3()
        {
            Toast.ShowMessage("Accion de toolbar secundario 3");
        }
    }
}
