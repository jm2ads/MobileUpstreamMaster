using Frontend.Business.Almacenes;
using Frontend.Business.Materiales;
using Frontend.Business.Synchronizer;
using Frontend.Core.Areas.CambiosUbicacion.IViewModels;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.Commons.Validations;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.CambiosUbicacion.ViewModels
{
    public class CrearCambioUbicacionViewModel : BaseViewModel, ICrearCambioUbicacionViewModel
    {
        #region Properties

        private readonly IAlmacenService almacenService;
        private readonly IStockService stockService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly ICambioUbicacionService cambioUbicacionService;
        private readonly INavigationService navigationService;
        private readonly IMaterialService materialService;
        private readonly ILecturaQRService lecturaQRService;
        private readonly ISettingsService settingsService;

        public ICommand AplicarCommand { get; set; }
        public ICommand SeleccionarAlmacenesCommand { get; set; }

        private Business.CambiosUbicacion.CambioUbicacion _cambioUbicacion;
        public Business.CambiosUbicacion.CambioUbicacion cambioUbicacion
        {
            get { return _cambioUbicacion; }
            set
            {
                SetProperty(ref _cambioUbicacion, value);
            }
        }

        #region Almacen

        private string _almacenesIncluidos;
        public string AlmacenesIncluidos
        {
            get { return _almacenesIncluidos; }
            set
            {
                SetProperty(ref _almacenesIncluidos, value);
            }
        }

        #endregion

        #region Materiales

        private IList listMaterialesFull;
        public IList ListMaterialesFull
        {
            get { return listMaterialesFull; }
            set
            {
                SetProperty(ref listMaterialesFull, value);
            }
        }

        private IList listDescripcionMateriales;
        public IList ListDescripcionMateriales
        {
            get { return listDescripcionMateriales; }
            set
            {
                SetProperty(ref listDescripcionMateriales, value);
            }
        }

        private IList listaFiltros;
        public IList ListaFiltros
        {
            get { return listaFiltros; }
            set
            {
                SetProperty(ref listaFiltros, value);
            }
        }

        private string filtro;
        public string Filtro
        {
            get { return filtro; }
            set
            {
                SetProperty(ref filtro, value);
                SearchByCodigo = value == "Código";
                SearchValue = string.Empty;
            }
        }

        private bool searchByCodigo;
        public bool SearchByCodigo
        {
            get { return searchByCodigo; }
            set
            {
                SetProperty(ref searchByCodigo, value);
                OnPropertyChanged("SearchValue");
            }
        }

        private string searchValue;
        public string SearchValue
        {
            get
            {
                return searchValue;
            }
            set
            {
                SetProperty(ref searchValue, value);
            }
        }

        private bool materialEnabled;
        public bool MaterialEnabled
        {
            get
            {
                return materialEnabled;
            }
            set
            {
                SetProperty(ref materialEnabled, value);
            }
        }

        public IList<Tuple<string, int>> CodigoMaterialList { get; set; }
        public IList<Tuple<string, int>> DescripcionMaterialList { get; set; }

        private IList<Material> ListaMateriales { get; set; }

        #endregion

        #region Ubicacion

        private IList listUbicacionesFull;
        public IList ListUbicacionesFull
        {
            get { return listUbicacionesFull; }
            set
            {
                SetProperty(ref listUbicacionesFull, value);
            }
        }

        private ValidatableObject<string> _ubicacion;
        public ValidatableObject<string> ubicacion
        {
            get { return _ubicacion; }
            set
            {
                SetProperty(ref _ubicacion, value);
            }
        }

        #endregion

        #endregion

        #region Methods

        public CrearCambioUbicacionViewModel(IAlmacenService almacenService, IStockService stockService,
            IDisplayAlertService displayAlertService, ICambioUbicacionService cambioUbicacionService,
            INavigationService navigationService, IMaterialService materialService, ILecturaQRService lecturaQRService,
            ISettingsService settingsService)
        {
            this.almacenService = almacenService;
            this.stockService = stockService;
            this.cambioUbicacionService = cambioUbicacionService;
            this.displayAlertService = displayAlertService;
            this.navigationService = navigationService;
            this.materialService = materialService;
            this.lecturaQRService = lecturaQRService;
            this.settingsService = settingsService;
            Init();
        }

        private async void Init()
        {
            AplicarCommand = new Command(async () => await Aplicar());
            SeleccionarAlmacenesCommand = new Command(SeleccionarAlmacenes);
            ubicacion = new ValidatableObject<string>();
            CodigoMaterialList = new List<Tuple<string, int>>();
            DescripcionMaterialList = new List<Tuple<string, int>>();
            Title = "Cambio de ubicación";
            cambioUbicacion = await cambioUbicacionService.Create();
            MaterialEnabled = false;
            InitFiltro();

            await GetAllMaterialAndUbicacion();
            AddValidations();
        }

        public async Task ActualizarAlmacenes()
        {
            await StartSpinner();
            ListaMateriales = await stockService.GetAllMaterialBy(cambioUbicacion.IdCentro, cambioUbicacion.AlmacenesIncluidos.Select(x => x.Id).ToList());
            ListMaterialesFull = ListaMateriales.Select(x => x.Codigo.TrimStart('0')).ToList();
            ListDescripcionMateriales = ListaMateriales.Select(x => x.Descripcion).ToList();

            var listUbicacion = await stockService.GetAllUbicaciones(cambioUbicacion.IdCentro);
            ListUbicacionesFull = listUbicacion.ToList();

            SearchValue = string.Empty;
            MaterialEnabled = cambioUbicacion.AlmacenesIncluidos.Count != 0;

            AlmacenesIncluidos = cambioUbicacion.AlmacenesIncluidos.Count == 0 ? "No hay almacenes seleccionados" : "Almacenes seleccionados: " + String.Join(Environment.NewLine, cambioUbicacion.AlmacenesIncluidos.Select(x => x.DisplayDescription));

            await StopSpinner();
        }

        private async Task GetAllMaterialAndUbicacion()
        {
            var listMaterial = await stockService.GetAllMaterial();
            ListMaterialesFull = listMaterial.Select(x => x.Codigo.TrimStart('0')).ToList();
            ListDescripcionMateriales = listMaterial.Select(x => x.Descripcion).ToList();

            var listUbicacion = await stockService.GetAllUbicaciones(cambioUbicacion.IdCentro);
            ListUbicacionesFull = listUbicacion.ToList();

        }

        private void InitFiltro()
        {
            ListaFiltros = new List<string>();
            ListaFiltros.Add("Código");
            ListaFiltros.Add("Texto corto");
            Filtro = "Código";
        }

        private async Task Aplicar()
        {
            if (!Validate())
            {
                Toast.ShowMessage("Por favor, revise los campos ingresados.");
                return;
            }

            await CrearCambioDeUbicacion();
        }

        private async Task CrearCambioDeUbicacion()
        {
            try
            {
                var material = SearchByCodigo ?
                    await materialService.GetByCodigo((await lecturaQRService.GetLecturaQR(SearchValue)).CodigoMaterial) :
                    await materialService.GetByDescripcion(SearchValue);

                if (cambioUbicacion.AlmacenesIncluidos.Count == 0)
                {
                    Toast.ShowMessage("Debe seleccionar al menos un almacén para continuar.");
                    return;
                }

                if (material == null || !ListaMateriales.Contains(material))
                {
                    Toast.ShowMessage("Código o texto corto de material no válido. Por favor, vuelva a ingresarlo.");
                    return;
                }

                var answer = await displayAlertService.Show("Finalizar cambio ubicación", "¿Desea finalizar el cambio de ubicación?", "Aceptar", "Cancelar");
                if (answer)
                {
                    cambioUbicacion.Material = material;
                    cambioUbicacion.IdMaterial = material.Id;
                    cambioUbicacion.Ubicacion = ubicacion.Value.ToUpper();
                    cambioUbicacion.SyncState = SyncState.PendingToSync;

                    await cambioUbicacionService.Insert(cambioUbicacion);
                    await settingsService.SetPendingToSync(true);

                    #region ASOSA flagSync Cambio de ubicación
                    Frontend.Core.Commons.Globals.flagSync = "CrearCambioUbicacionViewModel";
                    #endregion

                    Toast.ShowMessage("El cambio de ubicación ha sido ingresado con éxito");
                    navigationService.PushFromRootAsync<HomeView>();
                }
            }
            catch (System.Exception)
            {
                Toast.ShowMessage("Código o texto corto de material no válido. Por favor, vuelva a ingresarlo.");
            }
        }

        private async void SeleccionarAlmacenes()
        {
            await navigationService.PushModalAsync<SeleccionarAlmacenesView>(cambioUbicacion);
        }

        #region Validations

        private void AddValidations()
        {
            AddAlmacenValidations();
            AddUbicacionValidations();
        }

        private void AddAlmacenValidations()
        {
            //validar lista de almacenes
        }

        private void AddUbicacionValidations()
        {
            ubicacion.Validations.Clear();
            ubicacion.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "La ubicación es obligatoria."
            });
            ubicacion.Validations.Add(new IsLengthLowerOrEqualThanRule<string>
            {
                ValidationMessage = "La longitud no debe superar los 10 caracteres.",
                Value = 10
            });
        }

        private bool Validate()
        {
            bool isValidAlmacen = ValidateAlmacen();
            bool isValidUbicacion = ValidateUbicacion();
            return isValidAlmacen && isValidUbicacion;
        }

        public bool ValidateAlmacen()
        {
            return true;
        }

        public bool ValidateUbicacion()
        {
            return ubicacion.Validate();
        }

        #endregion

        #endregion

    }
}
