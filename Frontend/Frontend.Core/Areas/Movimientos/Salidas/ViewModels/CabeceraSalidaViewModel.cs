using Frontend.Business.Movimientos.NotasDeReservas;
using Frontend.Business.Movimientos.Reservas;
using Frontend.Business.Settings;
using Frontend.Core.Areas.Movimientos.Salidas.IViewModels;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Validations;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Movimientos.Salidas.ViewModels
{
    public class CabeceraSalidaViewModel : BaseViewModel, ICabeceraSalidaViewModel
    {
        private readonly INavigationService navigationService;
        private readonly ISettingsService settingsService;
        private readonly INotaDeReservaService notaDeReservaService;

        private NotaDeReserva _notaDeReserva;
        public NotaDeReserva notaDeReserva
        {
            get { return _notaDeReserva; }
            set
            {
                SetProperty(ref _notaDeReserva, value);
            }
        }

        private string _nombreImputacion;
        public string NombreImputacion
        {
            get { return _nombreImputacion; }
            set
            {
                SetProperty(ref _nombreImputacion, value);
            }
        }

        private string _textoCabecera;
        public string TextoCabecera
        {
            get { return _textoCabecera; }
            set
            {
                SetProperty(ref _textoCabecera, value);
            }
        }

        private string _textoPosicionGenerico;
        public string TextoPosicionGenerico
        {
            get { return _textoPosicionGenerico; }
            set
            {
                SetProperty(ref _textoPosicionGenerico, value);
            }
        }

        private ValidatableObject<DateTime> _fechaDocumento;
        public ValidatableObject<DateTime> FechaDocumento
        {
            get { return _fechaDocumento; }
            set
            {
                SetProperty(ref _fechaDocumento, value);
            }
        }

        private ValidatableObject<DateTime> _fechaContabilizacion;
        public ValidatableObject<DateTime> FechaContabilizacion
        {
            get { return _fechaContabilizacion; }
            set
            {
                SetProperty(ref _fechaContabilizacion, value);
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
    
        public ICommand AplicarCommand { get; set; }

        public CabeceraSalidaViewModel(INavigationService navigationService, ISettingsService settingsService, INotaDeReservaService notaDeReservaService)
        {
            this.navigationService = navigationService;
            this.settingsService = settingsService;
            this.notaDeReservaService = notaDeReservaService;
            Init();
        }
        private async void Init()
        {
            var reserva = navigationService.GetNavigationParams<CabeceraSalidaView>() as Reserva;

            NombreImputacion = ClaseDeMovimientoReserva.Get()[reserva.ClaseDeMovimiento];

            FechaContabilizacion = new ValidatableObject<DateTime>();
            FechaDocumento = new ValidatableObject<DateTime>();
            AplicarCommand = new Command(Aplicar);

            await InitAsync(reserva);
        }

        private async Task InitAsync(Reserva reserva)
        {
            notaDeReserva = await notaDeReservaService.GetOrCreate(reserva);
             setting = await settingsService.GetWithChildren();
           // notaDeReserva.UsuarioReserva = setting.UsuarioActivo.IdRed;//ASOSA USUARIO

            setting = await settingsService.GetWithChildren();
            Title = "Salida " + notaDeReserva.Reserva.Numero;
            TextoCabecera = notaDeReserva.TextoCabecera;
            TextoPosicionGenerico = notaDeReserva.TextoPosicionGenerico;

            FechaContabilizacion.Value = notaDeReserva.FechaContabilizacion;
            FechaDocumento.Value = notaDeReserva.FechaDocumentacion;
        }

        private async void Aplicar()
        {
            var notaDeReserva = await CreateNotaDeReserva();
            navigationService.PushAsync<CabeceraSalidaView, DetalleSalidaView>(notaDeReserva);
        }

        private async Task<NotaDeReserva> CreateNotaDeReserva()
        {
            notaDeReserva.TextoCabecera = TextoCabecera;
            notaDeReserva.FechaContabilizacion = FechaContabilizacion.Value;
            notaDeReserva.FechaDocumentacion = FechaDocumento.Value;
            notaDeReserva.UsuarioReserva = setting.UsuarioActivo.IdRed;

            if (!string.IsNullOrWhiteSpace(TextoPosicionGenerico)
                && notaDeReserva.TextoPosicionGenerico != TextoPosicionGenerico)
            {
                foreach (var detalleNotaDeReserva in notaDeReserva.DetallesNotasDeReservas)
                {
                    detalleNotaDeReserva.TextoPosicion = TextoPosicionGenerico;
                }
            }

            notaDeReserva.TextoPosicionGenerico = TextoPosicionGenerico;

            await notaDeReservaService.Update(notaDeReserva);

            return notaDeReserva;
        }
    }
}
