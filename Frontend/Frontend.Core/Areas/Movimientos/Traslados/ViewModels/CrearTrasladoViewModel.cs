using Frontend.Business.Movimientos.Traslados;
using Frontend.Business.Movimientos.Traslados.Core;
using Frontend.Business.Movimientos.Traslados.Helper;
using Frontend.Business.Settings;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.Commons.Validations;
using Frontend.Core.Resources;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Movimientos.Traslados.ViewModels
{
    public class CrearTrasladoViewModel : BaseViewModel, ICrearTrasladoViewModel
    {
        public ICommand AplicarCommand { get; set; }

        private Traslado traslado;
        public Traslado Traslado
        {
            get { return traslado; }
            set { SetProperty(ref traslado, value); }
        }

        private Setting setting { get; set; }

        public ObservableRangeCollection<string> ListaClaseDeMovimiento { get; set; }

        private Dictionary<string, Type> claseDeMovimientoTrasladoPages { get; set; }

        #region Validatable Objects
        private ValidatableObject<string> claseDeMovimiento;
        public ValidatableObject<string> ClaseDeMovimiento
        {
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
        private readonly INavigationService navigationService;
        private readonly ClaseDeMovimientoTrasladoHelper claseDeMovimientoTrasladoHelper;
        private readonly TrasladoGenerator trasladoGenerator;
        private readonly ISettingsService settingsService;

        public CrearTrasladoViewModel(INavigationService navigationService, ClaseDeMovimientoTrasladoHelper claseDeMovimientoTrasladoHelper,
            TrasladoGenerator trasladoGenerator, ISettingsService settingsService)
        {
            this.navigationService = navigationService;
            this.claseDeMovimientoTrasladoHelper = claseDeMovimientoTrasladoHelper;
            this.trasladoGenerator = trasladoGenerator;
            this.settingsService = settingsService;
            Init();
        }
        #endregion

        private async void Init()
        {
            AplicarCommand = new Command(Aplicar);
            ClaseDeMovimiento = new ValidatableObject<string>();
            ListaClaseDeMovimiento = new ObservableRangeCollection<string>();
            FechaDocumento = new ValidatableObject<DateTime>();
            FechaContabilizacion = new ValidatableObject<DateTime>();
            claseDeMovimientoTrasladoPages = new Dictionary<string, Type>();
            setting = await settingsService.GetWithChildren();
            traslado = await trasladoGenerator.Generate(setting.UsuarioActivo.IdRed);
            Title = "Traslado " + Traslado.NumeroProvisorio;
            InitFechas();
            FillListaClaseDeMovimiento();
            AddValidations();
            InitMovimientosPages();
        }

        private void InitMovimientosPages()
        {
            claseDeMovimientoTrasladoPages.Add(ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_309], typeof(Traslado309PorMaterialView));
            claseDeMovimientoTrasladoPages.Add(ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_311], typeof(Traslado311PorMaterialView));
            claseDeMovimientoTrasladoPages.Add(ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_541], typeof(Traslado541PorMaterialView));
            claseDeMovimientoTrasladoPages.Add(ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_542], typeof(Traslado542PorMaterialView));
            claseDeMovimientoTrasladoPages.Add(ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_321], typeof(Traslado321PorMaterialView));
        }

        private void InitFechas()
        {
            FechaContabilizacion.Value = DateTime.Now;
            FechaDocumento.Value = DateTime.Now;
        }

        private void FillListaClaseDeMovimiento()
        {
            ListaClaseDeMovimiento.Clear();
            ListaClaseDeMovimiento.AddRange(claseDeMovimientoTrasladoHelper.GetClasesDeMovimientoTraslados());
        }

        private void Aplicar()
        {
            if (!Validate())
            {
                Toast.ShowMessage("Por favor, revise los campos ingresados.");
                return;
            }
            CompletarTraslado();
            Type type = claseDeMovimientoTrasladoPages[traslado.ClaseDeMovimientoCodigo];
            navigationService.PushFromAsync(typeof(CrearTrasladoView), type, traslado);
        }

        private void CompletarTraslado()
        {
            traslado.FechaContabilizacion = FechaContabilizacion.Value;
            traslado.FechaDocumento = FechaDocumento.Value;
            traslado.ClaseDeMovimientoCodigo = ClaseDeMovimiento.Value;
        }

        #region Validations

        private void AddValidations()
        {
            AddFechaDocumentoValidations();
            AddFechaContabilizacionValidations();
            AddClaseDeMovimientoValidations();
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
            bool isValidFechaContabilizacion = ValidateFechaContabilizacion();
            bool isValidFechaDocumento = ValidateFechaDocumento();
            bool isValidClaseDeMovimiento = ValidateClaseDeMovimiento();

            return isValidFechaContabilizacion && isValidFechaDocumento && isValidClaseDeMovimiento;
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
