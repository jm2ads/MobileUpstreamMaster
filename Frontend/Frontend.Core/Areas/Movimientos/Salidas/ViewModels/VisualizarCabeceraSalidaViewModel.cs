using Frontend.Business.Movimientos.NotasDeReservas;
using Frontend.Business.Movimientos.Reservas;
using Frontend.Business.Settings;
using Frontend.Core.Areas.Movimientos.Salidas.IViewModels;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;

namespace Frontend.Core.Areas.Movimientos.Salidas.ViewModels
{
    public class VisualizarCabeceraSalidaViewModel : BaseViewModel, IVisualizarCabeceraSalidaViewModel
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

        private string _fechaDocumentacion;
        public string FechaDocumentacion
        {
            get { return _fechaDocumentacion; }
            set
            {
                SetProperty(ref _fechaDocumentacion, value);
            }
        }

        private string _nombreCampoVariable;
        public string NombreCampoVariable
        {
            get { return _nombreCampoVariable; }
            set
            {
                SetProperty(ref _nombreCampoVariable, value);
            }
        }

        private NotaDeReserva _notaDeReserva;
        public NotaDeReserva notaDeReserva
        {
            get { return _notaDeReserva; }
            set
            {
                SetProperty(ref _notaDeReserva, value);
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

        public VisualizarCabeceraSalidaViewModel(INavigationService navigationService, ISettingsService settingsService)
        {
            this.navigationService = navigationService;
            this.settingsService = settingsService;
            Init();
        }

        private async void Init()
        {
            notaDeReserva = navigationService.GetNavigationParams<VisualizarCabeceraSalidaView>() as NotaDeReserva;
            Title = "Salida " + notaDeReserva.Reserva.Numero;
            setting = await settingsService.GetWithChildren();
            FechaContabilizacion = notaDeReserva.FechaContabilizacion.ToString("dd/MM/yyyy");
            FechaDocumentacion = notaDeReserva.FechaDocumentacion.ToString("dd/MM/yyyy");
            NombreCampoVariable = ClaseDeMovimientoReserva.Get()[notaDeReserva.Reserva.ClaseDeMovimiento];
        }
    }
}
