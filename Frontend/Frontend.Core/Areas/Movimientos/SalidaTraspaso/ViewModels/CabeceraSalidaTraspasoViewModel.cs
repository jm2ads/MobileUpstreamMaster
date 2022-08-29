using Frontend.Business.Movimientos.SalidasInternas;
using Frontend.Business.Settings;
using Frontend.Business.Synchronizer;
using Frontend.Core.Areas.Movimientos.SalidaTraspaso.IViewModels;
using Frontend.Core.Areas.Views;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Validations;
using Frontend.Core.ViewModels;
using Frontend.IServices.IServices;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Movimientos.SalidaTraspaso.ViewModels
{
    public class CabeceraSalidaTraspasoViewModel : BaseViewModel, ICabeceraSalidaTraspasoViewModel
    {
        public ICommand GoToPosicionesCommand { get; set; }

        private SalidaInterna salida;
        public SalidaInterna Salida
        {
            get { return salida; }
            set
            {
                SetProperty(ref salida, value);
            }
        }

        private string centroEmisor;
        public string CentroEmisor
        {
            get { return centroEmisor; }
            set
            {
                SetProperty(ref centroEmisor, value);
            }
        }
        private string centroReceptor;
        public string CentroReceptor
        {
            get { return centroReceptor; }
            set
            {
                SetProperty(ref centroReceptor, value);
            }
        }

        private Setting setting { get; set; }

        private string claseDeMovimiento;
        public string ClaseDeMovimiento
        {
            get { return claseDeMovimiento; }
            set { SetProperty(ref claseDeMovimiento, value); }
        }

        private ValidatableObject<DateTime> fechaContabilizacion;
        public ValidatableObject<DateTime> FechaContabilizacion
        {
            get { return fechaContabilizacion; }
            set
            {
                SetProperty(ref fechaContabilizacion, value);
            }
        }

        private ValidatableObject<DateTime> fechaDocumento;
        public ValidatableObject<DateTime> FechaDocumento
        {
            get { return fechaDocumento; }
            set
            {
                SetProperty(ref fechaDocumento, value);
            }
        }

        #region Service constructor
        private readonly IDisplayAlertService displayAlertService;
        private readonly INavigationService navigationService;
        private readonly ISalidaInternaService salidaInternaService;
        private readonly ISettingsService settingsService;

        public CabeceraSalidaTraspasoViewModel(IDisplayAlertService displayAlertService, ISalidaInternaService salidaInternaService, INavigationService navigationService, ISettingsService settingsService)
        {
            this.navigationService = navigationService;
            this.displayAlertService = displayAlertService;
            this.salidaInternaService = salidaInternaService;
            this.settingsService = settingsService;
            Init();
        }
        #endregion
        private async void Init()
        {
            GoToPosicionesCommand = new Command(GoToPosiciones);
            salida = navigationService.GetNavigationParams<CabeceraSalidaTraspasoView>() as SalidaInterna;
            FechaDocumento = new ValidatableObject<DateTime>();
            FechaContabilizacion = new ValidatableObject<DateTime>();
            Title = "Pedido " + salida.NumeroPedido;
            await GetPedido();
        }

        private async Task GetPedido()
        {
            setting = await settingsService.GetWithChildren();
            CentroEmisor = setting.CentroActivo.Codigo;
            CentroReceptor = salida.CentroReceptor.Codigo;
            FechaDocumento.Value = DateTime.Now;
            FechaContabilizacion.Value = DateTime.Now;
            ClaseDeMovimiento = salida.ClaseDeMovimientoCodigo;
        }

        private async void GoToPosiciones()
        {
            await CompletarSalida();
            navigationService.PushAsync<CabeceraDeSalidaInternaView, ListadoPosicionesSalidaTraspasoView>(salida);
        }

        private async Task CompletarSalida()
        {
            salida.ClaseDeMovimientoCodigo = claseDeMovimiento;
            salida.FechaContabilizacion = FechaContabilizacion.Value;
            salida.FechaDocumento = FechaDocumento.Value;
            await salidaInternaService.Update(salida, SyncState.Updated);
        }
    }
}
