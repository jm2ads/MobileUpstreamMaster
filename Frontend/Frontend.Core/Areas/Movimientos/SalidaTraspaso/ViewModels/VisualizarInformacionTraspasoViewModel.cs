using Frontend.Business.Movimientos.SalidasInternas;
using Frontend.Business.Settings;
using Frontend.Core.Areas.Movimientos.SalidaTraspaso.IViewModels;
using Frontend.Core.Areas.Views;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.ViewModels;
using Frontend.IServices.IServices;

namespace Frontend.Core.Areas.Movimientos.SalidaTraspaso.ViewModels
{
    public class VisualizarInformacionTraspasoViewModel : BaseViewModel, IVisualizarInformacionTraspasoViewModel
    {

        private string _fechaContabilizacion;
        public string FechaContabilizacion
        {
            get { return _fechaContabilizacion; }
            set
            {
                SetProperty(ref _fechaContabilizacion, value);
            }
        }
        private string claseDeMovimiento;
        public string ClaseDeMovimiento
        {
            get { return claseDeMovimiento; }
            set { SetProperty(ref claseDeMovimiento, value); }
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
        private string _centroEmisor;
        public string CentroEmisor
        {
            get { return _centroEmisor; }
            set
            {
                SetProperty(ref _centroEmisor, value);
            }
        }
        private string _centroReceptor;
        public string CentroReceptor
        {
            get { return _centroReceptor; }
            set
            {
                SetProperty(ref _centroReceptor, value);
            }
        }

        private SalidaInterna _salidaInterna;
        public SalidaInterna salidaInterna
        {
            get { return _salidaInterna; }
            set
            {
                SetProperty(ref _salidaInterna, value);
            }
        }

        private Setting _setting;
        public Setting setting
        {
            get { return _setting; }
            set
            {
                SetProperty(ref _setting, value);
            }
        }

        private readonly INavigationService navigationService;
        private readonly ISettingsService settingsService;

        public VisualizarInformacionTraspasoViewModel(INavigationService navigationService, ISettingsService settingsService)
        {
            this.navigationService = navigationService;
            this.settingsService = settingsService;
            Init();
        }

        private async void Init()
        {
            salidaInterna = navigationService.GetNavigationParams<VisualizarInformacionTraspasoView>() as SalidaInterna;
            setting = await settingsService.GetWithChildren();
            CentroEmisor = setting.CentroActivo.Codigo;
            CentroReceptor = salidaInterna.CentroReceptor.Codigo;
            ClaseDeMovimiento = salidaInterna.ClaseDeMovimientoCodigo;
            FechaContabilizacion = salidaInterna.FechaContabilizacion.ToString("dd/MM/yyyy");
            FechaDocumento = salidaInterna.FechaDocumento.ToString("dd/MM/yyyy");
        }
    }
}
