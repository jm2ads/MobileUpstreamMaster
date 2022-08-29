using Frontend.Business.Almacenes;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.Movimientos.SalidasInternas;
using Frontend.Core.Areas.Movimientos.SalidaTraspaso.IViewModels;
using Frontend.Core.Areas.Views;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.Commons.Validations;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace Frontend.Core.Areas.Movimientos.SalidaTraspaso.ViewModels
{
    public class DetalleMaterialSalidaTraspasoViewModel : BaseViewModel, IDetalleMaterialSalidaTraspasoViewModel
    {
        private readonly INavigationService navigationService;
        private readonly ISalidaInternaService salidaInternaService;
        private readonly IAlmacenService almacenService;
        private readonly IClaseDeValoracionService claseDeValoracionService;
        private readonly ISettingsService settingsService;
        private readonly IStockEspecialService stockEspecialService;
        private readonly IDisplayAlertService displayAlertService;

        public ICommand ConfirmarCommand { get; set; }
        public DetalleSalidaInterna detalleSalidaInterna { get; set; }

        private ValidatableObject<double> _cantidad;
        public ValidatableObject<double> cantidadEnviada
        {
            get { return _cantidad; }
            set
            {
                SetProperty(ref _cantidad, value);
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

        private int _almacenIndex = -1;
        public int almacenIndex
        {
            get { return _almacenIndex; }
            set
            {
                SetProperty(ref _almacenIndex, value);
            }
        }

        private int _claseDeValoracionIndex = -1;
        public int claseDeValoracionIndex
        {
            get { return _claseDeValoracionIndex; }
            set
            {
                SetProperty(ref _claseDeValoracionIndex, value);
            }
        }

        private SalidaInterna salidaInterna;
        private IList<string> _unidadesList;


        public DetalleMaterialSalidaTraspasoViewModel(INavigationService navigationService, ISalidaInternaService salidaInternaService, IAlmacenService almacenService,
            IClaseDeValoracionService claseDeValoracionService, ISettingsService settingsService,
            IStockEspecialService stockEspecialService, IDisplayAlertService displayAlertService)
        {
            this.navigationService = navigationService;
            this.salidaInternaService = salidaInternaService;
            this.almacenService = almacenService;
            this.claseDeValoracionService = claseDeValoracionService;
            this.settingsService = settingsService;
            this.stockEspecialService = stockEspecialService;
            this.displayAlertService = displayAlertService;
            Init();
        }

        private void Init()
        {
            Title = "Detalle de material";
            ConfirmarCommand = new Command(ConfirmarMaterial);
            detalleSalidaInterna = navigationService.GetNavigationParams<DetalleMaterialSalidaTraspasoView>() as DetalleSalidaInterna;
            salidaInterna = detalleSalidaInterna.SalidaInterna;

            cantidadEnviada = new ValidatableObject<double>();
            ListaAlmacen = new ObservableRangeCollection<Almacen>();
            almacen = new ValidatableObject<Almacen>();
            ListaClaseValoracion = new ObservableRangeCollection<ClaseDeValoracion>();
            claseValoracion = new ValidatableObject<ClaseDeValoracion>();
            _unidadesList = new List<string>();

            cantidadEnviada.Value = detalleSalidaInterna.CantidadEnviada;

            AddValidations();

            InitUnidades();

            var ret = InitAsync();
        }

        private void InitUnidades()
        {
            _unidadesList.Add("UN");
            _unidadesList.Add("UNI");
            _unidadesList.Add("KIT");
            _unidadesList.Add("ROL");
            _unidadesList.Add("PZA");
            _unidadesList.Add("CJ");
        }

        private async Task InitAsync()
        {
            await FillListaAlmacen();
            await FillClaseValoracion();
        }

        private async Task FillListaAlmacen()
        {
            var setting = await settingsService.Get();
            ListaAlmacen.AddRange(await almacenService.GetByIdCentro(setting.CentroActivoId));
            almacenIndex = ListaAlmacen.Select(x => x.Id).ToList().IndexOf(detalleSalidaInterna.AlmacenId.GetValueOrDefault());
            almacen.Value = detalleSalidaInterna.Almacen;
        }

        private async Task FillClaseValoracion()
        {
            ListaClaseValoracion.AddRange(await claseDeValoracionService.GetAll());
            claseValoracion.Value = detalleSalidaInterna.ClaseDeValoracion ?? detalleSalidaInterna.ClaseDeValoracion;
            claseDeValoracionIndex = ListaClaseValoracion.Select(x => x.Id).ToList().IndexOf(detalleSalidaInterna.ClaseDeValoracionId);
        }

        private async void ConfirmarMaterial()
        {
            if (!Validate())
            {
                Toast.ShowMessage("El material ingresado contiene errores. Por favor, vuelva a ingresarlo");
                return;
            }

            if (cantidadEnviada.Value > detalleSalidaInterna.CantidadPendiente)
            {
                string mensaje;
                if (_unidadesList.Contains(detalleSalidaInterna.UnidadDeMedida))
                {
                    mensaje = string.Format("La cantidad ingresada excede a la cantidad pedida por {0}. ¿Desea continuar ?", cantidadEnviada.Value - detalleSalidaInterna.CantidadPendiente);

                }
                else
                {
                    double porcentaje = (cantidadEnviada.Value / detalleSalidaInterna.CantidadPendiente * 100) - 100;
                    mensaje = string.Format("La cantidad ingresada excede a la cantidad pedida en un {0}%. ¿Desea continuar?", porcentaje.ToString("0.##"));
                }

                var respuesta = await displayAlertService.Show("Aviso", mensaje, "Aceptar", "Cancelar");
                if (!respuesta)
                {
                    return;
                }

            }
            await CompleteDetalleSalidaInterna();

            navigationService.PushFromAsync<CabeceraSalidaTraspasoView, ListadoPosicionesSalidaTraspasoView>(salidaInterna);
        }

        private async Task CompleteDetalleSalidaInterna()
        {
            detalleSalidaInterna.CantidadEnviada = cantidadEnviada.Value;
            detalleSalidaInterna.AlmacenId = almacen.Value?.Id;
            detalleSalidaInterna.Almacen = almacen.Value;
            detalleSalidaInterna.ClaseDeValoracionId = claseValoracion.Value.Id;
            detalleSalidaInterna.ClaseDeValoracion = claseValoracion.Value;
            detalleSalidaInterna.EsContado = true;

            await salidaInternaService.Update(detalleSalidaInterna);
        }

        #region Validaciones
        private void AddValidations()
        {
            AddClaseDeValoracionValidators();
            AddAlmacenValidators();
            AddCantidadValidators();
        }

        private void AddAlmacenValidators()
        {
            almacen.Validations.Clear();
            almacen.Validations.Add(new IsNotNullOrEmptyRule<Almacen>
            {
                ValidationMessage = "El almacén es obligatorio."
            });
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
            cantidadEnviada.Validations.Clear();
            cantidadEnviada.Validations.Add(new IsNotNullOrEmptyRule<double>
            {
                ValidationMessage = "La cantidad es obligatoria."
            });
            cantidadEnviada.Validations.Add(new RegularExpressionRule<double>
            {
                ValidationMessage = "Máximo 13 caracteres enteros y 3 decimales.",
                RegularExpression = @"^-?\d{1,13}((,|\.)\d{0,3}){0,1}$"
            });

            cantidadEnviada.Validations.Add(new IsGreaterEqualThanRule<double>
            {
                ValidationMessage = "La cantidad ingresada debe ser mayor o igual a 0.",
                Value = 0
            });
        }

        private bool Validate()
        {
            bool isValidCantidad = ValidateCantidad();
            bool isValidAlmacen = ValidateAlmacen();
            bool isValidClaseDeValoracion = ValidateClaseDeValoracion();

            return isValidCantidad
                && isValidAlmacen
                && isValidClaseDeValoracion;
        }

        public bool ValidateCantidad()
        {
            return cantidadEnviada.Validate();
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