using Frontend.Business.Inventarios;
using Frontend.Business.Stocks;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Inventarios.Models;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.IViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.ViewModels
{
    public class SearchMaterialViewModel : BaseViewModel, ISearchMaterialViewModel
    {
        public ICommand GetAllMaterialCommand { get; set; }
        public ICommand GoToCrearDetalleInventarioCommand { get; set; }
        public ICommand CheckInventarioCommand { get; set; }
        public Inventario Inventario { get; set; }
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

        private readonly INavigationService navigationService;
        private readonly IDetalleInventarioService detalleInventarioService;
        private readonly IStockService stockService;
        private readonly ILecturaQRService lecturaQRService;

        public SearchMaterialViewModel(INavigationService navigationService, IDetalleInventarioService detalleInventarioService, IStockService stockService, ILecturaQRService lecturaQRService)
        {
            this.navigationService = navigationService;
            this.detalleInventarioService = detalleInventarioService;
            this.stockService = stockService;
            this.lecturaQRService = lecturaQRService;
            Init();
        }

        private async void Init()
        {
            Inventario = navigationService.GetNavigationParams<SearchMaterialView>() as Inventario;

            Title = "Inventario " + Inventario.Codigo;

            GetAllMaterialCommand = new Command(async () => await GetAllMaterial());
            CheckInventarioCommand = new Command(CheckInventario);
            GoToCrearDetalleInventarioCommand = new Command(async () => await GoToCrearDetalleInventario());

            ListaFiltros = new List<string>();
            ListaFiltros.Add("Código");
            ListaFiltros.Add("Texto corto");

            Filtro = "Código";

            await GetAllMaterial();
        }

        private void CheckInventario(object obj)
        {
            if (Inventario.DetallesInventario.Count == 0)
            {
                navigationService.PushFromAsync<HomeView, SearchMaterialView>(Inventario);
            }
        }

        private async Task GetAllMaterial()
        {
            await StartSpinner();
            var listMaterial = await stockService.GetAllMaterialBy(Inventario.IdCentro, Inventario.IdAlmacen, Inventario.IdStockEspecial);
            ListMaterialesFull = listMaterial.Select(x => x.Codigo.TrimStart('0')).ToList();
            ListDescripcionMateriales = listMaterial.Select(x => x.Descripcion).ToList();
            await StopSpinner();
        }

        private async Task GoToCrearDetalleInventario()
        {
            try
            {
                var lecturaQR = await lecturaQRService.GetLecturaQR(SearchValue);

                var stocksDisponibles = SearchByCodigo ?
                    await stockService.GetBy(Inventario.IdCentro, Inventario.IdAlmacen, Inventario.IdStockEspecial, lecturaQR.CodigoMaterial, lecturaQR.LoteId) :
                    await stockService.GetByDescripcionMaterial(Inventario.IdCentro, Inventario.IdAlmacen, Inventario.IdStockEspecial, SearchValue);

                if (stocksDisponibles.Count == 0)
                {
                    Toast.ShowMessage("Código o texto corto de material no válido. Por favor, vuelva a ingresarlo.");
                    return;
                }

                var detalle = new DetalleInventarioModel()
                {
                    DetalleInventario = await detalleInventarioService.Create(Inventario, stocksDisponibles.ToList(), lecturaQR),
                    ShowComentario = false,
                    ShowCantidad = true
                };
                navigationService.PushAsync<SearchMaterialView, CrearDetalleInventarioView>(detalle);
            }
            catch (System.Exception)
            {
                Toast.ShowMessage("Código o texto corto de material no válido. Por favor, vuelva a ingresarlo.");
            }
        }
    }
}
