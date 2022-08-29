using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Ingresos;
using Frontend.Business.Movimientos.SalidasInternas;
using Frontend.Business.Synchronizer;
using Frontend.Business.Settings;
using Frontend.Core.Areas.Movimientos.SalidasInternas.IViewModels;
using Frontend.Core.Areas.Views;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Validations;
using Frontend.Core.Resources;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Movimientos.SalidasInternas.ViewModels
{
    public class CabeceraDeSalidaInternaViewModel : BaseViewModel, ICabeceraDeSalidaInternaViewModel
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

        private string centro;
        public string Centro
        {
            get { return centro; }
            set
            {
                SetProperty(ref centro, value);
            }
        }
        private IDictionary<int, string> claseDeMovimientoSalida;

        private Setting setting { get; set; }

        private string claseDeMovimiento;
        public string ClaseDeMovimiento
        {
            get { return claseDeMovimiento; }
            set { SetProperty(ref claseDeMovimiento, value); }
        }

        private string _fechaContabilizacion;
        public string FechaContabilizacion
        {
            get { return _fechaContabilizacion; }
            set
            {
                SetProperty(ref _fechaContabilizacion, value);
            }
        }

        private string _fechaDocumento;
        public string FechaDocumento
        {
            get { return _fechaDocumento; }
            set
            {
                SetProperty(ref _fechaDocumento, value);
            }
        }

        #region Service constructor
        private readonly IDisplayAlertService displayAlertService;
        private readonly INavigationService navigationService;
        private readonly ISalidaInternaService salidaInternaService;
        private readonly ISettingsService settingsService;

        public CabeceraDeSalidaInternaViewModel(IDisplayAlertService displayAlertService, ISalidaInternaService salidaInternaService, INavigationService navigationService, ISettingsService settingsService)
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
            salida = navigationService.GetNavigationParams<CabeceraDeSalidaInternaView>() as SalidaInterna;
            Title = "Pedido " + salida.NumeroPedido;
            await GetPedido();
        }

        private async Task GetPedido()
        {
            setting = await settingsService.GetWithChildren();
            Centro = setting.CentroActivo.Codigo;
            FechaDocumento = DateTime.Now.ToString("dd/MM/yyyy");
            FechaContabilizacion = DateTime.Now.ToString("dd/MM/yyyy");
            ClaseDeMovimiento = salida.ClaseDeMovimientoCodigo;
        }

        private async void GoToPosiciones()
        {
            await CompletarSalida();
            navigationService.PushAsync<CabeceraDeSalidaInternaView, ListadoPosicionesSalidaInternaView>(salida);
        }

        private async Task CompletarSalida()
        {
            salida.ClaseDeMovimientoCodigo = claseDeMovimiento;
            salida.FechaContabilizacion = DateTime.Now;
            salida.FechaDocumento = DateTime.Now;
            await salidaInternaService.Update(salida, SyncState.Updated);
        }
    }
}
