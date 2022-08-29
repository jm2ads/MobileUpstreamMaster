using Frontend.Business.Almacenes;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.Movimientos.NotasDeReservas;
using Frontend.Business.StocksEspeciales;
using Frontend.Core.Areas.Movimientos.Devoluciones.IViewModels;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.Commons.Validations;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Movimientos.Devoluciones.ViewModels
{
    public class DetalleMaterialDevolucionViewModel : BaseViewModel, IDetalleMaterialDevolucionViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IReservaService reservaService;
        private readonly IAlmacenService almacenService;
        private readonly INotaDeReservaService notaDeReservaService;
        private readonly IClaseDeValoracionService claseDeValoracionService;
        private readonly ISettingsService settingsService;
        private readonly IStockEspecialService stockEspecialService;
        private readonly IStockService stockService;

        public ICommand ConfirmarCommand { get; set; }
        public DetalleNotaDeReserva detalleNotaDeReserva { get; set; }

        private ValidatableObject<double> _cantidad;
        public ValidatableObject<double> Cantidad
        {
            get { return _cantidad; }
            set
            {
                SetProperty(ref _cantidad, value);
            }
        }

        public bool IsAlmacenEnabled { get { return detalleNotaDeReserva.DetalleReserva.Almacen == null; } }
        public bool IsClaseValoracionEnabled { get { return detalleNotaDeReserva.DetalleReserva.ClaseDeValoracion == null; } }

        public ObservableRangeCollection<Almacen> ListaAlmacen { get; set; }

        private ValidatableObject<Almacen> _almacen;
        public ValidatableObject<Almacen> almacen
        {
            get { return _almacen; }
            set
            {
                SetProperty(ref _almacen, value);
            }
        }

        public ObservableRangeCollection<ClaseDeValoracion> ListaClaseValoracion { get; set; }

        private ValidatableObject<ClaseDeValoracion> _claseValoracion;
        public ValidatableObject<ClaseDeValoracion> claseValoracion
        {
            get { return _claseValoracion; }
            set
            {
                SetProperty(ref _claseValoracion, value);
            }
        }

        public ObservableRangeCollection<StockEspecial> ListaStockEspecial { get; set; }

        private ValidatableObject<StockEspecial> _stockEspecial;
        public ValidatableObject<StockEspecial> stockEspecial
        {
            get { return _stockEspecial; }
            set
            {
                SetProperty(ref _stockEspecial, value);
            }
        }

        private int _stockEspecialIndex;
        public int stockEspecialIndex
        {
            get { return _stockEspecialIndex; }
            set
            {
                SetProperty(ref _stockEspecialIndex, value);
            }
        }

        public ObservableRangeCollection<TipoStock> ListaTipoStock { get; set; }

        private ValidatableObject<TipoStock> _tipoStock;
        public ValidatableObject<TipoStock> tipoStock
        {
            get { return _tipoStock; }
            set
            {
                SetProperty(ref _tipoStock, value);
            }
        }

        private ValidatableObject<string> _textoPosicion;
        public ValidatableObject<string> textoPosicion
        {
            get { return _textoPosicion; }
            set
            {
                SetProperty(ref _textoPosicion, value);
            }
        }

        private int almacenIndex = -1;
        public int AlmacenIndex
        {
            get { return almacenIndex; }
            set
            {
                SetProperty(ref almacenIndex, value);
            }
        }
        private string _ubicacion;
        public string Ubicacion
        {
            get { if (String.IsNullOrEmpty(_ubicacion)) return "-"; else return _ubicacion; }
            set
            {
                SetProperty(ref _ubicacion, value);
            }
        }
        private int claseDeValoracionIndex = -1;
        public int ClaseDeValoracionIndex
        {
            get { return claseDeValoracionIndex; }
            set
            {
                SetProperty(ref claseDeValoracionIndex, value);
            }
        }
        
        private NotaDeReserva notaDeReserva;

        public DetalleMaterialDevolucionViewModel(INavigationService navigationService, IReservaService reservaService, IAlmacenService almacenService,
            INotaDeReservaService notaDeReservaService, IClaseDeValoracionService claseDeValoracionService, ISettingsService settingsService,
            IStockEspecialService stockEspecialService, IStockService stockService)
        {
            this.navigationService = navigationService;
            this.reservaService = reservaService;
            this.almacenService = almacenService;
            this.notaDeReservaService = notaDeReservaService;
            this.claseDeValoracionService = claseDeValoracionService;
            this.settingsService = settingsService;
            this.stockEspecialService = stockEspecialService;
            this.stockService = stockService;
            Init();
        }

        private async void Init()
        {
            Title = "Detalle de material";
            ConfirmarCommand = new Command(ConfirmarMaterial);
            detalleNotaDeReserva = navigationService.GetNavigationParams<DetalleMaterialDevolucionView>() as DetalleNotaDeReserva;

            Cantidad = new ValidatableObject<double>();
            ListaAlmacen = new ObservableRangeCollection<Almacen>();
            ListaClaseValoracion = new ObservableRangeCollection<ClaseDeValoracion>();
            ListaStockEspecial = new ObservableRangeCollection<StockEspecial>();
            ListaTipoStock = new ObservableRangeCollection<TipoStock>();
            tipoStock = new ValidatableObject<TipoStock>();
            stockEspecial = new ValidatableObject<StockEspecial>();
            textoPosicion = new ValidatableObject<string>();
            claseValoracion = new ValidatableObject<ClaseDeValoracion>(async () => await LoteValueChanged());
            almacen = new ValidatableObject<Almacen>(async () => await AlmacenValueChanged());
            Cantidad.Value = detalleNotaDeReserva.CantidadIngresada;
            textoPosicion.Value = detalleNotaDeReserva.TextoPosicion;

            AddValidations();

            FillTipoStock();

            await InitAsync();
        }

        private async Task LoteValueChanged()
        {
            if (claseValoracion.Value != null)
            {
                if (almacen.Value != null)
                {
                    await MostrarUbicacion();
                }
            }
        }

        private async Task MostrarUbicacion()
        {
            try
            {
                Ubicacion = await stockService.GetUbicacionBy(detalleNotaDeReserva.DetalleReserva.CentroId, detalleNotaDeReserva.DetalleReserva.MaterialId, almacen.Value.Id, claseValoracion.Value.Id);
            }
            catch (Exception e)
            {
                Ubicacion = "-";
            }
        }

        private async Task AlmacenValueChanged()
        {
            if (almacen.Value != null)
            {
                if (claseValoracion.Value != null)
                {
                    await MostrarUbicacion();
                }
            }
        }
        private async Task InitAsync()
        {
            await FillListaAlmacen();
            await FillClaseValoracion();
            await FillStockEspecial();
        }

        private async Task FillListaAlmacen()
        {
            var setting = await settingsService.Get();
            ListaAlmacen.AddRange(await almacenService.GetByIdCentro(setting.CentroActivoId));
            almacen.Value = detalleNotaDeReserva.Almacen ?? detalleNotaDeReserva.DetalleReserva.Almacen;
            AlmacenIndex = ListaAlmacen.Select(x => x.Id).ToList().IndexOf(detalleNotaDeReserva.AlmacenId);
        }

        private async Task FillClaseValoracion()
        {
            ListaClaseValoracion.AddRange(await claseDeValoracionService.GetAll());
            claseValoracion.Value = detalleNotaDeReserva.ClaseDeValoracion ?? detalleNotaDeReserva.DetalleReserva.ClaseDeValoracion;
            ClaseDeValoracionIndex = ListaAlmacen.Select(x => x.Id).ToList().IndexOf(detalleNotaDeReserva.ClaseDeValoracionId);
        }

        private async Task FillStockEspecial()
        {
            ListaStockEspecial.AddRange(await stockEspecialService.GetAll());
            stockEspecial.Value = detalleNotaDeReserva.StockEspecial;
            stockEspecialIndex = ListaStockEspecial.Select(x => x.Codigo).ToList().IndexOf(detalleNotaDeReserva.StockEspecial.Codigo);
        }

        private void FillTipoStock()
        {
            ListaTipoStock.AddRange(reservaService.GetAllTipoStock());
            tipoStock.Value = reservaService.GetTipoStockBy(detalleNotaDeReserva.TipoStockCodigo);
        }

        private async void ConfirmarMaterial()
        {
            CheckEntregaFinal();
            if (!Validate())
            {
                Toast.ShowMessage("El material ingresado contiene errores. Por favor, vuelva a ingresarlo");
                return;
            }
            await CompleteDetalleReserva();

            navigationService.PushFromAsync<CabeceraDevolucionView, DetalleDevolucionView>(notaDeReserva);
        }

        private void CheckEntregaFinal()
        {
            Cantidad.Validations.Clear();
            Cantidad.Validations.Add(new IsNotNullOrEmptyRule<double>
            {
                ValidationMessage = "La cantidad es obligatoria."
            });
            Cantidad.Validations.Add(new RegularExpressionRule<double>
            {
                ValidationMessage = "Máximo 13 caracteres enteros y 3 decimales.",
                RegularExpression = @"^-?\d{1,13}((,|\.)\d{0,3}){0,1}$"
            });

            if (detalleNotaDeReserva.EsEntregaFinal)
            {
                Cantidad.Validations.Add(new IsGreaterEqualThanRule<double>
                {
                    ValidationMessage = "La cantidad ingresada debe ser mayor o igual a 0.",
                    Value = 0
                });
            }
            else
            {
                Cantidad.Validations.Add(new IsGreaterThanRule<double>
                {
                    ValidationMessage = "La cantidad ingresada debe ser mayor a 0.",
                    Value = 0
                });
            }
        }

        private async Task CompleteDetalleReserva()
        {
            detalleNotaDeReserva.CantidadIngresada = Cantidad.Value;
            detalleNotaDeReserva.EsContado = true;
            detalleNotaDeReserva.AlmacenId = almacen.Value.Id;
            detalleNotaDeReserva.Almacen = almacen.Value;
            detalleNotaDeReserva.ClaseDeValoracionId = claseValoracion.Value.Id;
            detalleNotaDeReserva.ClaseDeValoracion = claseValoracion.Value;
            detalleNotaDeReserva.StockEspecialId = stockEspecial.Value.Id;
            detalleNotaDeReserva.StockEspecial = stockEspecial.Value;
            detalleNotaDeReserva.TipoStockCodigo = tipoStock.Value.Codigo;
            detalleNotaDeReserva.TextoPosicion = textoPosicion.Value;

            await notaDeReservaService.Update(detalleNotaDeReserva);
            notaDeReserva = await notaDeReservaService.GetOrCreate(detalleNotaDeReserva.DetalleReserva.Reserva);
        }

        #region Validaciones
        private void AddValidations()
        {
            almacen.Validations.Clear();
            almacen.Validations.Add(new IsNotNullOrEmptyRule<Almacen>
            {
                ValidationMessage = "El almacen es obligatorio."
            });

            claseValoracion.Validations.Clear();
            claseValoracion.Validations.Add(new IsNotNullOrEmptyRule<ClaseDeValoracion>
            {
                ValidationMessage = "La clase de valoración es obligatoria."
            });

            tipoStock.Validations.Clear();
            tipoStock.Validations.Add(new IsNotNullOrEmptyRule<TipoStock>
            {
                ValidationMessage = "El tipo de stock es obligatorio."
            });

            stockEspecial.Validations.Clear();
            stockEspecial.Validations.Add(new IsNotNullOrEmptyRule<StockEspecial>
            {
                ValidationMessage = "El stock especial es obligatorio."
            });

            textoPosicion.Validations.Clear();
            textoPosicion.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "El texto posición es obligatorio."
            });
        }

        private bool Validate()
        {
            bool isValidCantidad = ValidateCantidad();
            bool isValidAlmacen = ValidateAlmacen();
            bool isValidClaseDeValoracion = ValidateClaseDeValoracion();
            bool isValidStockEspecial = ValidateStockEspecial();
            bool isValidTipoStock = ValidateTipoStock();
            bool isVaidTextoPosicion = ValidateTextoPosicion();

            return isValidCantidad
                && isValidAlmacen
                && isValidClaseDeValoracion
                && isValidStockEspecial
                && isValidTipoStock
                && isVaidTextoPosicion;
        }

        public bool ValidateCantidad()
        {
            return Cantidad.Validate();
        }

        public bool ValidateAlmacen()
        {
            return almacen.Validate();
        }

        public bool ValidateClaseDeValoracion()
        {
            return claseValoracion.Validate();
        }

        public bool ValidateStockEspecial()
        {
            return stockEspecial.Validate();
        }

        public bool ValidateTipoStock()
        {
            return tipoStock.Validate();
        }

        public bool ValidateTextoPosicion()
        {
            return textoPosicion.Validate();
        }
        #endregion
    }
}
