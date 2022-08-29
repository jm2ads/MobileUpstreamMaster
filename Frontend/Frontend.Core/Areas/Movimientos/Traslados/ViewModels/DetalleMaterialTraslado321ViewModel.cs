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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Movimientos.Traslados.ViewModels
{
    public class DetalleMaterialTraslado321ViewModel : BaseViewModel, IDetalleMaterialTraslado321ViewModel
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

        public bool IsClaseValoracionEnabled
        {
            get
            {
                return detalleTraslado.ClaseDeValoracion == null
                    || traslado.ClaseDeMovimientoCodigo == ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_309]
                    || traslado.ClaseDeMovimientoCodigo == ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_311]
                    || traslado.ClaseDeMovimientoCodigo == ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_321];
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


        private ValidatableObject<Almacen> _almacen;
        public ValidatableObject<Almacen> almacen
        {
            get { return _almacen; }
            set
            {
                SetProperty(ref _almacen, value);
            }
        }


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
        private Traslado traslado;


        public DetalleMaterialTraslado321ViewModel(INavigationService navigationService, ITrasladoService trasladoService, IAlmacenService almacenService,
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
            detalleTraslado = navigationService.GetNavigationParams<DetalleMaterialTraslado321View>() as DetalleTraslado;
            traslado = detalleTraslado.Traslado;

            Cantidad = new ValidatableObject<double>();
            almacen = new ValidatableObject<Almacen>();
            claseValoracion = new ValidatableObject<ClaseDeValoracion>();
            codigoMaterial = new ValidatableObject<string>();

            Cantidad.Value = detalleTraslado.Stock.CantidadCalidad;
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
            almacen.Value = detalleTraslado.Almacen;
        }

        private async Task FillClaseValoracion()
        {
            claseValoracion.Value = detalleTraslado.ClaseDeValoracion ?? detalleTraslado.ClaseDeValoracion;
        }

        private async void ConfirmarMaterial()
        {
            if (!Validate())
            {
                Toast.ShowMessage("El material ingresado contiene errores. Por favor, vuelva a ingresarlo");
                return;
            }
            await CompleteDetalleTraslado();

            navigationService.PushFromAsync<HomeView, ListaPosicionesTraslado321View>(traslado);
        }

        private async Task CompleteDetalleTraslado()
        {
            detalleTraslado.Cantidad = Cantidad.Value;
            detalleTraslado.AlmacenId = almacen.Value?.Id;
            detalleTraslado.Almacen = almacen.Value;
            detalleTraslado.ClaseDeValoracionId = claseValoracion.Value.Id;
            detalleTraslado.ClaseDeValoracion = claseValoracion.Value;
            detalleTraslado.CodigoMaterial = codigoMaterial.Value;
            detalleTraslado.ElementoPEP = detalleTraslado.StockEspecial.Codigo == "Q" ? detalleTraslado.Stock.DetalleStockEspecial.Detalle : string.Empty;
            detalleTraslado.Proveedor = detalleTraslado.StockEspecial.Codigo == "K" || detalleTraslado.StockEspecial.Codigo == "O" ? detalleTraslado.Stock.DetalleStockEspecial.Detalle : string.Empty;
            detalleTraslado.TextoBreve = textoBreve;

            await trasladoService.Update(detalleTraslado);
        }

        #region Validaciones
        private void AddValidations()
        {
            AddClaseDeValoracionValidators();
            AddCantidadValidators();
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
                ValidationMessage = string.Format("La cantidad ingresada no debe ser mayor a {0}.", detalleTraslado.Stock.CantidadCalidad),
                Value = detalleTraslado.Stock.CantidadCalidad
            });
        }

        private bool Validate()
        {
            AddAlmacenValidators();

            bool isValidCantidad = ValidateCantidad();
            bool isValidAlmacen = ValidateAlmacen();
            bool isValidClaseDeValoracion = ValidateClaseDeValoracion();

            return isValidCantidad
                && isValidAlmacen
                && isValidClaseDeValoracion;
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
        #endregion

    }
}