using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Ingresos;
using Frontend.Business.Movimientos.Ingresos.Searchers;
using Frontend.Business.Settings;
using Frontend.Core.Areas.Movimientos.Ingresos.IViewModels;
using Frontend.Core.Areas.Movimientos.Ingresos.Models;
using Frontend.Core.Areas.Views;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.Commons.Validations;
using Frontend.Core.Resources;
using Frontend.Core.ViewModels;
using Frontend.IServices.IServices;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Movimientos.Ingresos.ViewModels
{
    public class CabeceraDePedidoViewModel : BaseViewModel, ICabeceraDePedidoViewModel
    {

        public ICommand GoToPosicionesCommand { get; set; }

        private Pedido _pedido;
        public Pedido pedido
        {
            get { return _pedido; }
            set
            {
                SetProperty(ref _pedido, value);
            }
        }

        private NotaDeEntrega notaDeEntrega;
        public NotaDeEntrega NotaDeEntrega
        {
            get { return notaDeEntrega; }
            set
            {
                SetProperty(ref notaDeEntrega, value);
            }
        }
        private CabeceraDePedidoModel _cabeceraDePedidoModel;
        public CabeceraDePedidoModel cabeceraDePedidoModel
        {
            get { return _cabeceraDePedidoModel; }
            set
            {
                SetProperty(ref _cabeceraDePedidoModel, value);
            }
        }        

        private string cartaDePorte;
        public string CartaDePorte
        {
            get { return cartaDePorte; }
            set
            {
                SetProperty(ref cartaDePorte, value);
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

        private Movimiento movimiento;

        private Setting setting { get; set; }
        
        public ObservableRangeCollection<string> ListaClaseDeMovimiento { get; set; }

        private bool listaClaseDeMovimientoEditable;
        public bool ListaClaseDeMovimientoEditable
        {
            get { return listaClaseDeMovimientoEditable; }
            set { SetProperty(ref listaClaseDeMovimientoEditable, value); }
        }

        #region Validatable Objects
        private ValidatableObject<string> numeroNotaDeEntrega;
        public ValidatableObject<string> NumeroNotaDeEntrega
        {
            get { return numeroNotaDeEntrega; }
            set { SetProperty(ref numeroNotaDeEntrega, value); }
        }

        private ValidatableObject<string> claseDeMovimiento;
        public ValidatableObject<string> ClaseDeMovimiento        {
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
        #endregion

        #region Service constructor
        private readonly IDisplayAlertService displayAlertService;
        private readonly INavigationService navigationService;
        private readonly IPedidoService pedidoService;
        private readonly INotaDeEntregaService notaDeEntregaService;
        private readonly ISettingsService settingsService;
        private readonly DetalleNotaDeEntregaPosicionSearcher detalleNotaDeEntregaPosicionSearcher;
        private readonly IMovimientoService movimientoService;

        public CabeceraDePedidoViewModel(IDisplayAlertService displayAlertService, INavigationService navigationService, IPedidoService pedidoService, INotaDeEntregaService notaDeEntregaService, ISettingsService settingsService
            ,DetalleNotaDeEntregaPosicionSearcher detalleNotaDeEntregaPosicionSearcher, IMovimientoService movimientoService)
        {
            this.navigationService = navigationService;
            this.displayAlertService = displayAlertService;
            this.pedidoService = pedidoService;
            this.notaDeEntregaService = notaDeEntregaService;
            this.settingsService = settingsService;
            this.detalleNotaDeEntregaPosicionSearcher = detalleNotaDeEntregaPosicionSearcher;
            this.movimientoService = movimientoService;
            Init();
        }
        #endregion
        private async void Init()
        {
            cabeceraDePedidoModel = new CabeceraDePedidoModel();
            GoToPosicionesCommand = new Command(GoToPosiciones);
            
            NumeroNotaDeEntrega = new ValidatableObject<string>();
            ClaseDeMovimiento = new ValidatableObject<string>();
            ListaClaseDeMovimiento = new ObservableRangeCollection<string>();
            FechaDocumento = new ValidatableObject<DateTime>();
            FechaContabilizacion = new ValidatableObject<DateTime>();
            pedido = navigationService.GetNavigationParams<CabeceraDePedidoView>() as Pedido;
            Title = "Pedido " + pedido.NumeroPedido;
            var detalles = await detalleNotaDeEntregaPosicionSearcher.GetAll();
            FillListaClaseDeMovimiento();
            AddValidations();
            await GetPedido();
        }

        private async Task GetPedido()
        {
            setting = await settingsService.GetWithChildren();
            Centro = setting.CentroActivo.Codigo;
            NotaDeEntrega = await notaDeEntregaService.GetOrCreate(pedido);
            CartaDePorte = NotaDeEntrega.CartaDePorte;
            NumeroNotaDeEntrega.Value = NotaDeEntrega.NumeroNotaDeEntrega;
            FechaDocumento.Value = NotaDeEntrega.FechaDocumento;
            FechaContabilizacion.Value = NotaDeEntrega.FechaContabilizacion;
            movimiento = await movimientoService.GetBy(Movimiento.Ingreso);
        }

        private async void FillListaClaseDeMovimiento()
        {
            
            ListaClaseDeMovimiento.Clear();
            ListaClaseDeMovimientoEditable = true;
            if (pedidoService.ValidatePosicionesSinClase(pedido.DetallesPedido))
            {
                ListaClaseDeMovimiento.AddRange(await movimientoService.GetListaParaPosicionesSinClaseDeMovimiento());
            }
            else if (pedidoService.ValidatePosicionesTodasClase103(pedido.DetallesPedido))
            {
                ClaseDeMovimiento.Value = await movimientoService.GetValorParaPosicionesConTodasClases103();
                ListaClaseDeMovimientoEditable = false;
            }
            else
            {
                ListaClaseDeMovimiento.AddRange(await movimientoService.GetListaParaPosicionesAlMenosUnaClase103());
            }

        }
        
        private async void GoToPosiciones()
        {
            if (!Validate())
            {
                Toast.ShowMessage("La cabecera de pedido contiene errores.");
                return;
            }
            await CrearNotaDeEntrega();
            cabeceraDePedidoModel.claseMovimiento = ClaseDeMovimiento.Value;
            cabeceraDePedidoModel.notaDeEntrega = NotaDeEntrega;
            navigationService.PushAsync<CabeceraDePedidoView, PosicionesDePedidoView>(cabeceraDePedidoModel);
        }

        private async Task CrearNotaDeEntrega()
        {
            NotaDeEntrega.CartaDePorte = cartaDePorte;
            NotaDeEntrega.FechaDocumento = FechaDocumento.Value;
            NotaDeEntrega.FechaContabilizacion = FechaContabilizacion.Value;
            NotaDeEntrega.NumeroNotaDeEntrega = NumeroNotaDeEntrega.Value;
            NotaDeEntrega.UsuarioCreacion = setting.UsuarioActivo.IdRed;
            NotaDeEntrega.Pedido = pedido;
            await notaDeEntregaService.Update(NotaDeEntrega);
        }

        #region Validations

        private void AddValidations()
        {
            AddNotaDeEntregaValidations();
            AddFechaDocumentoValidations();
            AddFechaContabilizacionValidations();
            AddClaseDeMovimientoValidations();
        }

        private void AddNotaDeEntregaValidations()
        {
            NumeroNotaDeEntrega.Validations.Clear();
            NumeroNotaDeEntrega.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = MessageText.FieldRequired
            });
        }

        private void AddClaseDeMovimientoValidations()
        {
            ClaseDeMovimiento.Validations.Clear();
            ClaseDeMovimiento.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = MessageText.FieldRequired
            });
        }

        private void AddFechaDocumentoValidations()
        {
            FechaDocumento.Validations.Clear();
            FechaDocumento.Validations.Add(new IsNotNullOrEmptyRule<DateTime>
            {
                ValidationMessage = MessageText.FieldRequired
            });
        }

        private void AddFechaContabilizacionValidations()
        {
            FechaContabilizacion.Validations.Clear();
            FechaContabilizacion.Validations.Add(new IsNotNullOrEmptyRule<DateTime>
            {
                ValidationMessage = MessageText.FieldRequired
            });
        }

        private bool Validate()
        {
            bool isValidNotaDeEntrega = ValidateNotaDeEntrega();
            bool isValidFechaContabilizacion = ValidateFechaContabilizacion();
            bool isValidFechaDocumento = ValidateFechaDocumento();
            bool isValidClaseDeMovimiento= ValidateClaseDeMovimiento();

            return isValidNotaDeEntrega && isValidFechaContabilizacion && isValidFechaDocumento && isValidClaseDeMovimiento;
        }

        public bool ValidateNotaDeEntrega()
        {
            return NumeroNotaDeEntrega.Validate();
        }
        public bool ValidateClaseDeMovimiento()
        {
            return ClaseDeMovimiento.Validate();
        }

        public bool ValidateFechaDocumento()
        {
            return FechaDocumento.Validate();
        }

        public bool ValidateFechaContabilizacion()
        {
            return FechaContabilizacion.Validate();
        }
        #endregion
    }
}
