using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.DetallesInventario;
using Frontend.Business.DetallesInventario.TiposStock;
using Frontend.Business.Inventarios;
using Frontend.Business.Stocks;
using Frontend.Commons.Enums;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.Areas.Inventarios.Models;
using Frontend.Core.Commons.Alerts;
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

namespace Frontend.Core.Areas.Inventarios.ViewModels
{
    public class RecuentoDetalleMaterialViewModel : BaseViewModel, IRecuentoDetalleMaterialViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IClaseDeValoracionService claseDeValoracionService;
        private readonly IInventarioService inventarioService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly ITipoStockService tipoStockService;
        private readonly IDetalleInventarioService detalleInventarioService;

        public ICommand GoToListCommand { get; set; }
        public ICommand GetClasesDeValoracionCommand { get; set; }
        private DetalleInventarioModel _detalleInventarioModel;
        public DetalleInventarioModel DetalleInventarioModel
        {
            get { return _detalleInventarioModel; }
            set
            {
                SetProperty(ref _detalleInventarioModel, value);
            }
        }
        public bool ShowComentario { get { return DetalleInventarioModel.ShowComentario; } }
        public DetalleInventario detalleInventario { get; set; }
        public Inventario inventario { get; set; }
        public ObservableRangeCollection<string> Unidades { get; set; }

        private ValidatableObject<double> _cantidad;
        public ValidatableObject<double> Cantidad
        {
            get { return _cantidad; }
            set
            {
                SetProperty(ref _cantidad, value);
            }
        }
        
        private ValidatableObject<string> _unidad;
        public ValidatableObject<string> Unidad
        {
            get { return _unidad; }
            set
            {
                SetProperty(ref _unidad, value);
            }
        }

        private TipoStock tipoStock;
        public TipoStock TipoStock
        {
            get { return tipoStock; }
            set
            {
                SetProperty(ref tipoStock, value);
            }
        }

        private int loteIndex;
        public int LoteIndex
        {
            get { return loteIndex; }
            set
            {
                SetProperty(ref loteIndex, value);
            }
        }


        private void FillUnidades()
        {
            Unidades.Clear();
            Unidades.Add(detalleInventario.Stock.Material.UnidadDeMedidaBase);
            if (!string.IsNullOrEmpty(detalleInventario.Stock.Material.UnidadDeMedidaAlternativa1))
            {
                Unidades.Add(detalleInventario.Stock.Material.UnidadDeMedidaAlternativa1);
            }
            if (!string.IsNullOrEmpty(detalleInventario.Stock.Material.UnidadDeMedidaAlternativa2))
            {
                Unidades.Add(detalleInventario.Stock.Material.UnidadDeMedidaAlternativa2);
            }
            if (!string.IsNullOrEmpty(detalleInventario.Stock.Material.UnidadDeMedidaAlternativa3))
            {
                Unidades.Add(detalleInventario.Stock.Material.UnidadDeMedidaAlternativa3);
            }
            if (!string.IsNullOrEmpty(detalleInventario.Stock.Material.UnidadDeMedidaAlternativa4))
            {
                Unidades.Add(detalleInventario.Stock.Material.UnidadDeMedidaAlternativa4);
            }
        }

        public RecuentoDetalleMaterialViewModel(INavigationService navigationService, IClaseDeValoracionService claseDeValoracionService,
            IInventarioService inventarioService, IDisplayAlertService displayAlertService, ITipoStockService tipoStockService,
            IDetalleInventarioService detalleInventarioService)
        {
            Title = "Detalle material";
            this.navigationService = navigationService;
            this.claseDeValoracionService = claseDeValoracionService;
            this.inventarioService = inventarioService;
            this.displayAlertService = displayAlertService;
            this.tipoStockService = tipoStockService;
            this.detalleInventarioService = detalleInventarioService;
            Init();
        }

        private void Init()
        {
            GoToListCommand = new Command(ConfirmarMaterial);
            DetalleInventarioModel = navigationService.GetNavigationParams<RecuentoDetalleMaterialView>() as DetalleInventarioModel;
            detalleInventario = DetalleInventarioModel.DetalleInventario;
            inventario = detalleInventario.Inventario;

            Unidades = new ObservableRangeCollection<string>();

            Cantidad = new ValidatableObject<double>();
            Unidad = new ValidatableObject<string>();

            Cantidad.Value = detalleInventario.CantidadContada;
            TipoStock = tipoStockService.GetById(detalleInventario.TipoStockId); 
            Unidad.Value = detalleInventario.UnidadAlmacen;
            AddValidations();

            FillUnidades();
        }

        private async void ConfirmarMaterial(object obj)
        {
            if (!Validate())
            {
                Toast.ShowMessage("El material ingresado contiene errores. Por favor, vuelva a ingresarlo");
                return;
            }
            await CompleteDetalleInventario();
            await detalleInventarioService.Update(detalleInventario);
            navigationService.PushFromAsync<HomeView, RecuentoDetalleInventarioView>(inventario);
        }

        private async Task CompleteDetalleInventario()
        {
            detalleInventario.CantidadContada = detalleInventario.Cantidad = Cantidad.Value;
            detalleInventario.UnidadAlmacen = Unidad.Value;
            detalleInventario.EsContado = true;
            detalleInventario.HayConteoErroneo = detalleInventario.Cantidad != GetCantidadStock(detalleInventario.Stock, detalleInventario.TipoStockId);
            detalleInventario.EstadoConteo = detalleInventario.HayConteoErroneo ? EstadoConteoEnum.Erroneo : EstadoConteoEnum.Completo;

            inventario = await inventarioService.GetInventarioById(detalleInventario.Inventario.Id);
        }

        private double GetCantidadStock(Stock stock, int tipoStockId)
        {
            return tipoStockId == 1 ? stock.CantidadAlmacen :
                       tipoStockId == 2 ? stock.CantidadBloqueado : stock.CantidadCalidad;
        }

        private void AddValidations()
        {
            Cantidad.Validations.Clear();

            Cantidad.Validations.Add(new IsGreaterEqualThanRule<double>
            {
                ValidationMessage = "La cantidad ingresada debe ser igual o mayor a 0.",
                Value = 0
            });
            Cantidad.Validations.Add(new IsNotNullOrEmptyRule<double>
            {
                ValidationMessage = "La cantidad es obligatoria."
            });
            Cantidad.Validations.Add(new RegularExpressionRule<double>
            {
                ValidationMessage = "Máximo 13 caracteres enteros y 3 decimales.",
                RegularExpression = @"^\d{1,13}((,|\.)\d{0,3}){0,1}$"
            });

            Unidad.Validations.Clear();
            Unidad.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "La unidad es obligatoria."
            });

        }

        private bool Validate()
        {
            bool isValidCantidad = ValidateCantidad();
            bool isValidUnidad = ValidateUnidad();
            return isValidCantidad && isValidUnidad;
        }

        public bool ValidateCantidad()
        {
            return Cantidad.Validate();
        }

        public bool ValidateUnidad()
        {
            return Unidad.Validate();
        }
    }
}
