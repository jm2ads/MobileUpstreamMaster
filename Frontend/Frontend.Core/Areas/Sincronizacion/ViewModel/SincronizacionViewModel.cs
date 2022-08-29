using Frontend.Core.Areas.Sincronizacion.IViewModel;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.Core.ViewModels;
using Frontend.IServices.IServices;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Sincronizacion.ViewModel
{
    public class SincronizacionViewModel : BaseViewModel, ISincronizacionViewModel
    {
        private readonly ISyncService syncService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly INetworkConnection networkConnection;
        public ICommand SyncCommand { get; set; }
        public ICommand SyncParcialCommand { get; set; }

        public SincronizacionViewModel(ISyncService syncService, IDisplayAlertService displayAlertService)
        {
            this.syncService = syncService;
            this.displayAlertService = displayAlertService;
            SyncCommand = new Command(async () => await Sync());
            SyncParcialCommand = new Command(async () => await SyncParcial());

            networkConnection = DependencyService.Get<INetworkConnection>();
        }

        public  async Task Sync()
        {
            IsBusy = true;
            try
            {
                networkConnection.CheckConnection();
                if (!networkConnection.IsConnected)
                {
                    displayAlertService.Show("Sin Conexion", "Intente obtener una conexion estable de datos 3G, 4G o WIFI para poder realizar la sincronización", "Cerrar");
                    IsBusy = false;
                    return;
                }
                await this.syncService.DropData();
                await this.syncService.SyncData();
                IsBusy = false;
                Toast.ShowMessage("Sincronización terminada");
            }
            catch (Exception e)
            {
                Toast.ShowMessage("No se pudo completar la sincronización, intente de nuevo");
                IsBusy = false;
            }
        }

        public async Task SyncParcial()
        {
            IsBusy = true;
            try
            {
                networkConnection.CheckConnection();
                if (!networkConnection.IsConnected)
                {
                    displayAlertService.Show("Sin Conexion", "Intente obtener una conexion estable de datos 3G, 4G o WIFI para poder realizar la sincronización", "Cerrar");
                    IsBusy = false;
                    return;
                }
                
                await this.syncService.SyncDataParcial();
                IsBusy = false;
                Toast.ShowMessage("Sincronización terminada");
            }
            catch (Exception e)
            {
                Toast.ShowMessage("No se pudo completar la sincronización, intente de nuevo");
                IsBusy = false;
            }
        }
    }
}
