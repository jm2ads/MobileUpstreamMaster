using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.SalidasInternas;
using Frontend.Core.Areas.Movimientos.SalidasInternas.IViewModels;
using Frontend.Core.Areas.Views;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.ViewModels;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Movimientos.SalidasInternas.ViewModels
{
    public class SalidaPorVentaInternaMaterialViewModel : BaseViewModel, ISalidaPorVentaInternaMaterialViewModel
    {

        public ICommand GetAllMaterialCommand { get; set; }
        public ICommand GoToIngresarPorMaterialCommand { get; set; }
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

        public IList<Tuple<string, int>> CodigoMaterialList { get; set; }
        public IList<Tuple<string, int>> DescripcionMaterialList { get; set; }
        
        private readonly INavigationService navigationService;
        private readonly IMaterialService materialService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly ISalidaInternaService salidaInternaService;
        private readonly ILecturaQRService lecturaQRService;


        public SalidaPorVentaInternaMaterialViewModel(INavigationService navigationService, IMaterialService materialService,
                IDisplayAlertService displayAlertService, ILecturaQRService lecturaQRService, ISalidaInternaService salidaInternaService)
        {
            Title = "Material";
            this.navigationService = navigationService;
            this.salidaInternaService = salidaInternaService;
            this.materialService = materialService;
            this.displayAlertService = displayAlertService;
            this.lecturaQRService = lecturaQRService;

            Init();
        }

        private async void Init()
        {
            Title = "Material";

            GetAllMaterialCommand = new Command(async () => await GetAllMaterial());
            GoToIngresarPorMaterialCommand = new Command(async () => await GoToIngresarPorMaterial());

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
            var listMateriales = await salidaInternaService.GetAllMaterialBy(EstadoMovimiento.Recibir, ClaseDeMovimientoSalidaInterna.ClaseDeMovimiento[ClaseDeMovimientoSalidaInterna.CLASE_643]);

            foreach (var item in listMateriales)
            {
                CodigoMaterialList.Add(new Tuple<string, int>(item.Codigo.TrimStart('0'), item.Id));
                DescripcionMaterialList.Add(new Tuple<string, int>(item.Descripcion, item.Id));
            }
            ListMaterialesFull = CodigoMaterialList.Select(x => x.Item1).ToList();
            ListDescripcionMateriales = DescripcionMaterialList.Select(x => x.Item1).ToList();

            await StopSpinner();
        }

        private async Task GoToIngresarPorMaterial()
        {
            try
            {
                await StartSpinner();
                var salidas = await salidaInternaService.GetAllBy(await lecturaQRService.GetLecturaQR(SearchValue), ClaseDeMovimientoSalidaInterna.ClaseDeMovimiento[ClaseDeMovimientoSalidaInterna.CLASE_643]);

                if (salidas.Count > 1)
                {
                    var answer = await displayAlertService.DisplayActionSheet("Seleccionar salida", "Cancelar", "", salidas.Select(x => x.NumeroPedido).ToArray());
                    if (!String.IsNullOrWhiteSpace(answer) && answer != "Cancelar")
                    {
                        GoToCabeceraDeSalida(await salidaInternaService.GetWithChildren(salidas.First(x => x.NumeroPedido == answer).Id));
                    }
                }
                else if (salidas.Count == 1)
                {
                    GoToCabeceraDeSalida(await salidaInternaService.GetWithChildren(salidas.First().Id));
                }
                else
                {
                    Toast.ShowMessage("Código o texto corto de material no válido. Por favor, vuelva a ingresarlo.");
                }
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

        private void GoToCabeceraDeSalida(SalidaInterna salida)
        {
            navigationService.PushAsync<SalidaPorVentaInternaMaterialView, CabeceraDeSalidaInternaView>(salida);
        }
    }
}
