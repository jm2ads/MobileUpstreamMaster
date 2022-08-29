using Frontend.Business.Almacenes;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.Movimientos.Ingresos;
using Frontend.Business.StocksEspeciales;
using Frontend.Core.Areas.Movimientos.Ingresos.IViewModels;
using Frontend.Core.Areas.Views;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.Commons.Validations;
using Frontend.Core.Resources;
using Frontend.Core.ViewModels;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Movimientos.Ingresos.ViewModels
{
    public class DetalleDeMaterialPedidoViewModel : BaseViewModel, IDetalleDeMaterialPedidoViewModel
    {

        private readonly INavigationService navigationService;
        private readonly IClaseDeValoracionService claseDeValoracionService;
        private readonly IAlmacenService almacenService;
        private readonly ISettingsService settingsService;
        private readonly IStockEspecialService stockEspecialService;
        private readonly INotaDeEntregaService notaDeEntregaService;
        private readonly IStockService stockService;

        public ICommand ConfirmarMaterialCommand { get; set; }

        private DetalleNotaDeEntregaPosicion detalleNotaDeEntregaPosicion;
        public DetalleNotaDeEntregaPosicion DetalleNotaDeEntregaPosicion
        {
            get { return detalleNotaDeEntregaPosicion; }
            set
            {
                SetProperty(ref detalleNotaDeEntregaPosicion, value);
            }
        }

        public DetallePedido detallePedido { get; set; }

        public Pedido pedido { get; set; }

        private string destinatarioMercancia;
        public string DestinatarioMercancia
        {
            get { return destinatarioMercancia; }
            set
            {
                SetProperty(ref destinatarioMercancia, value);
            }
        }

        private string puestoDeDescarga;
        public string PuestoDeDescarga
        {
            get { return puestoDeDescarga; }
            set
            {
                SetProperty(ref puestoDeDescarga, value);
            }
        }

        private bool entregaFinal;
        public bool EntregaFinal
        {
            get { return entregaFinal; }
            set
            {
                SetProperty(ref entregaFinal, value);
            }
        }

        public bool AlmacenEditable
        {
            get { return DetalleNotaDeEntregaPosicion.DetallePedidoPosicion.DetallePedido.Almacen == null; }
        }

        public bool TipoStockEditable
        {
            get { return DetalleNotaDeEntregaPosicion.ClaseDeMovimientoCodigo == ClaseDeMovimientoPedido.ClaseDeMovimiento[ClaseDeMovimientoPedido.CLASE_103]; }
        }

        public bool StockEspecialEditable
        {
            get { return DetalleNotaDeEntregaPosicion.DetallePedidoPosicion.DetallePedido.StockEspecial == null; }
        }

        public bool ClaseDeValoracionEditable
        {
            get { return DetalleNotaDeEntregaPosicion.DetallePedidoPosicion.DetallePedido.ClaseDeValoracion == null; }
        }

        private ValidatableObject<ClaseDeValoracion> claseDeValoracion;
        public ValidatableObject<ClaseDeValoracion> ClaseDeValoracion
        {
            get { return claseDeValoracion; }
            set { SetProperty(ref claseDeValoracion, value); }
        }

        private ValidatableObject<StockEspecial> stockEspecial;
        public ValidatableObject<StockEspecial> StockEspecial
        {
            get { return stockEspecial; }
            set { SetProperty(ref stockEspecial, value); }
        }

        private int _stockEspecialIndex = -1;
        public int stockEspecialIndex
        {
            get { return _stockEspecialIndex; }
            set
            {
                SetProperty(ref _stockEspecialIndex, value);
            }
        }

        private ValidatableObject<Almacen> almacen;
        public ValidatableObject<Almacen> Almacen
        {
            get { return almacen; }
            set { SetProperty(ref almacen, value); }
        }

        private ValidatableObject<double> cantidadPedida;
        public ValidatableObject<double> CantidadPedida
        {
            get { return cantidadPedida; }
            set { SetProperty(ref cantidadPedida, value); }
        }

        private ValidatableObject<string> tipoStock;
        public ValidatableObject<string> TipoStock
        {
            get { return tipoStock; }
            set { SetProperty(ref tipoStock, value); }
        }

        private string _textoPosicion;
        public string textoPosicion
        {
            get { return _textoPosicion; }
            set
            {
                SetProperty(ref _textoPosicion, value);
            }
        }

        private string _ubicacion;
        public string Ubicacion
        {
            get { if (String.IsNullOrEmpty(_ubicacion)) return "-"; else return _ubicacion ; }
            set
            {
                SetProperty(ref _ubicacion, value);
            }
        }

        public ObservableRangeCollection<StockEspecial> ListaStockEspecial { get; set; }

        public ObservableRangeCollection<ClaseDeValoracion> ListaClaseDeValoracion { get; set; }

        public ObservableRangeCollection<Almacen> ListaAlmacen { get; set; }

        public ObservableRangeCollection<string> ListaTipoStock { get; set; }

        private IDictionary<string, string> tipoStockDetalleNotaDeEntrega;

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

        public DetalleDeMaterialPedidoViewModel(INavigationService navigationService, IClaseDeValoracionService claseDeValoracionService
                                                , IAlmacenService almacenService, ISettingsService settingsService, IStockEspecialService stockEspecialService, INotaDeEntregaService notaDeEntregaService, IStockService stockService)
        {
            this.navigationService = navigationService;
            this.claseDeValoracionService = claseDeValoracionService;
            this.almacenService = almacenService;
            this.settingsService = settingsService;
            this.stockEspecialService = stockEspecialService;
            this.notaDeEntregaService = notaDeEntregaService;
            this.stockService = stockService;
            Init();
        }

        private async void Init()
        {
            tipoStockDetalleNotaDeEntrega = new TipoStockDetalleNotaDeEntrega().TipoStock;
            CantidadPedida = new ValidatableObject<double>();
            StockEspecial = new ValidatableObject<StockEspecial>();
            TipoStock = new ValidatableObject<string>();
            ListaClaseDeValoracion = new ObservableRangeCollection<ClaseDeValoracion>();
            ListaTipoStock = new ObservableRangeCollection<string>();
            ListaAlmacen = new ObservableRangeCollection<Almacen>();
            ListaStockEspecial = new ObservableRangeCollection<StockEspecial>();
            ConfirmarMaterialCommand = new Command(ConfirmarMaterial);
            ClaseDeValoracion = new ValidatableObject<ClaseDeValoracion>(async () => await LoteValueChanged());
            Almacen = new ValidatableObject<Almacen>(async () => await AlmacenValueChanged());
            DetalleNotaDeEntregaPosicion = navigationService.GetNavigationParams<DetalleDeMaterialPedidoView>() as DetalleNotaDeEntregaPosicion;
            Title = "Detalle de material";
            FillListaTipoStock();
            detallePedido = DetalleNotaDeEntregaPosicion.DetallePedidoPosicion.DetallePedido;
            PuestoDeDescarga = DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.PuestoDeDescarga;
            DestinatarioMercancia = DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.DestinatarioMercancia;
            EntregaFinal = DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.EntregaFinal;
            textoPosicion = DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.TextoPosicion;
            await InitAsync();
            await MostrarUbicacion();
            CantidadPedida.Value = DetalleNotaDeEntregaPosicion.CantidadRecibida;
        }

        private async Task LoteValueChanged()
        {
            if (Almacen.Value != null && ClaseDeValoracion.Value != null)
            {
                await MostrarUbicacion();
            }
        }

        private async Task MostrarUbicacion()
        {
            try
            {
                Ubicacion = await stockService.GetUbicacionBy(detallePedido.CentroId, detallePedido.MaterialId, Almacen.Value.Id, ClaseDeValoracion.Value.Id);
            }
            catch(Exception e)
            {
                Ubicacion = "-";
            }
        }

        private async Task AlmacenValueChanged()
        {
            if (Almacen.Value != null && ClaseDeValoracion.Value != null)
            {
                await MostrarUbicacion();
            }
        }

        private async Task InitAsync()
        {
            await FillListaClaseDeValoracion();
            await FillListaAlmacen();
            await FillListaStockEspecial();
            AddValidations();
        }

        private async void ConfirmarMaterial(object obj)
        {
            CheckEntregaFinal();
            if (!Validate())
            {
                Toast.ShowMessage("El material ingresado contiene errores. Por favor, vuelva a ingresarlo");
                return;
            }
            await CompleteDetalle();
            navigationService.PopAsync<DetalleDeMaterialPedidoView>();
        }

        private void CheckEntregaFinal()
        {
            CantidadPedida.Validations.Clear();
            CantidadPedida.Validations.Add(new IsNotNullOrEmptyRule<double>
            {
                ValidationMessage = "La cantidad es obligatoria."
            });
            CantidadPedida.Validations.Add(new RegularExpressionRule<double>
            {
                ValidationMessage = "Máximo 13 caracteres enteros y 3 decimales.",
                RegularExpression = @"^-?\d{1,13}((,|\.)\d{0,3}){0,1}$"
            });
            CantidadPedida.Validations.Add(new IsLowerEqualThanRule<double>
            {
                ValidationMessage = "No se puede ingresar más que lo pedido.",
                Value = DetalleNotaDeEntregaPosicion.DetallePedidoPosicion.CantidadPendiente * ((DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.DetallePedido.Tolerancia / 100) + 1)
            });

            if (EntregaFinal)
            {
                CantidadPedida.Validations.Add(new IsGreaterEqualThanRule<double>
                {
                    ValidationMessage = "La cantidad ingresada debe ser mayor o igual a 0.",
                    Value = 0
                });
            }
            else
            {
                CantidadPedida.Validations.Add(new IsGreaterThanRule<double>
                {
                    ValidationMessage = "La cantidad ingresada debe ser mayor a 0.",
                    Value = 0
                });
            }
        }

        private async Task CompleteDetalle()
        {
            DetalleNotaDeEntregaPosicion.CantidadRecibida = CantidadPedida.Value;
            DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.ClaseDeValoracion = ClaseDeValoracion.Value;
            DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.ClaseDeValoracionId = ClaseDeValoracion.Value.Id;
            DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.StockEspecial = StockEspecial.Value;
            DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.StockEspecialId = StockEspecial.Value.Id;
            DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.Almacen = Almacen.Value;
            DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.AlmacenId = Almacen.Value.Id;
            DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.TipoStockId = tipoStockDetalleNotaDeEntrega.FirstOrDefault(x => x.Value == TipoStock.Value).Key;
            DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.TextoPosicion = textoPosicion;
            DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.PuestoDeDescarga = puestoDeDescarga;
            DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.DestinatarioMercancia = destinatarioMercancia;
            DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.EntregaFinal = EntregaFinal;
            DetalleNotaDeEntregaPosicion.EsContado = true;
            await notaDeEntregaService.Update(DetalleNotaDeEntregaPosicion);
        }

        private async Task FillListaClaseDeValoracion()
        {
            var clases = await this.claseDeValoracionService.GetAll();
            ListaClaseDeValoracion.AddRange(clases);
            ClaseDeValoracionIndex = ListaClaseDeValoracion.Select(x => x.Id).ToList().IndexOf(detalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.ClaseDeValoracionId.GetValueOrDefault());

        }

        private async Task FillListaAlmacen()
        {
            var setting = await settingsService.GetWithChildren();
            var almacenes = await this.almacenService.GetByIdCentro(setting.CentroActivoId);
            ListaAlmacen.AddRange(almacenes);
            AlmacenIndex = ListaAlmacen.Select(x => x.Id).ToList().IndexOf(detalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.AlmacenId.GetValueOrDefault());

        }

        private void FillListaTipoStock()
        {
            var list = new List<string>();
            list.Add(tipoStockDetalleNotaDeEntrega[TipoStockDetalleNotaDeEntrega.LIBRE_UTILIZACION]);
            list.Add(tipoStockDetalleNotaDeEntrega[TipoStockDetalleNotaDeEntrega.CONTROL_DE_CALIDAD]);
            list.Add(tipoStockDetalleNotaDeEntrega[TipoStockDetalleNotaDeEntrega.BLOQUEADO]);
            ListaTipoStock.AddRange(list);
            var stockId = DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.TipoStockId;
            TipoStock.Value = tipoStockDetalleNotaDeEntrega[stockId];

        }

        private async Task FillListaStockEspecial()
        {
            if (StockEspecialEditable)
            {
                var list = await stockEspecialService.GetAll();
                ListaStockEspecial.AddRange(list);
                stockEspecialIndex = ListaStockEspecial.Select(x => x.Id).ToList().IndexOf(DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.StockEspecialId);
            }
            else
            {
                StockEspecial.Value = DetalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.StockEspecial;
            }
        }

        #region Validations

        private void AddValidations()
        {
            AddAlmacenValidations();
            AddClaseDeValoracionValidations();
            AddTipoStockValidations();
            AddStockEspecialValidations();
        }

        private void AddAlmacenValidations()
        {
            Almacen.Validations.Clear();
            Almacen.Validations.Add(new IsNotNullOrEmptyRule<Almacen>
            {
                ValidationMessage = MessageText.FieldRequired
            });
        }
        private void AddClaseDeValoracionValidations()
        {
            ClaseDeValoracion.Validations.Clear();
            ClaseDeValoracion.Validations.Add(new IsNotNullOrEmptyRule<ClaseDeValoracion>
            {
                ValidationMessage = MessageText.FieldRequired
            });
        }
        private void AddTipoStockValidations()
        {
            TipoStock.Validations.Clear();
            TipoStock.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = MessageText.FieldRequired
            });
        }
        private void AddStockEspecialValidations()
        {
            StockEspecial.Validations.Clear();
            StockEspecial.Validations.Add(new IsNotNullOrEmptyRule<StockEspecial>
            {
                ValidationMessage = MessageText.FieldRequired
            });
        }

        public bool ValidateCantidadPendiente()
        {
            return CantidadPedida.Validate();
        }
        public bool ValidateClaseDeValoracion()
        {
            return ClaseDeValoracion.Validate();
        }
        public bool ValidateTipoStock()
        {
            return TipoStock.Validate();
        }
        public bool ValidateStockEspecial()
        {
            return StockEspecial.Validate();
        }
        public bool ValidateAlmacen()
        {
            return Almacen.Validate();
        }

        private bool Validate()
        {
            bool isValidCantidadPendiente = ValidateCantidadPendiente();
            bool isValidClaseDeValoracion = ValidateClaseDeValoracion();
            bool isValidAlmacen = ValidateAlmacen();
            bool isValidTipoStock = ValidateTipoStock();
            bool isValidStockEspecial = ValidateStockEspecial();
            return isValidCantidadPendiente && isValidAlmacen && isValidClaseDeValoracion && isValidStockEspecial && isValidTipoStock;
        }

        #endregion
    }
}
