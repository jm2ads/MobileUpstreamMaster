using Frontend.Business.GruposDeArticulos;
using Frontend.Business.Materiales;
using Frontend.Business.Settings;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.Areas.Inventarios.Models;
using Frontend.Core.Commons.Navigation;
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
    public class ConsultaStockViewModel : BaseViewModel, IConsultaStockViewModel
    {
        private readonly INavigationService navigationService;
        private readonly ICentroService centroService;
        private readonly IAlmacenService almacenService;
        private readonly IClaseDeValoracionService claseDeValoracionService;
        private readonly IGrupoDeArticuloService grupoDeArticuloService;
        private readonly IMaterialService materialService;
        private readonly ISettingsService settingsService;
        private readonly IStockService stockService;
        private readonly ILecturaQRService lecturaQRService;

        public ConsultaStockModel consultaStockModel { get; set; }

        public ICommand ConsultarCommand { get; set; }
        public Setting setting;
        public ConsultaStockViewModel(INavigationService navigationService, ICentroService centroService, IAlmacenService almacenService, IClaseDeValoracionService claseDeValoracionService,
            IGrupoDeArticuloService grupoDeArticuloService, IMaterialService materialService, ISettingsService settingsService, IStockService stockService, ILecturaQRService lecturaQRService)
        {
            Title = "Consulta de stock";
            this.navigationService = navigationService;
            this.centroService = centroService;
            this.almacenService = almacenService;
            this.claseDeValoracionService = claseDeValoracionService;
            this.grupoDeArticuloService = grupoDeArticuloService;
            this.materialService = materialService;
            this.settingsService = settingsService;
            this.stockService = stockService;
            this.lecturaQRService = lecturaQRService;
            Init();
        }

        private async void Init()
        {
            ConsultarCommand = new Command(Consultar);

            consultaStockModel = new ConsultaStockModel();
            setting = await settingsService.GetWithChildren();
            await FillListaCentro();
            await FillListaAlmacen();
            await FillListaGrupoDeArticulo();
            await FillListaLote();
            await FillListaCodigo();
            await FillListaTextoCorto();
        }

        private void AddValidations()
        {
            throw new NotImplementedException();
        }

        private async void Consultar()
        {
            await StartSpinner();

            var centro = consultaStockModel.Centro;
            var almacen = consultaStockModel.Almacen;
            var lote = consultaStockModel.Lote;

            GrupoDeArticulo grupoDeArticulo = null;
            if (consultaStockModel.GrupoDeArticulo != string.Empty)
            {
                grupoDeArticulo = await grupoDeArticuloService.GetByCodigo(consultaStockModel.GrupoDeArticulo);
                if (grupoDeArticulo == null)
                {
                    Toast.ShowMessage("El grupo de artículo ingresado no corresponde a un material en stock.");
                    await StopSpinner();
                    return;
                }
            }

            Material material = null;
            if (consultaStockModel.Codigo != string.Empty)
            {
                var lecturaQR = await lecturaQRService.GetLecturaQR(consultaStockModel.Codigo);
                
                if (lecturaQR.MaterialId == null)
                {
                    Toast.ShowMessage("El código ingresado no corresponde a un material en stock.");
                    await StopSpinner();
                    return;
                }

                material = await materialService.GetByCodigo(lecturaQR.CodigoMaterial);
            }
            else if (consultaStockModel.TextoCorto != string.Empty)
            {
                material = await materialService.GetByDescripcion(consultaStockModel.TextoCorto);
                if (material == null)
                {
                    Toast.ShowMessage("El texto corto ingresado no corresponde a un material en stock.");
                    await StopSpinner();
                    return;
                }
            }

            var lista = await stockService.GetBy(centro.Id, almacen?.Id, lote?.Id, material?.Id, grupoDeArticulo?.Id);

            navigationService.PushAsync<ConsultaStockView, ResultadoConsultaStockView>(lista);
            await StopSpinner();
        }

        private async Task FillListaAlmacen()
        {
            var listaAlmacen = await almacenService.GetByIdCentro(setting.CentroActivoId);
            consultaStockModel.ListaAlmacen.AddRange(listaAlmacen);
        }

        private async Task FillListaCentro()
        {
            await StartSpinner();
            consultaStockModel.Centro = setting.CentroActivo;
            await StopSpinner();
        }

        private async Task FillListaGrupoDeArticulo()
        {
            var listaGrupoDeArticulo = await grupoDeArticuloService.GetAllCodigoAutocomplete();
            consultaStockModel.ListaGrupoDeArticulo = listaGrupoDeArticulo.ToList();
        }

        private async Task FillListaLote()
        {
            var lotes = await claseDeValoracionService.GetAll();
            consultaStockModel.ListaLote.AddRange(lotes);
        }

        private async Task FillListaCodigo()
        {
            var listCodigo = await materialService.GetAllCodigoMaterialAutocomplete();
            consultaStockModel.ListaCodigo = listCodigo.ToList();
        }

        private async Task FillListaTextoCorto()
        {
            var listaTextoCorto = await materialService.GetAllDescripcionMaterialAutocomplete();
            consultaStockModel.ListaTextoCorto = listaTextoCorto.ToList();
        }
    }
}
