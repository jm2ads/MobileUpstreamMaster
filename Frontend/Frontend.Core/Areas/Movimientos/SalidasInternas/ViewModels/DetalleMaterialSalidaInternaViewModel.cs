using Frontend.Business.Almacenes;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.Movimientos.SalidasInternas;
using Frontend.Business.Movimientos.Traslados;
using Frontend.Business.StocksEspeciales;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Movimientos.SalidasInternas.IViewModels;
using Frontend.Core.Areas.Views;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.Commons.Validations;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Movimientos.SalidasInternas.ViewModels
{
    public class DetalleMaterialSalidaInternaViewModel : BaseViewModel, IDetalleMaterialSalidaInternaViewModel
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
        public ValidatableObject<double> CantidadEnviada
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

        private SalidaInterna salidaInterna;
        private IList<string> _unidadesEnterasList;


        public DetalleMaterialSalidaInternaViewModel(INavigationService navigationService, ISalidaInternaService salidaInternaService, IAlmacenService almacenService,
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
            detalleSalidaInterna = navigationService.GetNavigationParams<DetalleMaterialSalidaInternaView>() as DetalleSalidaInterna;
            salidaInterna = detalleSalidaInterna.SalidaInterna;

            CantidadEnviada = new ValidatableObject<double>();
            ListaAlmacen = new ObservableRangeCollection<Almacen>();
            almacen = new ValidatableObject<Almacen>();
            ListaClaseValoracion = new ObservableRangeCollection<ClaseDeValoracion>();
            claseValoracion = new ValidatableObject<ClaseDeValoracion>();
            _unidadesEnterasList = new List<string>();

            CantidadEnviada.Value = detalleSalidaInterna.CantidadEnviada;

            InitUnidadesEnteras();

            AddValidations();

            var ret = InitAsync();
        }

        private void InitUnidadesEnteras()
        {
            _unidadesEnterasList.Add("UN");
            _unidadesEnterasList.Add("UNI");
            _unidadesEnterasList.Add("KIT");
            _unidadesEnterasList.Add("ROL");
            _unidadesEnterasList.Add("PZA");
            _unidadesEnterasList.Add("CJ");
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
            AlmacenIndex = ListaAlmacen.Select(x => x.Id).ToList().IndexOf(detalleSalidaInterna.AlmacenId.GetValueOrDefault());
            almacen.Value = detalleSalidaInterna.Almacen;
        }

        private async Task FillClaseValoracion()
        {
            ListaClaseValoracion.AddRange(await claseDeValoracionService.GetAll());
            claseValoracion.Value = detalleSalidaInterna.ClaseDeValoracion ?? detalleSalidaInterna.ClaseDeValoracion;
            ClaseDeValoracionIndex = ListaAlmacen.Select(x => x.Id).ToList().IndexOf(detalleSalidaInterna.ClaseDeValoracionId);
        }

        private async void ConfirmarMaterial()
        {
            if (!Validate())
            {
                Toast.ShowMessage("El material ingresado contiene errores. Por favor, vuelva a ingresarlo");
                return;
            }

            if (CantidadEnviada.Value > detalleSalidaInterna.CantidadPendiente)
            {
                string mensaje;
                if (_unidadesEnterasList.Contains(detalleSalidaInterna.UnidadDeMedida))
                {
                    mensaje = string.Format("La cantidad ingresada excede a la cantidad pedida por {0}. ¿Desea continuar ?", CantidadEnviada.Value - detalleSalidaInterna.CantidadPendiente);
                }
                else
                {
                    double porcentaje = (CantidadEnviada.Value / detalleSalidaInterna.CantidadPendiente * 100) - 100;
                    mensaje = string.Format("La cantidad ingresada excede a la cantidad pedida en un {0}%. ¿Desea continuar?", porcentaje.ToString("0.##"));
                }

                var respuesta = await displayAlertService.Show("Aviso", mensaje, "Aceptar", "Cancelar");
                if (!respuesta)
                {
                    return;
                }

            }
            await CompleteDetalleSalidaInterna();

            navigationService.PushFromAsync<CabeceraDeSalidaInternaView, ListadoPosicionesSalidaInternaView>(salidaInterna);
        }

        private async Task CompleteDetalleSalidaInterna()
        {
            detalleSalidaInterna.CantidadEnviada = CantidadEnviada.Value;
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
            AddAlmacenValidators();
            AddClaseDeValoracionValidators();
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
            CantidadEnviada.Validations.Clear();
            CantidadEnviada.Validations.Add(new IsNotNullOrEmptyRule<double>
            {
                ValidationMessage = "La cantidad es obligatoria."
            });


            if (_unidadesEnterasList.Contains(detalleSalidaInterna.UnidadDeMedida))
            {
                CantidadEnviada.Validations.Add(new RegularExpressionRule<double>
                {
                    ValidationMessage = "La cantidad no puede ser decimal.",
                    RegularExpression = @"^-?\d{1,13}$"
                });
            }
            else
            {
                CantidadEnviada.Validations.Add(new RegularExpressionRule<double>
                {
                    ValidationMessage = "Máximo 13 caracteres enteros y 3 decimales.",
                    RegularExpression = @"^-?\d{1,13}((,|\.)\d{0,3}){0,1}$"
                });
            }

            CantidadEnviada.Validations.Add(new IsGreaterEqualThanRule<double>
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
            return CantidadEnviada.Validate();
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