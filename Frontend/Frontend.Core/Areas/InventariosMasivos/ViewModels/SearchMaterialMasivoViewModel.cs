using Frontend.Business.InventariosMasivos;
using Frontend.Business.Materiales;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.InventariosMasivos.IViewModels;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.InventariosMasivos.ViewModels
{
    public class SearchMaterialMasivoViewModel : BaseViewModel, ISearchMaterialMasivoViewModel
    {
        public ICommand GetAllMaterialCommand { get; set; }
        public ICommand GoToCrearDetalleInventarioMasivoCommand { get; set; }
        public ICommand CheckInventarioMasivoCommand { get; set; }
        public InventarioMasivo inventarioMasivo{ get; set; }
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
        private readonly IDetalleInventarioMasivoService detalleInventarioMasivoService;
        private readonly IStockService stockService;
        private readonly IMaterialService materialService;
        private readonly ILecturaQRService lecturaQRService;
        private readonly IDisplayAlertService displayAlertService;

        public SearchMaterialMasivoViewModel(INavigationService navigationService, 
            IDetalleInventarioMasivoService detalleInventarioMasivoService, 
            IStockService stockService, 
            IMaterialService materialService, 
            ILecturaQRService lecturaQRService,
            IDisplayAlertService displayAlertService)
        {
            this.navigationService = navigationService;
            this.detalleInventarioMasivoService = detalleInventarioMasivoService;
            this.stockService = stockService;
            this.materialService = materialService;
            this.lecturaQRService = lecturaQRService;
            this.displayAlertService = displayAlertService;
            Init();
        }

        private async void Init()
        {
            inventarioMasivo = navigationService.GetNavigationParams<SearchMaterialMasivoView>() as InventarioMasivo;

            Title = "Inventario masivo " + inventarioMasivo.NumeroProvisorio;

            GetAllMaterialCommand = new Command(async () => await GetAllMaterial());
            CheckInventarioMasivoCommand = new Command(CheckInventarioMasivo);
            GoToCrearDetalleInventarioMasivoCommand = new Command(async () => await GoToCrearDetalleInventarioMasivo());

            ListaFiltros = new List<string>();
            ListaFiltros.Add("Código");
            ListaFiltros.Add("Texto corto");

            Filtro = "Código";

            await GetAllMaterial();
        }

        private void CheckInventarioMasivo(object obj)
        {
            if (inventarioMasivo.DetallesInventarioMasivo.Count == 0)
            {
                navigationService.PushFromAsync<HomeView, SearchMaterialMasivoView>(inventarioMasivo);
            }
        }

        private async Task GetAllMaterial()
        {
            await StartSpinner();
            var listMaterial = await stockService.GetAllMaterialBy(inventarioMasivo.IdCentro, inventarioMasivo.Ubicacion);
            ListMaterialesFull = listMaterial.Select(x => x.Codigo.TrimStart('0')).ToList();
            ListDescripcionMateriales = listMaterial.Select(x => x.Descripcion).ToList();
            await StopSpinner();
        }

        private async Task GoToCrearDetalleInventarioMasivo()
        {
            try
            {
                var lecturaQr = await lecturaQRService.GetLecturaQR(SearchValue);
                var material = SearchByCodigo ?
                    await materialService.GetByCodigo(lecturaQr.CodigoMaterial) :
                    await materialService.GetByDescripcion(SearchValue);
                
                if (material == null || !await detalleInventarioMasivoService.ValidateMaterial(material))
                {
                    Toast.ShowMessage("Código o texto corto de material no válido. Por favor, vuelva a ingresarlo.");
                    return;
                }

                var detalleInventarioMasivo = await CompletarInventarioMasivo(material);

                if (detalleInventarioMasivo.TipoLote == TipoLote.Mixto)
                {
                    var respuesta = await displayAlertService.Show("Tipo de Lote", "Este material posee stock que pertenece a lotes nuevos y usados. Seleccione cual está contando.", "Nuevo", "Usado");
                    if (respuesta)
                    {
                        detalleInventarioMasivo.TipoLote = TipoLote.Nuevo;
                    }
                    else
                    {
                        detalleInventarioMasivo.TipoLote = TipoLote.Usado;
                    }
                }

                navigationService.PushFromAsync<SearchMaterialMasivoView, DetalleMaterialInventarioMasivoView>(detalleInventarioMasivo);
            }
            catch (System.Exception)
            {
                Toast.ShowMessage("Código o texto corto de material no válido. Por favor, vuelva a ingresarlo.");
            }
        }

        private async Task<DetalleInventarioMasivo> CompletarInventarioMasivo(Material material)
        {
            var detalleInventarioMasivo = await detalleInventarioMasivoService.Create(inventarioMasivo, material);
            //inventarioMasivo.DetallesInventarioMasivo.Add(detalleInventarioMasivo);
            return detalleInventarioMasivo;
        }
    }
}