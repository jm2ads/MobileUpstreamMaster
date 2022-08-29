using Frontend.Business.Almacenes;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.Movimientos.Traslados;
using Frontend.Business.Settings;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.Commons.Validations;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Movimientos.Traslados.ViewModels
{
    public class DetalleMaterialTraslado309ViewModel : BaseViewModel, IDetalleMaterialTraslado309ViewModel
    {
        private readonly INavigationService navigationService;
        private readonly ITrasladoService trasladoService;
        private readonly IAlmacenService almacenService;
        private readonly IClaseDeValoracionService claseDeValoracionService;
        private readonly ISettingsService settingsService;
        private readonly IStockEspecialService stockEspecialService;

        public ICommand ConfirmarCommand { get; set; }
        public DetalleTraslado detalleTraslado { get; set; }

        private string _textoBreve;
        public string textoBreve
        {
            get { return _textoBreve; }
            set
            {
                SetProperty(ref _textoBreve, value);
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

        private ValidatableObject<double> _cantidad;
        public ValidatableObject<double> Cantidad
        {
            get { return _cantidad; }
            set
            {
                SetProperty(ref _cantidad, value);
            }
        }

        private bool _isAlmacenVisible;
        public bool IsAlmacenVisible
        {
            get { return _isAlmacenVisible; }
            set
            {
                SetProperty(ref _isAlmacenVisible, value);
            }
        }

        
        public bool IsProveedorEnabled
        {
            get
            {
                return traslado.ClaseDeMovimientoCodigo == ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_541];
            }
        }
        public bool IsElementoPEPEnabled
        {
            get
            {
                return detalleTraslado.StockEspecial.Codigo == "Q";
            }
        }

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

        private ValidatableObject<string> _codigoMaterial;
        public ValidatableObject<string> codigoMaterial
        {
            get { return _codigoMaterial; }
            set
            {
                SetProperty(ref _codigoMaterial, value);
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

        private int claseDeValoracionIndex = -1;
        public int ClaseDeValoracionIndex
        {
            get { return claseDeValoracionIndex; }
            set
            {
                SetProperty(ref claseDeValoracionIndex, value);
            }
        }

        private Traslado traslado;


        public DetalleMaterialTraslado309ViewModel(INavigationService navigationService, ITrasladoService trasladoService, IAlmacenService almacenService,
            IClaseDeValoracionService claseDeValoracionService, ISettingsService settingsService,
            IStockEspecialService stockEspecialService)
        {
            this.navigationService = navigationService;
            this.trasladoService = trasladoService;
            this.almacenService = almacenService;
            this.claseDeValoracionService = claseDeValoracionService;
            this.settingsService = settingsService;
            this.stockEspecialService = stockEspecialService;
            Init();
        }

        private void Init()
        {
            Title = "Detalle de material";
            ConfirmarCommand = new Command(ConfirmarMaterial);
            detalleTraslado = navigationService.GetNavigationParams<DetalleMaterialTraslado309View>() as DetalleTraslado;
            traslado = detalleTraslado.Traslado;

            Cantidad = new ValidatableObject<double>();
            ListaAlmacen = new ObservableRangeCollection<Almacen>();
            almacen = new ValidatableObject<Almacen>();
            ListaClaseValoracion = new ObservableRangeCollection<ClaseDeValoracion>();
            claseValoracion = new ValidatableObject<ClaseDeValoracion>();
            codigoMaterial = new ValidatableObject<string>();

            Cantidad.Value = detalleTraslado.Cantidad;
            codigoMaterial.Value = detalleTraslado.CodigoMaterial;
            textoBreve = detalleTraslado.TextoBreve;

            AddValidations();

            var ret = InitAsync();
        }

        private async Task InitAsync()
        {
            setting = await settingsService.GetWithChildren();
            await FillListaAlmacen();
            await FillClaseValoracion();
        }

        private async Task FillListaAlmacen()
        {
            ListaAlmacen.AddRange(await almacenService.GetByIdCentro(setting.CentroActivoId));
            AlmacenIndex = ListaAlmacen.Select(x => x.Id).ToList().IndexOf(detalleTraslado.AlmacenId.GetValueOrDefault());
            almacen.Value = detalleTraslado.Almacen;
            IsAlmacenVisible = detalleTraslado.StockEspecial.Codigo != "O";
        }

        private async Task FillClaseValoracion()
        {
            ListaClaseValoracion.AddRange(await claseDeValoracionService.GetAll());
            claseValoracion.Value = detalleTraslado.ClaseDeValoracion ?? detalleTraslado.ClaseDeValoracion;
            ClaseDeValoracionIndex = ListaClaseValoracion.Select(x => x.Id).ToList().IndexOf(detalleTraslado.ClaseDeValoracionId);
        }

        private async void ConfirmarMaterial()
        {
            if (!Validate())
            {
                Toast.ShowMessage("El material ingresado contiene errores. Por favor, vuelva a ingresarlo");
                return;
            }
            await CompleteDetalleTraslado();

            navigationService.PushFromAsync<HomeView, ListaPosicionesTraslado309View>(traslado);
        }

        private async Task CompleteDetalleTraslado()
        {
            detalleTraslado.Cantidad = Cantidad.Value;
            detalleTraslado.AlmacenId = almacen.Value?.Id ?? 0;
            detalleTraslado.Almacen = almacen.Value;
            detalleTraslado.ClaseDeValoracionId = claseValoracion.Value.Id;
            detalleTraslado.ClaseDeValoracion = claseValoracion.Value;
            detalleTraslado.CodigoMaterial = codigoMaterial.Value.PadLeft(18, '0').ToUpper();
            detalleTraslado.ElementoPEP = detalleTraslado.Stock.DetalleStockEspecial.Detalle;
            detalleTraslado.Proveedor = detalleTraslado.Stock.DetalleStockEspecial.Detalle;
            detalleTraslado.TextoBreve = textoBreve;

            await trasladoService.Update(detalleTraslado);
        }

        #region Validaciones
        private void AddValidations()
        {
            AddCodigoMaterialValidators();
            AddClaseDeValoracionValidators();
            AddCantidadValidators();
        }

        private void AddCodigoMaterialValidators()
        {
            codigoMaterial.Validations.Clear();
            codigoMaterial.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "El código es obligatorio."
            });
        }

        private void AddAlmacenValidators()
        {
            if (IsAlmacenVisible)
            {
                almacen.Validations.Clear();
                almacen.Validations.Add(new IsNotNullOrEmptyRule<Almacen>
                {
                    ValidationMessage = "El almacen es obligatorio."
                });
            }
        }

        private void AddClaseDeValoracionValidators()
        {
            claseValoracion.Validations.Clear();
            claseValoracion.Validations.Add(new IsNotNullOrEmptyRule<ClaseDeValoracion>
            {
                ValidationMessage = "La clase de valoración es obligatoria."
            });
        }

        private void AddCantidadValidators()
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

            Cantidad.Validations.Add(new IsGreaterEqualThanRule<double>
            {
                ValidationMessage = "La cantidad ingresada debe ser mayor o igual a 0.",
                Value = 0
            });

            Cantidad.Validations.Add(new IsLowerEqualThanRule<double>
            {
                ValidationMessage = string.Format("La cantidad ingresada no debe ser mayor a {0}.", detalleTraslado.Stock.CantidadAlmacen),
                Value = detalleTraslado.Stock.CantidadAlmacen
            });
        }

        private bool Validate()
        {
            AddAlmacenValidators();

            bool isValidCantidad = ValidateCantidad();
            bool isValidAlmacen = ValidateAlmacen();
            bool isValidClaseDeValoracion = ValidateClaseDeValoracion();
            bool isValidCodigoMaterial = ValidateCodigoMaterial();

            return isValidCantidad
                && isValidAlmacen
                && isValidClaseDeValoracion
                && isValidCodigoMaterial;
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

        public bool ValidateCodigoMaterial()
        {
            return codigoMaterial.Validate();
        }
        #endregion

    }
}
