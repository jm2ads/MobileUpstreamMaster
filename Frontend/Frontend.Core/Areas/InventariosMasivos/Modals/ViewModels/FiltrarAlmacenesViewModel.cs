using Frontend.Business.Almacenes;
using Frontend.Business.InventariosMasivos;
using Frontend.Core.Areas.InventariosMasivos.IViewModels;
using Frontend.Core.Areas.InventariosMasivos.Modals.Models;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
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

namespace Frontend.Core.Areas.InventariosMasivos.ViewModels
{
    public class FiltrarAlmacenesViewModel : BaseViewModel, IFiltrarAlmacenesViewModel
    {
        public ICommand GuardarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand FiltroAlmacenCommand { get; set; }

        private InventarioMasivo _inventarioMasivo;
        public InventarioMasivo inventarioMasivo
        {
            get { return _inventarioMasivo; }
            set
            {
                SetProperty(ref _inventarioMasivo, value);
            }
        }
        private ObservableRangeCollection<FiltrarAlmacenesModel> _listaAlmacenes;
        public ObservableRangeCollection<FiltrarAlmacenesModel> ListaAlmacenes
        {
            get { return _listaAlmacenes; }
            set
            {
                SetProperty(ref _listaAlmacenes, value);
            }
        }
        private List<Almacen> almacenesCopia;
        private List<FiltrarAlmacenesModel> almacenesList;

        private FiltrarAlmacenesModel _almacenSelected;
        public FiltrarAlmacenesModel AlmacenSelected
        {
            get { return _almacenSelected; }
            set
            {
                SetProperty(ref _almacenSelected, value);
                ExcluirAlmacen(_almacenSelected);
            }
        }
        private readonly INavigationService navigationService;
        private readonly IInventarioMasivoService inventarioMasivoService;
        private readonly IDisplayAlertService displayAlertService;


        public FiltrarAlmacenesViewModel(INavigationService navigationService, IInventarioMasivoService inventarioMasivoService, IDisplayAlertService displayAlertService)
        {
            this.navigationService = navigationService;
            this.inventarioMasivoService = inventarioMasivoService;
            this.displayAlertService = displayAlertService;
            Init();
            var init = GetAlmacenes();
        }

        private void Init()
        {
            Title = "Almacenes a distribuir";
            ListaAlmacenes = new ObservableRangeCollection<FiltrarAlmacenesModel>();
            GuardarCommand = new Command(async () => await Guardar());
            CancelarCommand = new Command(async () => await Cancelar());
            almacenesList = new List<FiltrarAlmacenesModel>();
            FiltroAlmacenCommand = new Command<string>(FiltroAlmacen);
        }

        private void FiltroAlmacen(string value)
        {
            ListaAlmacenes.ReplaceRange(almacenesList.Where(x => string.IsNullOrEmpty(value) || x.Almacen.DisplayDescription.Contains(value)));
        }

        private async Task Cancelar()
        {
            inventarioMasivo.AlmacenesExcluidos = almacenesCopia;
            await navigationService.PopModalAsync();
        }

        private async Task Guardar()
        {
            inventarioMasivo.AlmacenesExcluidos = ListaAlmacenes.Where(x => x.EsExcluido).Select(al => al.Almacen).ToList();
            await navigationService.PopModalAsync<InformacionInventarioMasivoView>(inventarioMasivo);
        }

        private async Task GetAlmacenes()
        {
            ListaAlmacenes.Clear();
            inventarioMasivo = navigationService.GetNavigationParams<FiltrarAlmacenesModalView>() as InventarioMasivo;
            almacenesCopia = inventarioMasivo.AlmacenesExcluidos;
            var almacenes = await inventarioMasivoService.GetAlmacenes();
            ListaAlmacenes.AddRange(almacenes.Select(alm => new FiltrarAlmacenesModel() { Almacen = alm , EsExcluido = almacenesCopia.Exists(x=>x.Id == alm.Id)}));
            almacenesList.AddRange(ListaAlmacenes);
        }
        private void ExcluirAlmacen(FiltrarAlmacenesModel almacen)
        {
            if (almacen != null)
            {
                almacen.EsExcluido = !almacen.EsExcluido;
            }
        }
    }
}
