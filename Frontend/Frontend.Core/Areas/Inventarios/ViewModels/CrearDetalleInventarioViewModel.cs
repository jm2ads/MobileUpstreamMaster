using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.Commons;
using Frontend.Business.DetallesInventario;
using Frontend.Business.DetallesInventario.TiposStock;
using Frontend.Business.Inventarios;
using Frontend.Business.InventariosLocales;
using Frontend.Business.Materiales;
using Frontend.Business.Stocks;
using Frontend.Commons.Enums;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Inventarios.Models;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.Commons.Validations;
using Frontend.Core.IViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.ViewModels
{
    public class CrearDetalleInventarioViewModel : BaseViewModel, ICrearDetalleInventarioViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IClaseDeValoracionService claseDeValoracionService;
        private readonly IInventarioService inventarioService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly ITipoStockService tipoStockService;
        private readonly IDetalleInventarioService detalleInventarioService;
        private readonly IInventarioLocalService inventarioLocalService;
        private readonly IStockService stockService;

        public ICommand GoToListCommand { get; set; }



        private DetalleInventarioModel _detalleInventarioModel;
        public DetalleInventarioModel DetalleInventarioModel
        {
            get { return _detalleInventarioModel; }
            set
            {
                SetProperty(ref _detalleInventarioModel, value);
            }
        }
        public bool ShowComentario { get { return DetalleInventarioModel.ShowComentario && !String.IsNullOrEmpty(detalleInventario.Comentario); } }

        public DetalleInventario detalleInventario { get; set; }
        public Inventario inventario { get; set; }
        public IList<Stock> StocksDisponibles { get; private set; }
        public ObservableRangeCollection<ClaseDeValoracion> ListaClaseDeValoracion { get; set; }
        public ObservableRangeCollection<string> Unidades { get; set; }
        public ObservableRangeCollection<TipoStock> TiposStock { get; set; }
        public ObservableRangeCollection<string> EstadosInventario { get; set; }
        public ObservableRangeCollection<string> Peps { get; set; }
        public ObservableRangeCollection<string> Proveedores { get; set; }

        private ValidatableObject<double> _cantidad;
        public ValidatableObject<double> Cantidad
        {
            get { return _cantidad; }
            set
            {
                SetProperty(ref _cantidad, value);
            }
        }

        private ValidatableObject<ClaseDeValoracion> lote;
        public ValidatableObject<ClaseDeValoracion> Lote
        {
            get { return lote; }
            set
            {
                SetProperty(ref lote, value);
            }
        }

        private ValidatableObject<TipoStock> tipoStock;
        public ValidatableObject<TipoStock> TipoStock
        {
            get { return tipoStock; }
            set
            {
                SetProperty(ref tipoStock, value);
            }
        }

        private ValidatableObject<string> pep;
        public ValidatableObject<string> Pep
        {
            get { return pep; }
            set
            {
                SetProperty(ref pep, value);
            }
        }

        private ValidatableObject<string> proveedor;
        public ValidatableObject<string> Proveedor
        {
            get { return proveedor; }
            set
            {
                SetProperty(ref proveedor, value);
            }
        }

        private ValidatableObject<string> unidadAlmacen;
        public ValidatableObject<string> UnidadAlmacen
        {
            get { return unidadAlmacen; }
            set
            {
                SetProperty(ref unidadAlmacen, value);
            }
        }

        private string ubicacion;
        public string Ubicacion
        {
            get { return ubicacion; }
            set
            {
                SetProperty(ref ubicacion, value);
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

        private Material material;
        public Material Material
        {
            get { return material; }
            set
            {
                SetProperty(ref material, value);
            }
        }

        public bool IsPepEnabled { get { return inventario?.StockEspecial.Codigo.Trim() == "Q"; } }

        public bool IsProveedorEnabled { get { return inventario?.StockEspecial.Codigo.Trim() == "K" || inventario?.StockEspecial.Codigo.Trim() == "O"; } }

        private void FillUnidades()
        {
            Unidades.Clear();
            Unidades.Add(Material.UnidadDeMedidaBase);

            if (!string.IsNullOrEmpty(Material.UnidadDeMedidaAlternativa1))
            {
                Unidades.Add(Material.UnidadDeMedidaAlternativa1);
            }

            if (!string.IsNullOrEmpty(Material.UnidadDeMedidaAlternativa2))
            {
                Unidades.Add(Material.UnidadDeMedidaAlternativa2);
            }

            if (!string.IsNullOrEmpty(Material.UnidadDeMedidaAlternativa3))
            {
                Unidades.Add(Material.UnidadDeMedidaAlternativa3);
            }

            if (!string.IsNullOrEmpty(Material.UnidadDeMedidaAlternativa4))
            {
                Unidades.Add(Material.UnidadDeMedidaAlternativa4);
            }
        }

        private void FillTiposStock()
        {
            TiposStock.Clear();
            TiposStock.AddRange(tipoStockService.GetAll());
        }

        private void FillPeps()
        {
            Peps.Clear();
            foreach (var stock in StocksDisponibles)
            {
                if (!Peps.Contains(stock.DetalleStockEspecial.Detalle)) Peps.Add(stock.DetalleStockEspecial.Detalle);
            }
        }

        private void FillProveedores()
        {
            Proveedores.Clear();
            foreach (var stock in StocksDisponibles)
            {
                if (!Proveedores.Contains(stock.DetalleStockEspecial.Detalle)) Proveedores.Add(stock.DetalleStockEspecial.Detalle);
            }
        }

        private void FillEstadosInventario()
        {
            EstadosInventario.Clear();
            EstadosInventario.Add("Contado");
            EstadosInventario.Add("No contado");
        }

        private void InitForm()
        {
            Title = "Detalle del material";
            GoToListCommand = new Command(ConfirmarMaterial);
            DetalleInventarioModel = navigationService.GetNavigationParams<CrearDetalleInventarioView>() as DetalleInventarioModel;
            detalleInventario = DetalleInventarioModel.DetalleInventario;
            inventario = detalleInventario.Inventario;

            ListaClaseDeValoracion = new ObservableRangeCollection<ClaseDeValoracion>();
            Unidades = new ObservableRangeCollection<string>();
            TiposStock = new ObservableRangeCollection<TipoStock>();
            EstadosInventario = new ObservableRangeCollection<string>();
            Peps = new ObservableRangeCollection<string>();
            Proveedores = new ObservableRangeCollection<string>();

            Cantidad = new ValidatableObject<double>();
            Lote = new ValidatableObject<ClaseDeValoracion>();
            TipoStock = new ValidatableObject<TipoStock>();
            Pep = new ValidatableObject<string>();
            Proveedor = new ValidatableObject<string>();
            UnidadAlmacen = new ValidatableObject<string>();

            Material = detalleInventario.Stock != null ? detalleInventario.Stock.Material : detalleInventario.StocksDisponibles.FirstOrDefault().Material;

            var ret = InitAsync();
        }

        private async Task InitAsync()
        {
            await StartSpinner();

            await GetStocksdisponibles();
            await GetClasesDeValoracion();

            AddValidations();

            FillTiposStock();
            FillUnidades();
            FillEstadosInventario();

            Cantidad.Value = detalleInventario.Cantidad;
            Lote.Value = detalleInventario.Lote;
            TipoStock.Value = tipoStockService.GetByCodigo(detalleInventario.TipoStockId.ToString());

            var ubicacionOriginal = detalleInventario.Stock != null ? detalleInventario.Stock.Ubicacion : detalleInventario.StocksDisponibles.FirstOrDefault().Ubicacion;

            if (inventario.Id == 0) //creando inventario
            {
                //Es la primera vez
                if (string.IsNullOrEmpty(detalleInventario.Ubicacion) && !string.IsNullOrEmpty(ubicacionOriginal))
                    Ubicacion = ubicacionOriginal;

                //Se modificó la ubicación
                if (!string.IsNullOrEmpty(detalleInventario.Ubicacion))
                    Ubicacion = detalleInventario.Ubicacion;
            }
            else
                Ubicacion = detalleInventario.Ubicacion;

            UnidadAlmacen.Value = Material.UnidadDeMedidaBase;

            if (IsPepEnabled)
            {
                if (detalleInventario.DetalleStockEspecial != null) Pep.Value = detalleInventario.DetalleStockEspecial.Detalle;
                FillPeps();
            }

            if (IsProveedorEnabled)
            {
                if (detalleInventario.DetalleStockEspecial != null) Proveedor.Value = detalleInventario.DetalleStockEspecial.Detalle;
                FillProveedores();
            }
            await StopSpinner();
        }

        private async Task GetStocksdisponibles()
        {
            StocksDisponibles = await stockService.GetByCodigoMaterial(inventario.IdCentro, inventario.IdAlmacen, inventario.IdStockEspecial, Material.Codigo);
        }

        public CrearDetalleInventarioViewModel(INavigationService navigationService, IClaseDeValoracionService claseDeValoracionService,
            IInventarioService inventarioService, IDisplayAlertService displayAlertService, ITipoStockService tipoStockService, IDetalleInventarioService detalleInventarioService,
            IInventarioLocalService inventarioLocalService, IStockService stockService)
        {
            this.navigationService = navigationService;
            this.claseDeValoracionService = claseDeValoracionService;
            this.inventarioService = inventarioService;
            this.displayAlertService = displayAlertService;
            this.tipoStockService = tipoStockService;
            this.detalleInventarioService = detalleInventarioService;
            this.inventarioLocalService = inventarioLocalService;
            this.stockService = stockService;
            InitForm();
        }

        private async Task GetClasesDeValoracion()
        {
            await GetStocksdisponibles();
            ListaClaseDeValoracion.Clear();
            ListaClaseDeValoracion.AddRange(StocksDisponibles.Select(x => x.ClaseDeValoracion)
                .GroupBy(claseDeValoracion => claseDeValoracion.Id).Select(group => group.First()));
            Lote.Value = detalleInventario.Lote;
            LoteIndex = ListaClaseDeValoracion.IndexOf(Lote.Value);
        }

        private async void ConfirmarMaterial(object obj)
        {
            if (!Validate())
            {
                Toast.ShowMessage("El material ingresado contiene errores. Por favor, vuelva a ingresarlo");
                return;
            }
            var detalleOriginal = detalleInventario;
            var detalleInventarioAuxiliar = CompleteDetalleInventarioAuxiliar();

            if (inventarioService.IsDuplicatedDetalleInventario(inventario, detalleInventarioAuxiliar))
            {
                var answerDuplicated = await displayAlertService.Show("Material duplicado", "El material ya se encuentra ingresado en el inventario,¿Desea sumar las cantidades?", "Aceptar", "Cancelar");
                if (answerDuplicated)
                {
                    CompleteDetalleInventario();
                    if (inventario.Id == 0) //creando inventario
                    {
                        var inventarioLocal = await inventarioLocalService.GetInventarioById(inventario.inventarioLocalId);
                        var detalleInventarioActual = Helper.MapToDetalleInventarioLocal(detalleInventario);
                        var detalleLocal = inventarioLocalService.GetDetalleInventarioDuplicated(inventarioLocal, detalleInventarioActual);
                        detalleLocal.Cantidad += Cantidad.Value;
                        detalleLocal.CantidadContada += Cantidad.Value;
                        inventario = Helper.MapToInventario(inventarioLocal);
                        inventario.inventarioLocalId = inventarioLocal.Id;

                        await inventarioLocalService.DeleteDetalleLocal(detalleInventarioActual);
                        inventario.DetallesInventario.RemoveAll(x => x.Id == detalleInventario.Id);

                        detalleInventario = Helper.MapToDetalleInventario(detalleLocal);
                        detalleInventario.Inventario = inventario;
                        detalleInventario.InventarioId = inventario.Id;
                        detalleInventario.StocksDisponibles = StocksDisponibles.ToList();
                    }
                    else
                    {
                        await detalleInventarioService.Delete(detalleInventario);
                        inventario.DetallesInventario.RemoveAll(x => x.Id == detalleInventario.Id);
                        detalleInventario = inventarioService.GetDetalleInventarioDuplicated(inventario, detalleInventario);
                        detalleInventario.Cantidad += Cantidad.Value;
                        detalleInventario.CantidadContada += Cantidad.Value;
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                CompleteDetalleInventario();
                if (!inventario.DetallesInventario.Exists(x => x == detalleInventario))
                {
                    inventario.DetallesInventario.Add(detalleInventario);
                }
            }

            await SaveInventarioLocal();
            BackToList();
        }

        private async void BackToList()
        {
            var pageTypeList = new List<Type>() {
                typeof(ListaInventarioView),
                typeof(ListaDetalleInventarioView)
            };

            navigationService.PushFromAsync(typeof(HomeView), pageTypeList, inventario);
        }

        private async Task SaveInventarioLocal()
        {
            try
            {
                if (inventario.Id == 0) //creando inventario
                {
                    if (inventario.inventarioLocalId == 0)
                    {
                        //Parse to inventario
                        InventarioLocal inventarioLocal = Helper.MapToInventarioLocal(inventario);
                        var inventarioSaved = await inventarioLocalService.Save(inventarioLocal);
                        inventario = Helper.MapToInventario(inventarioSaved);
                    }
                    else
                    {
                        InventarioLocal inventarioLocal = Helper.MapToInventarioLocal(inventario);
                        inventarioLocal.Id = inventario.inventarioLocalId;
                        var inventarioSaved = await inventarioLocalService.Save(inventarioLocal);
                        inventario = Helper.MapToInventario(inventarioSaved);
                    }

                }
                else
                {
                    await inventarioService.Save(inventario);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void CompleteDetalleInventario()
        {
            detalleInventario.Cantidad = Cantidad.Value;
            detalleInventario.CantidadContada = Cantidad.Value;
            detalleInventario.ClaseDeValoracionId = Lote.Value.Id;
            detalleInventario.Lote = Lote.Value;
            detalleInventario.TipoStockId = TipoStock.Value.Id;
            detalleInventario.EsContado = true;
            detalleInventario.Ubicacion = Ubicacion;
            detalleInventario.UnidadAlmacen = UnidadAlmacen.Value;

            if (!IsPepEnabled && !IsProveedorEnabled)
            {
                detalleInventario.Stock = StocksDisponibles.Where(c => c.ClaseDeValoracion.Id == Lote.Value.Id).FirstOrDefault();
            }
            else if (IsPepEnabled)
            {
                detalleInventario.Stock = StocksDisponibles.Where(c => c.ClaseDeValoracion.Id == Lote.Value.Id && c.DetalleStockEspecial.Detalle == Pep.Value).FirstOrDefault();
            }
            else if (IsProveedorEnabled)
            {
                detalleInventario.Stock = StocksDisponibles.Where(c => c.ClaseDeValoracion.Id == Lote.Value.Id && c.DetalleStockEspecial.Detalle == Proveedor.Value).FirstOrDefault();
            }

            detalleInventario.StockId = detalleInventario.Stock.Id;
            detalleInventario.DetalleStockEspecial = detalleInventario.Stock.DetalleStockEspecial;
            detalleInventario.DetalleStockEspecialId = detalleInventario.Stock.DetalleStockEspecial.Id;
            detalleInventario.HayConteoErroneo = detalleInventario.Cantidad != GetCantidadStock(detalleInventario.Stock, detalleInventario.TipoStockId);
            detalleInventario.EstadoConteo = detalleInventario.HayConteoErroneo ? EstadoConteoEnum.Erroneo : EstadoConteoEnum.Completo;
        }


        private double GetCantidadStock(Stock stock, int tipoStockId)
        {
            return tipoStockId == 1 ? stock.CantidadAlmacen :
                       tipoStockId == 2 ? stock.CantidadBloqueado : stock.CantidadCalidad;
        }

        private DetalleInventario CompleteDetalleInventarioAuxiliar()
        {
            var detalleInventarioAuxiliar = new DetalleInventario();
            detalleInventarioAuxiliar.Id = detalleInventario.Id;
            detalleInventarioAuxiliar.Cantidad = Cantidad.Value;
            detalleInventarioAuxiliar.CantidadContada = Cantidad.Value;
            detalleInventarioAuxiliar.ClaseDeValoracionId = Lote.Value.Id;
            detalleInventarioAuxiliar.Lote = Lote.Value;
            detalleInventarioAuxiliar.TipoStockId = TipoStock.Value.Id;
            detalleInventarioAuxiliar.EsContado = true;
            detalleInventarioAuxiliar.Ubicacion = Ubicacion;
            detalleInventarioAuxiliar.UnidadAlmacen = UnidadAlmacen.Value;

            if (!IsPepEnabled && !IsProveedorEnabled)
            {
                detalleInventarioAuxiliar.Stock = StocksDisponibles.Where(c => c.ClaseDeValoracion.Id == Lote.Value.Id).FirstOrDefault();
            }
            else if (IsPepEnabled)
            {
                detalleInventarioAuxiliar.Stock = StocksDisponibles.Where(c => c.ClaseDeValoracion.Id == Lote.Value.Id && c.DetalleStockEspecial.Detalle == Pep.Value).FirstOrDefault();
            }
            else if (IsProveedorEnabled)
            {
                detalleInventarioAuxiliar.Stock = StocksDisponibles.Where(c => c.ClaseDeValoracion.Id == Lote.Value.Id && c.DetalleStockEspecial.Detalle == Proveedor.Value).FirstOrDefault();
            }

            detalleInventarioAuxiliar.StockId = detalleInventarioAuxiliar.Stock.Id;
            detalleInventarioAuxiliar.DetalleStockEspecial = detalleInventarioAuxiliar.Stock.DetalleStockEspecial;
            detalleInventarioAuxiliar.DetalleStockEspecialId = detalleInventarioAuxiliar.Stock.DetalleStockEspecial.Id;

            return detalleInventarioAuxiliar;
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

            Lote.Validations.Clear();
            Lote.Validations.Add(new IsNotNullOrEmptyRule<ClaseDeValoracion>
            {
                ValidationMessage = "El lote es obligatorio."
            });

            TipoStock.Validations.Clear();
            TipoStock.Validations.Add(new IsNotNullOrEmptyRule<TipoStock>
            {
                ValidationMessage = "El tipo de stock es obligatorio."
            });

            UnidadAlmacen.Validations.Clear();
            UnidadAlmacen.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "La unidad de medida es obligatoria."
            });

            if (IsPepEnabled)
            {
                Pep.Validations.Clear();
                Pep.Validations.Add(new IsNotNullOrEmptyRule<string>
                {
                    ValidationMessage = "El pep es obligatorio."
                });
            }

            if (IsProveedorEnabled)
            {
                Proveedor.Validations.Clear();
                Proveedor.Validations.Add(new IsNotNullOrEmptyRule<string>
                {
                    ValidationMessage = "El proveedor es obligatorio."
                });
            }
        }

        private bool Validate()
        {
            bool isValidCantidad = ValidateCantidad();
            bool isValidLote = ValidateLote();
            bool isValidTipoStock = ValidateTipoStock();
            bool isValidPep = ValidatePep();
            bool isValidProveedor = ValidateProveedor();
            bool isValidLoteDetalle = ValidateLoteDetalleStock();
            bool isValidUnidadAlmacen = ValidateUnidadAlmacen();
            return isValidCantidad
                && isValidLote
                && isValidTipoStock
                && isValidPep
                && isValidProveedor
                && isValidLoteDetalle
                && isValidUnidadAlmacen;
        }

        private bool ValidateLoteDetalleStock()
        {
            if (IsPepEnabled && Lote.IsValid)
            {
                var stockSeleccionado = StocksDisponibles.Where(c => c.ClaseDeValoracion.Id == Lote.Value.Id && c.DetalleStockEspecial.Detalle == Pep.Value).FirstOrDefault();
                if (stockSeleccionado == null)
                {
                    Toast.ShowMessage("Combinación de lote y PEP no válida.");
                    return false;
                }
            }
            else if (IsProveedorEnabled && Lote.IsValid)
            {
                var stockSeleccionado = StocksDisponibles.Where(c => c.ClaseDeValoracion.Id == Lote.Value.Id && c.DetalleStockEspecial.Detalle == Proveedor.Value).FirstOrDefault();
                if (stockSeleccionado == null)
                {
                    Toast.ShowMessage("Combinación de lote y Proveedor no válida.");
                    return false;
                }
            }

            return true;
        }

        public bool ValidateCantidad()
        {
            return Cantidad.Validate();
        }

        public bool ValidateLote()
        {
            return Lote.Validate();
        }

        public bool ValidateUnidadAlmacen()
        {
            return UnidadAlmacen.Validate();
        }

        public bool ValidatePep()
        {
            return Pep.Validate();
        }

        public bool ValidateProveedor()
        {
            return Proveedor.Validate();
        }

        public bool ValidateTipoStock()
        {
            return TipoStock.Validate();
        }
    }
}
