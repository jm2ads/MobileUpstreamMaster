using Frontend.Business.DetallesInventario;
using Frontend.Business.Inventarios;
using Frontend.Business.Materiales;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Inventarios.ViewModels
{
    public class RecuentoMaterialViewModel : BaseViewModel, IRecuentoMaterialViewModel
    {
        public ICommand GetAllMaterialCommand { get; set; }
        public ICommand GoToCrearDetalleInventarioCommand { get; set; }
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
        private readonly IMaterialService materialService;
        private readonly IDetalleInventarioService detalleInventarioService;
        private readonly IInventarioService inventarioService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly ILecturaQRService lecturaQRService;

        public IList<Tuple<string, int>> CodigoMaterialList { get; set; }
        public IList<Tuple<string, int>> DescripcionMaterialList { get; set; }

        public RecuentoMaterialViewModel(INavigationService navigationService, IMaterialService materialService,
            IDetalleInventarioService detalleInventarioService, IInventarioService inventarioService, IDisplayAlertService displayAlertService,
            ILecturaQRService lecturaQRService)
        {
            this.navigationService = navigationService;
            this.materialService = materialService;
            this.detalleInventarioService = detalleInventarioService;
            this.inventarioService = inventarioService;
            this.displayAlertService = displayAlertService;
            this.lecturaQRService = lecturaQRService;
            Init();
        }

        private async void Init()
        {
            Title = "Material";

            GetAllMaterialCommand = new Command(async () => await GetAllMaterial());
            GoToCrearDetalleInventarioCommand = new Command(async () => await GoToCrearDetalleInventario());
            CodigoMaterialList = new List<Tuple<string, int>>();
            DescripcionMaterialList = new List<Tuple<string, int>>();

            ListaFiltros = new List<string>();
            ListaFiltros.Add("Código");
            ListaFiltros.Add("Texto corto");

            Filtro = "Código";

            await GetAllMaterial();
        }

        private async Task GetAllMaterial()
        {
            await StartSpinner();
            var listMateriales = await inventarioService.GetAllMaterialAutocompleteRecuento();

            foreach (var item in listMateriales)
            {
                CodigoMaterialList.Add(new Tuple<string, int>(item.Codigo.TrimStart('0'), item.Id));
                DescripcionMaterialList.Add(new Tuple<string, int>(item.Descripcion, item.Id));
            }
            ListMaterialesFull = CodigoMaterialList.Select(x => x.Item1).ToList();
            ListDescripcionMateriales = DescripcionMaterialList.Select(x => x.Item1).ToList();

            await StopSpinner();
        }

        private async Task GoToCrearDetalleInventario()
        {
            try
            {
                await StartSpinner();
                var inventarios = await detalleInventarioService.GetInventariosByIdMaterial(await lecturaQRService.GetLecturaQR(SearchValue));
                GoToRecuentoDetalleMaterial(inventarios);
            }
            catch (Exception e)
            {
                Toast.ShowMessage("Código o texto corto de material no válido. Por favor, vuelva a ingresarlo.");
            }
            finally
            {
                await StopSpinner();
            }
        }

        private async void GoToRecuentoDetalleMaterial(IList<Inventario> inventarios)
        {
            if (inventarios.Count == 1)
            {
                var inventario = await inventarioService.GetInventarioById(inventarios.First().Id);
                navigationService.PushAsync<RecuentoMaterialView, RecuentoDetalleInventarioView>(inventario);
            }
            else if(inventarios.Count > 1)
            {

                var inventariosDictionary = inventarios.ToDictionary(x => x.Codigo, x => x);
                var resultado = await this.displayAlertService.DisplayActionSheet("Inventarios", "Cerrar", "", inventariosDictionary.Keys.ToArray());
                if (!string.IsNullOrEmpty(resultado) && resultado != "Cerrar")
                {
                    var selected = inventariosDictionary[resultado];
                    var inventario = await inventarioService.GetInventarioById(selected.Id);
                    navigationService.PushAsync<RecuentoMaterialView, RecuentoDetalleInventarioView>(inventario);
                }
            }
            else
            {
                Toast.ShowMessage("Código o texto corto de material no válido. Por favor, vuelva a ingresarlo.");
            }
        }
    }
}
