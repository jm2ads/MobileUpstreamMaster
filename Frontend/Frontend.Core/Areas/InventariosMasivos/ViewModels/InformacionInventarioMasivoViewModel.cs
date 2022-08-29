using Frontend.Business.InventariosMasivos;
using Frontend.Business.Settings;
using Frontend.Core.Areas.InventariosMasivos.IViewModels;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.InventariosMasivos.ViewModels
{
    public class InformacionInventarioMasivoViewModel : BaseViewModel, IInformacionInventarioMasivoViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IInventarioMasivoService inventarioMasivoService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly ISettingsService settingsService;
        private readonly IStockService stockService;

        public ICommand CrearInventarioMasivoCommand { get; set; }
        public ICommand FiltrarAlmacenesCommand { get; set; }

        private InventarioMasivo _inventarioMasivo;
        public InventarioMasivo inventarioMasivo
        {
            get { return _inventarioMasivo; }
            set
            {
                SetProperty(ref _inventarioMasivo, value);
            }
        }


        public bool _isAlmacenEnabled;

        public bool IsAlmacenEnabled
        {
            get { return _isAlmacenEnabled; }
            set
            {
                SetProperty(ref _isAlmacenEnabled, value);
            }
        }

        private System.Collections.IList listaUbicaciones;
        public System.Collections.IList ListaUbicaciones
        {
            get { return listaUbicaciones; }
            set
            {
                SetProperty(ref listaUbicaciones, value);
            }
        }

        private string _fechaCreacion;
        public string FechaCreacion
        {
            get { return _fechaCreacion; }
            set
            {
                SetProperty(ref _fechaCreacion, value);
            }
        }

        private string _almacenesExcluidos;
        public string AlmacenesExcluidos
        {
            get { return _almacenesExcluidos; }
            set
            {
                SetProperty(ref _almacenesExcluidos, value);
            }
        }

        private string _fechaDocumento;
        public string FechaDocumento
        {
            get { return _fechaDocumento; }
            set
            {
                SetProperty(ref _fechaDocumento, value);
            }
        }

        private string _ubicacion;
        public string Ubicacion
        {
            get { return _ubicacion; }
            set
            {
                SetProperty(ref _ubicacion, value);
            }
        }

        private Setting setting;

        public InformacionInventarioMasivoViewModel(INavigationService navigationService, IInventarioMasivoService inventarioMasivoService,
            IDisplayAlertService displayAlertService, ISettingsService settingsService, IStockService stockService)
        {
            this.navigationService = navigationService;
            this.inventarioMasivoService = inventarioMasivoService;
            this.displayAlertService = displayAlertService;
            this.settingsService = settingsService;
            this.stockService = stockService;
            Init();
        }

        private async Task GetInventarioMasivo()
        {
            await StartSpinner();
            setting = await settingsService.GetWithChildren();
            try
            {
                var inventarioMasivoFromModal = navigationService.GetNavigationParams<InformacionInventarioMasivoView>() as InventarioMasivo;
                navigationService.DropNavigationParams<InformacionInventarioMasivoView>();
            }
            catch (Exception e)
            {
                inventarioMasivo = inventarioMasivoService.Create(setting.CentroActivo);
            }
            finally
            {
                Title = "Creación";
                FechaDocumento = inventarioMasivo.FechaDocumento.ToString("dd/MM/yyyy");
                FechaCreacion = inventarioMasivo.FechaCreacion.ToString("dd/MM/yyyy");
                await FillListaUbicaciones();
                await StopSpinner();
                ActualizarAlmacenesExcluidos();
            }

        }

        private void Init()
        {
            CrearInventarioMasivoCommand = new Command(async () => await CrearInventarioMasivo());
            FiltrarAlmacenesCommand = new Command(async () => await FiltrarAlmacenes());
        }
        public async void Refresh()
        {
            await GetInventarioMasivo();
        }

        private async Task FiltrarAlmacenes()
        {
            await navigationService.PushModalAsync<FiltrarAlmacenesModalView>(inventarioMasivo);
        }

        public void ActualizarAlmacenesExcluidos()
        {
            if (inventarioMasivo == null)
            {
                AlmacenesExcluidos = "No hay almacenes excluidos";
            }
            else
            {
                AlmacenesExcluidos = inventarioMasivo.AlmacenesExcluidos.Count() == 0 ? "No hay almacenes excluidos" : "Almacenes excluidos: " + String.Join(Environment.NewLine, inventarioMasivo.AlmacenesExcluidos.Select(x => x.DisplayDescription));
            }
        }

        private async Task CrearInventarioMasivo()
        {
            CompletarInventarioMasivo();
            Ubicacion = String.Empty;
            navigationService.PushAsync<InformacionInventarioMasivoView, SearchMaterialMasivoView>(inventarioMasivo);
        }

        private void CompletarInventarioMasivo()
        {
            inventarioMasivo.Ubicacion = Ubicacion;
            inventarioMasivo.UsuarioCreacion = setting.UsuarioActivo.IdRed;
        }

        private async Task FillListaUbicaciones()
        {
            var list = await stockService.GetAllUbicaciones(setting.CentroActivoId);
            ListaUbicaciones = list.OrderBy(x => x).ToList();
        }
    }
}
