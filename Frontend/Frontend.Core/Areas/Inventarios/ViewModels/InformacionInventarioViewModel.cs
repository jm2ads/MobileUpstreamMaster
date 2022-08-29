using Frontend.Business.Almacenes;
using Frontend.Business.Inventarios;
using Frontend.Business.StocksEspeciales;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.Commons.Alerts;
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

namespace Frontend.Core.Areas.Inventarios.ViewModels
{
    public class InformacionInventarioViewModel : BaseViewModel, IInformacionInventarioViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IInventarioService inventarioService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly IStockEspecialService stockEspecialService;
        private readonly IAlmacenService almacenService;

        public ICommand CrearInventarioCommand { get; set; }

        private Inventario _inventario;
        public Inventario inventario
        {
            get { return _inventario; }
            set
            {
                SetProperty(ref _inventario, value);
            }
        }


        public bool _isAlmacenEnabled;

        public bool IsAlmacenEnabled
        {
            get { return _isAlmacenEnabled; }
            set
            {
                SetProperty(ref _isAlmacenEnabled, value);
            }
        }

        public ObservableRangeCollection<StockEspecial> ListaStockEspecial { get; set; }

        private ValidatableObject<StockEspecial> _stockEspecial;
        public ValidatableObject<StockEspecial> StockEspecial
        {
            get { return _stockEspecial; }
            set
            {
                SetProperty(ref _stockEspecial, value);
            }
        }

        private ObservableRangeCollection<Almacen> listaAlmacen;
        public ObservableRangeCollection<Almacen> ListaAlmacen
        {
            get { return listaAlmacen; }
            set { SetProperty(ref listaAlmacen, value); }
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

        private string _fechaCreacion;
        public string FechaCreacion
        {
            get { return _fechaCreacion; }
            set
            {
                SetProperty(ref _fechaCreacion, value);
            }
        }

        private string _fechaRecuento;
        public string FechaRecuento
        {
            get { return _fechaRecuento; }
            set
            {
                SetProperty(ref _fechaRecuento, value);
            }
        }

        public InformacionInventarioViewModel(INavigationService navigationService, IInventarioService inventarioService,
            IDisplayAlertService displayAlertService, IStockEspecialService stockEspecialService, IAlmacenService almacenService)
        {
            this.navigationService = navigationService;
            this.inventarioService = inventarioService;
            this.displayAlertService = displayAlertService;
            this.stockEspecialService = stockEspecialService;
            this.almacenService = almacenService;
            Init();
            var init = GetInventario();
        }

        private async Task GetInventario()
        {
            inventario = await inventarioService.Create();
            Title = "Inventario " + inventario.Codigo;
            StockEspecial.Value = inventario.StockEspecial;
            FechaRecuento = inventario.FechaRecuento == new DateTime() ? " - " : inventario.FechaRecuento.ToString("dd/MM/yyyy");
            FechaCreacion = inventario.FechaCreacion.ToString("dd/MM/yyyy");
            AddValidations();
            await FillListaStockEspecial();
            await FillListaAlmacen();
        }

        private void Init()
        {

            ListaStockEspecial = new ObservableRangeCollection<StockEspecial>();
            ListaAlmacen = new ObservableRangeCollection<Almacen>();

            StockEspecial = new ValidatableObject<StockEspecial>(StockEspecialValueChanged);
            almacen = new ValidatableObject<Almacen>();

            CrearInventarioCommand = new Command(async () => await CrearInventario());
        }

        private void StockEspecialValueChanged()
        {
            if (StockEspecial.Value?.Codigo == "O")
            {
                IsAlmacenEnabled = false;
                almacen.Validations.Clear();
                almacen.Value = null;
            }
            else
            {
                IsAlmacenEnabled = true;
                AddAlmacenValidations();
            }
        }

        private async Task CrearInventario()
        {
            if (!Validate())
            {
                Toast.ShowMessage("El inventario contiene errores. Por favor, vuelva a ingresarlo");
                return;
            }
            await CompletarInventario();
            navigationService.PushAsync<InformacionInventarioView, SearchMaterialView>(inventario);
        }

        private async Task CompletarInventario()
        {
            inventario.IdStockEspecial = StockEspecial.Value.Id;
            inventario.StockEspecial = StockEspecial.Value;
            inventario.IdAlmacen = almacen.Value?.Id;
            inventario.Almacen = almacen.Value ?? almacen.Value;
        }

        private async Task FillListaStockEspecial()
        {
            ListaStockEspecial.Clear();
            ListaStockEspecial.AddRange(await stockEspecialService.GetAll());
        }

        private async Task FillListaAlmacen()
        {
            ListaAlmacen.AddRange(await almacenService.GetByIdCentro(inventario.IdCentro));
        }

        #region Validations

        private void AddValidations()
        {
            AddStockEspecialValidations();
            AddAlmacenValidations();
        }

        private void AddStockEspecialValidations()
        {
            StockEspecial.Validations.Clear();
            StockEspecial.Validations.Add(new IsNotNullOrEmptyRule<StockEspecial>
            {
                ValidationMessage = "El stock especial es obligatorio."
            });
        }

        private void AddAlmacenValidations()
        {
            almacen.Validations.Clear();
            almacen.Validations.Add(new IsNotNullOrEmptyRule<Almacen>
            {
                ValidationMessage = "El almacén especial es obligatorio."
            });
        }

        private bool Validate()
        {
            bool isValidStockEspecial = ValidateStockEspecial();
            bool isValidAlmacen = ValidateAlmacen();
            return isValidStockEspecial && isValidAlmacen;
        }

        public bool ValidateStockEspecial()
        {
            return StockEspecial.Validate();
        }

        public bool ValidateAlmacen()
        {
            return almacen.Validate();
        }
        #endregion
    }
}
