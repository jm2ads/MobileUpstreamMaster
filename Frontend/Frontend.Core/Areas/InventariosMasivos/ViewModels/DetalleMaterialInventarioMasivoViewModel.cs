using Frontend.Business.Almacenes;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.DetallesInventario.TiposStock;
using Frontend.Business.InventariosMasivos;
using Frontend.Business.Settings;
using Frontend.Business.Stocks;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.InventariosMasivos.IViewModels;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.Commons.Validations;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.InventariosMasivos.ViewModels
{
    public class DetalleMaterialInventarioMasivoViewModel : BaseViewModel, IDetalleMaterialInventarioMasivoViewModel
    {
        private int _almacenIndex = -1;
        public int almacenIndex
        {
            get { return _almacenIndex; }
            set
            {
                SetProperty(ref _almacenIndex, value);
                RaisePropertyChanged("PepEditable");
            }
        }

        private int _claseDeValoracionIndex = -1;
        public int claseDeValoracionIndex
        {
            get { return _claseDeValoracionIndex; }
            set
            {
                SetProperty(ref _claseDeValoracionIndex, value);
                RaisePropertyChanged("PepEditable");
            }
        }

        private int _ubicacionIndex = -1;
        public int ubicacionIndex
        {
            get { return _ubicacionIndex; }
            set
            {
                SetProperty(ref _ubicacionIndex, value);
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

        public bool _ubicacionEditable;
        public bool UbicacionEditable
        {
            get { return _ubicacionEditable; }
            set
            {
                SetProperty(ref _ubicacionEditable, value);
            }
        }

        private bool _loteEditable = false;
        public bool LoteEditable
        {
            get { return _loteEditable; }
            set
            {
                SetProperty(ref _loteEditable, value);
            }
        }

        public bool _loteEmpty = true;
        public bool LoteEmpty
        {
            get { return _loteEmpty; }
            set
            {
                SetProperty(ref _loteEmpty, value);
            }
        }

        public ICommand ConfirmarCommand { get; set; }
        private InventarioMasivo inventarioMasivo;
        public ObservableRangeCollection<string> Unidades { get; set; }
        public DetalleInventarioMasivo detalleInventarioMasivo { get; set; }
        public IList<Stock> ListaStock { get; set; }

        #region Services
        private readonly INavigationService navigationService;
        private readonly IAlmacenService almacenService;
        private readonly IClaseDeValoracionService claseDeValoracionService;
        private readonly ISettingsService settingsService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly IStockService stockService;
        private readonly IDetalleInventarioMasivoService detalleInventarioMasivoService;
        private readonly IInventarioMasivoService inventarioMasivoService;
        private readonly ITipoStockService tipoStockService;
        #endregion
        #region ValidatableObjects

        private ValidatableObject<double> _cantidad;
        public ValidatableObject<double> cantidadEnviada
        {
            get { return _cantidad; }
            set
            {
                SetProperty(ref _cantidad, value);
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
        private ValidatableObject<string> _ubicacion;
        public ValidatableObject<string> ubicacion
        {
            get { return _ubicacion; }
            set
            {
                SetProperty(ref _ubicacion, value);
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

        private ValidatableObject<TipoStock> _tipoStock;
        public ValidatableObject<TipoStock> tipoStock
        {
            get { return _tipoStock; }
            set
            {
                SetProperty(ref _tipoStock, value);
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

        #endregion
        #region Observables

        public ObservableRangeCollection<Almacen> ListaAlmacen { get; set; }
        public ObservableRangeCollection<TipoStock> ListaTipoStock { get; set; }
        public ObservableRangeCollection<string> ListaUbicaciones { get; set; }
        public ObservableRangeCollection<ClaseDeValoracion> ListaClaseValoracion { get; set; }

        #endregion


        public DetalleMaterialInventarioMasivoViewModel(INavigationService navigationService, IDetalleInventarioMasivoService detalleInventarioMasivoService, IAlmacenService almacenService,
            IClaseDeValoracionService claseDeValoracionService, ISettingsService settingsService, IDisplayAlertService displayAlertService, IStockService stockService, IInventarioMasivoService inventarioMasivoService,
            ITipoStockService tipoStockService)
        {
            this.navigationService = navigationService;
            this.detalleInventarioMasivoService = detalleInventarioMasivoService;
            this.almacenService = almacenService;
            this.claseDeValoracionService = claseDeValoracionService;
            this.settingsService = settingsService;
            this.displayAlertService = displayAlertService;
            this.stockService = stockService;
            this.inventarioMasivoService = inventarioMasivoService;
            this.tipoStockService = tipoStockService;
            Init();
        }

        private void Init()
        {
            Title = "Detalle de material";
            detalleInventarioMasivo = navigationService.GetNavigationParams<DetalleMaterialInventarioMasivoView>() as DetalleInventarioMasivo;
            inventarioMasivo = detalleInventarioMasivo.InventarioMasivo;
            Unidades = new ObservableRangeCollection<string>();
            ConfirmarCommand = new Command(ConfirmarMaterial);

            ubicacion = new ValidatableObject<string>(async () => await UbicacionValueChanged());
            Unidad = new ValidatableObject<string>();
            almacen = new ValidatableObject<Almacen>();
            claseValoracion = new ValidatableObject<ClaseDeValoracion>(ClaseDeValoracionValueChanged);
            cantidadEnviada = new ValidatableObject<double>();
            tipoStock = new ValidatableObject<TipoStock>();

            ListaAlmacen = new ObservableRangeCollection<Almacen>();
            ListaClaseValoracion = new ObservableRangeCollection<ClaseDeValoracion>();
            ListaTipoStock = new ObservableRangeCollection<TipoStock>();
            ListaUbicaciones = new ObservableRangeCollection<string>();

            InitValores();

            var ret = InitAsync();
        }

        private void ClaseDeValoracionValueChanged()
        {
            if (claseValoracion.Value == null)
            {
                almacenIndex = -1;
                almacen.Value = null;
                LoteEmpty = true;
                return;
            }

            var stocks = ListaStock.Where(x => (string.IsNullOrWhiteSpace(ubicacion.Value) || x.Ubicacion == ubicacion.Value) && x.IdClaseDeValoracion == claseValoracion.Value.Id);

            ListaAlmacen.Clear();
            ListaAlmacen.AddRange(stocks.Select(stock => stock.Almacen));
            LoteEmpty = false;
        }

        private async Task UbicacionValueChanged()
        {
            almacenIndex = -1;
            claseDeValoracionIndex = -1;
            await FillClaseValoracion();
            RaisePropertyChanged("LoteEditable");
        }

        private void InitValores()
        {
            cantidadEnviada.Value = detalleInventarioMasivo.Cantidad;
            FillUnidades();
            FillTipoStock();
        }

        private void FillUnidades()
        {
            Unidades.Clear();
            Unidades.Add(detalleInventarioMasivo.Material.UnidadDeMedidaBase);
            if (!string.IsNullOrEmpty(detalleInventarioMasivo.Material.UnidadDeMedidaAlternativa1))
            {
                Unidades.Add(detalleInventarioMasivo.Material.UnidadDeMedidaAlternativa1);
            }
            if (!string.IsNullOrEmpty(detalleInventarioMasivo.Material.UnidadDeMedidaAlternativa2))
            {
                Unidades.Add(detalleInventarioMasivo.Material.UnidadDeMedidaAlternativa2);
            }
            if (!string.IsNullOrEmpty(detalleInventarioMasivo.Material.UnidadDeMedidaAlternativa3))
            {
                Unidades.Add(detalleInventarioMasivo.Material.UnidadDeMedidaAlternativa3);
            }
            if (!string.IsNullOrEmpty(detalleInventarioMasivo.Material.UnidadDeMedidaAlternativa4))
            {
                Unidades.Add(detalleInventarioMasivo.Material.UnidadDeMedidaAlternativa4);
            }

            Unidad.Value = detalleInventarioMasivo.Material.UnidadDeMedidaBase;
        }

        private async Task InitAsync()
        {
            await StartSpinner();
            setting = await settingsService.Get();
            await FillListaStock();
            FillListaUbicaciones();
            await FillListaAlmacen();

            await StopSpinner();
        }

        private async Task FillListaStock()
        {
            ListaStock = (await stockService.GetBy(detalleInventarioMasivo.IdMaterial)).Where(stock => (stock.DetalleStockEspecial.StockEspecial.Codigo == "S" || stock.DetalleStockEspecial.StockEspecial.Codigo == "Q") && ValidateCantidades(stock) ).ToList();

        }
        private bool ValidateCantidades(Stock stock)
        {
            return (stock.CantidadAlmacen > 0 || stock.CantidadBloqueado > 0 || stock.CantidadCalidad > 0);
        }

        private void FillListaUbicaciones()
        {
            ListaUbicaciones.AddRange(ListaStock.Where(s => !String.IsNullOrEmpty(s.Ubicacion)).Select(x => x.Ubicacion).Distinct().OrderBy(x => x).ToList());
            ubicacion.Value = string.IsNullOrWhiteSpace(detalleInventarioMasivo.Ubicacion) ? inventarioMasivo.Ubicacion : detalleInventarioMasivo.Ubicacion;
            ubicacionIndex = ListaUbicaciones.IndexOf(detalleInventarioMasivo.Ubicacion);
            UbicacionEditable = String.IsNullOrEmpty(inventarioMasivo.Ubicacion);
        }

        private async Task FillClaseValoracion()
        {
            LoteEditable = detalleInventarioMasivo.TipoLote == TipoLote.Usado;
            if (LoteEditable)
            {
                var lotes = await stockService.GetAllClaseDeValoracionBy(detalleInventarioMasivo.IdMaterial, ubicacion.Value);
                ListaClaseValoracion.Clear();
                var listaClaseDeValoracion = detalleInventarioMasivoService.LoteEditable(lotes, detalleInventarioMasivo.IdMaterial);
                ListaClaseValoracion.AddRange(listaClaseDeValoracion);
                claseValoracion.Value = detalleInventarioMasivo.Lote ?? detalleInventarioMasivo.Lote;
                claseDeValoracionIndex = ListaClaseValoracion.Select(x => x.Id).ToList().IndexOf(detalleInventarioMasivo.IdLote.GetValueOrDefault());
            }
        }

        private async Task FillListaAlmacen()
        {
            ListaAlmacen.AddRange(await inventarioMasivoService.GetAlmacenes(detalleInventarioMasivo.IdMaterial));
            almacenIndex = ListaAlmacen.Select(x => x.Id).ToList().IndexOf(detalleInventarioMasivo.IdAlmacen.GetValueOrDefault());
            almacen.Value = detalleInventarioMasivo.Almacen;
        }

        private void FillTipoStock()
        {
            ListaTipoStock.Clear();
            ListaTipoStock.AddRange(tipoStockService.GetAll());
            tipoStock.Value = tipoStockService.GetByCodigo(detalleInventarioMasivo.TipoStockId.ToString());
        }

        private async void ConfirmarMaterial()
        {
            AddValidations();

            if (!Validate())
            {
                Toast.ShowMessage("El material ingresado contiene errores. Por favor, vuelva a ingresarlo");
                return;
            }
            CompleteDetalleSalidaInterna();
            if (await ValidateDuplicado())
            {
                displayAlertService.Show("Aviso", "La cantidad indicada en este material se sumará al material con las mismas características ingresado previamente.", "Aceptar");
            }
            else
            {
                if (!inventarioMasivo.DetallesInventarioMasivo.Contains(detalleInventarioMasivo))
                {
                    inventarioMasivo.DetallesInventarioMasivo.Add(detalleInventarioMasivo);
                }
            }
            await inventarioMasivoService.Save(inventarioMasivo);
            navigationService.PushFromRootAsync(new List<Type>() { typeof(HomeView), typeof(InventarioMasivoView), typeof(ListadoPosicionesInventarioMasivoView) }, inventarioMasivo);
        }

        private async Task<bool> ValidateDuplicado()
        {
            return await detalleInventarioMasivoService.ValidateDuplicado(inventarioMasivo, detalleInventarioMasivo);
        }

        private void CompleteDetalleSalidaInterna()
        {
            detalleInventarioMasivo.Ubicacion = ubicacion.Value;
            detalleInventarioMasivo.Cantidad = cantidadEnviada.Value;
            detalleInventarioMasivo.IdAlmacen = almacen.Value?.Id;
            detalleInventarioMasivo.Almacen = almacen.Value;
            detalleInventarioMasivo.IdLote = claseValoracion.Value == null ? 0 : claseValoracion.Value.Id;
            detalleInventarioMasivo.Lote = claseValoracion.Value;
            detalleInventarioMasivo.Unidad = Unidad.Value;
            detalleInventarioMasivo.TipoStockId = tipoStock.Value.Id;

            var stocks = ListaStock.Where(s => (detalleInventarioMasivo.Almacen == null || s.IdAlmacen == detalleInventarioMasivo.IdAlmacen)).ToList();
            var stocks2 = stocks.Where(s => string.IsNullOrWhiteSpace(detalleInventarioMasivo.Ubicacion) || detalleInventarioMasivo.Ubicacion == s.Ubicacion).ToList();
            var stocks3 = stocks2.Where(s => (detalleInventarioMasivo.IdLote == 0 || s.IdClaseDeValoracion == detalleInventarioMasivo.IdLote)).ToList();
            var stocks4 = stocks3.Where(s => !inventarioMasivo.DetallesInventarioMasivo.Any(dim => dim.IdStock == s.Id && dim.TipoStockId == detalleInventarioMasivo.TipoStockId)).ToList();

            if (stocks4.Count() == 1)
            {
                var stock = stocks4.First();
                detalleInventarioMasivo.Stock = stock;
                detalleInventarioMasivo.IdStock = stock.Id;
            }
        }

        #region Validaciones
        private void AddValidations()
        {
            AddUbicacionValidator();
            AddUnidadValidator();
            AddClaseDeValoracionValidators();
            AddAlmacenValidators();
            AddCantidadValidators();
            AddTipoStockValidators();
        }
        private void AddUbicacionValidator()
        {
            ubicacion.Validations.Clear();
        }
        private void AddUnidadValidator()
        {
            Unidad.Validations.Clear();
            Unidad.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "La unidad es obligatoria."
            });
        }
        private void AddAlmacenValidators()
        {
            if (LoteEditable)
            {
                almacen.Validations.Clear();
                almacen.Validations.Add(new IsNotNullOrEmptyRule<Almacen>
                {
                    ValidationMessage = "El almacén es obligatorio."
                });
            }
        }
        private void AddClaseDeValoracionValidators()
        {
            if (LoteEditable)
            {
                claseValoracion.Validations.Clear();
                claseValoracion.Validations.Add(new IsNotNullOrEmptyRule<ClaseDeValoracion>
                {
                    ValidationMessage = "La clase de valoración es obligatoria."
                });
            }
        }
        private void AddTipoStockValidators()
        {
            tipoStock.Validations.Clear();
            tipoStock.Validations.Add(new IsNotNullOrEmptyRule<TipoStock>
            {
                ValidationMessage = "El tipo de stock es obligatorio."
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
                ValidationMessage = "La cantidad ingresada debe ser mayor a 0.",
                Value = 1
            });
        }
        private bool Validate()
        {
            bool isValidUbicacion = ValidateUbicacion();
            bool isValidCantidad = ValidateCantidad();
            bool isValidAlmacen = ValidateAlmacen();
            bool isValidClaseDeValoracion = ValidateClaseDeValoracion();
            bool isValidUnidad = ValidateUnidad();
            bool isValidTipoStock = ValidateTipoStock();

            return isValidUbicacion
                && isValidCantidad
                && isValidAlmacen
                && isValidAlmacen
                && isValidUnidad
                && isValidClaseDeValoracion
                && isValidTipoStock;
        }
        public bool ValidateUbicacion()
        {
            return ubicacion.Validate();
        }
        public bool ValidateUnidad()
        {
            return Unidad.Validate();
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
        public bool ValidateTipoStock()
        {
            return tipoStock.Validate();
        }
        #endregion
    }
}
