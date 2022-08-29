using Frontend.Business.Almacenes;
using Frontend.Business.CambiosUbicacion;
using Frontend.Core.Areas.CambiosUbicacion.Modals.IViewModels;
using Frontend.Core.Areas.CambiosUbicacion.Modals.Models;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.CambiosUbicacion.Modals.ViewModels
{
    public class SeleccionarAlmacenesViewModel : BaseViewModel, ISeleccionarAlmacenesViewModel
    {
        public ICommand GuardarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand FiltroAlmacenCommand { get; set; }

        private CambioUbicacion _cambioUbicacion;
        public CambioUbicacion CambioUbicacion
        {
            get { return _cambioUbicacion; }
            set
            {
                SetProperty(ref _cambioUbicacion, value);
            }
        }
        private ObservableRangeCollection<SeleccionarAlmacenesModel> _listaAlmacenes;
        public ObservableRangeCollection<SeleccionarAlmacenesModel> ListaAlmacenes
        {
            get { return _listaAlmacenes; }
            set
            {
                SetProperty(ref _listaAlmacenes, value);
            }
        }
        private List<Almacen> almacenesCopia;
        private List<SeleccionarAlmacenesModel> almacenesList;

        private SeleccionarAlmacenesModel _almacenSelected;
        public SeleccionarAlmacenesModel AlmacenSelected
        {
            get { return _almacenSelected; }
            set
            {
                SetProperty(ref _almacenSelected, value);
                IncluirAlmacen(_almacenSelected);
            }
        }
        private readonly INavigationService navigationService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly IAlmacenService almacenService;
        private readonly ISettingsService settingsService;

        public SeleccionarAlmacenesViewModel(INavigationService navigationService, IDisplayAlertService displayAlertService, IAlmacenService almacenService,
            ISettingsService settingsService)
        {
            this.navigationService = navigationService;
            this.displayAlertService = displayAlertService;
            this.almacenService = almacenService;
            this.settingsService = settingsService;
            Init();
            var init = GetAlmacenes();
        }

        private void Init()
        {
            Title = "Seleccionar almacenes";
            ListaAlmacenes = new ObservableRangeCollection<SeleccionarAlmacenesModel>();
            GuardarCommand = new Command(async () => await Guardar());
            CancelarCommand = new Command(async () => await Cancelar());
            almacenesList = new List<SeleccionarAlmacenesModel>();
            FiltroAlmacenCommand = new Command<string>(FiltroAlmacen);
        }

        private void FiltroAlmacen(string value)
        {
            ListaAlmacenes.ReplaceRange(almacenesList.Where(x => string.IsNullOrEmpty(value) || x.Almacen.DisplayDescription.Contains(value)));
        }

        private async Task Cancelar()
        {
            CambioUbicacion.AlmacenesIncluidos = almacenesCopia;
            await navigationService.PopModalAsync();
        }

        private async Task Guardar()
        {
            CambioUbicacion.AlmacenesIncluidos = ListaAlmacenes.Where(x => x.EsIncluido).Select(al => al.Almacen).ToList();
            await navigationService.PopModalAsync();
        }

        private async Task GetAlmacenes()
        {
            ListaAlmacenes.Clear();
            var settings = await settingsService.GetWithChildren();
            CambioUbicacion = navigationService.GetNavigationParams<SeleccionarAlmacenesView>() as CambioUbicacion;
            almacenesCopia = CambioUbicacion.AlmacenesIncluidos;
            var almacenes = await almacenService.GetByIdCentro(settings.CentroActivoId);
            ListaAlmacenes.AddRange(almacenes.Select(alm => new SeleccionarAlmacenesModel() { Almacen = alm, EsIncluido = almacenesCopia.Exists(x => x.Id == alm.Id) }).OrderBy(x=>x.Almacen.Codigo));
            almacenesList.AddRange(ListaAlmacenes);
        }
        private void IncluirAlmacen(SeleccionarAlmacenesModel almacen)
        {
            if (almacen != null)
            {
                almacen.EsIncluido = !almacen.EsIncluido;
            }
        }
    }
}
