using Frontend.Business.Movimientos.Traslados;
using Frontend.Business.Movimientos.Traslados.Core;
using Frontend.Business.Stocks;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
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
    public class Traslado321PorMaterialViewModel : BaseViewModel, ITraslado321PorMaterialViewModel
    {
        public ICommand GoToListadoTrasladoCommand { get; set; }

        private System.Collections.IList listMaterialesFull;
        public System.Collections.IList ListMaterialesFull
        {
            get { return listMaterialesFull; }
            set
            {
                SetProperty(ref listMaterialesFull, value);
            }
        }

        private System.Collections.IList listDescripcionMateriales;
        public System.Collections.IList ListDescripcionMateriales
        {
            get { return listDescripcionMateriales; }
            set
            {
                SetProperty(ref listDescripcionMateriales, value);
            }
        }

        private System.Collections.IList listaFiltros;
        public System.Collections.IList ListaFiltros
        {
            get { return listaFiltros; }
            set
            {
                SetProperty(ref listaFiltros, value);
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

        private Traslado traslado;
        public Traslado Traslado
        {
            get { return traslado; }
            set
            {
                SetProperty(ref traslado, value);
            }
        }

        private readonly INavigationService navigationService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly IStockService stockService;
        private readonly DetalleTrasladoGenerator detalleTrasladoGenerator;
        private readonly ILecturaQRService lecturaQRService;
        private readonly IStockEspecialService stockEspecialService;

        public IList<Tuple<string, int>> CodigoMaterialList { get; set; }
        public IList<Tuple<string, int>> DescripcionMaterialList { get; set; }

        public Traslado321PorMaterialViewModel(INavigationService navigationService, IDisplayAlertService displayAlertService, IStockService stockService,
            DetalleTrasladoGenerator detalleTrasladoGenerator, ILecturaQRService lecturaQRService, IStockEspecialService stockEspecialService)
        {
            this.navigationService = navigationService;
            this.detalleTrasladoGenerator = detalleTrasladoGenerator;
            this.lecturaQRService = lecturaQRService;
            this.displayAlertService = displayAlertService;
            this.stockService = stockService;
            this.stockEspecialService = stockEspecialService;
            Init();
        }
        private async void Init()
        {
            GoToListadoTrasladoCommand = new Command(async () => await SearchMaterial());
            CodigoMaterialList = new List<Tuple<string, int>>();
            DescripcionMaterialList = new List<Tuple<string, int>>();
            traslado = navigationService.GetNavigationParams<Traslado321PorMaterialView>() as Traslado;
            Title = "Traslado " + traslado.NumeroProvisorio;
            InitFiltro();
            await GetAllStocks();
        }

        private async Task GetAllStocks()
        {
            await StartSpinner();
            var stockEspecial = await stockEspecialService.GetByCodigo("O");
            var listMateriales = await stockService.GetAllMaterialExceptWithStockCalidad(stockEspecial.Id);

            foreach (var item in listMateriales)
            {
                CodigoMaterialList.Add(new Tuple<string, int>(item.Codigo.TrimStart('0'), item.Id));
                DescripcionMaterialList.Add(new Tuple<string, int>(item.Descripcion, item.Id));
            }
            ListMaterialesFull = CodigoMaterialList.Select(x => x.Item1).ToList();
            ListDescripcionMateriales = DescripcionMaterialList.Select(x => x.Item1).ToList();
            await StopSpinner();
        }

        private void InitFiltro()
        {
            ListaFiltros = new List<string>();
            ListaFiltros.Add("Código");
            ListaFiltros.Add("Texto corto");
            Filtro = "Código";
        }

        private async Task SearchMaterial()
        {
            try
            {
                await StartSpinner();
                var stockEspecial = await stockEspecialService.GetByCodigo("O");
                var lecturaQR = await lecturaQRService.GetLecturaQR(SearchValue);
                int idMaterial = SearchByCodigo ? CodigoMaterialList.FirstOrDefault(x => x.Item1 == lecturaQR.CodigoMaterial).Item2 : DescripcionMaterialList.FirstOrDefault(x => x.Item1 == SearchValue).Item2;
                var stocks = (await stockService.GetByAndExceptStockEspecial(idMaterial, lecturaQR.LoteId, stockEspecial.Id)).Where(x => x.CantidadCalidad > 0).ToList();
                if (stocks.Count != 1)
                {
                    var stockDictionary = await CreateStockDictionary(stocks);
                    var answer = await displayAlertService.DisplayActionSheet("Opciones disponibles", "Cancelar", "", stockDictionary.Select(x => x.Key).ToArray());
                    if (!String.IsNullOrWhiteSpace(answer) && answer != "Cancelar")
                    {
                        await CrearDetalleTraslado(stockDictionary[answer]);
                    }
                }
                else
                {
                    await CrearDetalleTraslado(stocks.First());
                }
            }
            catch (Exception e)
            {
                Toast.ShowMessage("Material no válido. Por favor, vuelva a ingresarlo.");
            }
            finally
            {
                await StopSpinner();
            }
        }

        private async Task CrearDetalleTraslado(Stock stock)
        {
            var detalleTraslado = await detalleTrasladoGenerator.Generate(traslado, stock);
            traslado.DetallesTraslado.Add(detalleTraslado);
            GoToListado();
        }

        private async Task<Dictionary<string, Stock>> CreateStockDictionary(IList<Stock> stocks)
        {
            Dictionary<string, Stock> stockDictionary = new Dictionary<string, Stock>();
            foreach (var stock in stocks)
            {
                // Se asigna el string con formato "   -   " para que respete el formato del modal.
                var almacen = stock.Almacen == null ? "   -   " : stock.Almacen.Codigo;
                string key;
                var stockEspecial = await stockEspecialService.GetByCodigo("S");
                if (stock.DetalleStockEspecial.IdStockEspecial == stockEspecial.Id)
                {
                    key = String.Format("Almacén:\t{0}\t|\tLote:\t{1}\t|\tStock Especial:\t{2}\t|\tPEP:\t{3}", almacen, stock.ClaseDeValoracion.Codigo, stockEspecial.Codigo, stock.DetalleStockEspecial.Detalle);
                }
                else
                {
                    key = String.Format("Almacén:\t{0}\t|\tLote:\t{1}", almacen, stock.ClaseDeValoracion.Codigo);
                }
                stockDictionary.Add(key, stock);
            }
            return stockDictionary;
        }

        private void GoToListado()
        {
            navigationService.PushFromAsync<HomeView, ListaPosicionesTraslado321View>(traslado);
        }
    }
}
