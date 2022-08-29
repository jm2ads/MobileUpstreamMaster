using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.SalidasInternas;
using Frontend.Commons.Commons.Errors;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Movimientos.SalidaTraspaso.IViewModels;
using Frontend.Core.Areas.Movimientos.SalidaTraspaso.Models;
using Frontend.Core.Areas.Views;
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

namespace Frontend.Core.Areas.Movimientos.SalidaTraspaso.ViewModels
{
    public class ListadoPosicionesSalidaTraspasoViewModel : BaseViewModel, IListadoPosicionesSalidaTraspasoViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly ISalidaInternaService salidaInternaService;
        private readonly ISettingsService settingsService;

        public ICommand FiltroPosicionCommand { get; set; }
        public ICommand FinalizarCommand { get; set; }
        public ICommand GoToCabeceraCommand { get; set; }
        public ICommand OnBackButtonPressedCommnad { get; set; }


        private ListadoPosicionesSalidaInternaModel detalleSalidaInternaSelected;
        public ListadoPosicionesSalidaInternaModel DetalleSalidaInternaSelected
        {
            get { return detalleSalidaInternaSelected; }
            set
            {
                SetProperty(ref detalleSalidaInternaSelected, value);
                GoToDetalle(detalleSalidaInternaSelected);
            }
        }

        private SalidaInterna _salidaInterna;
        public SalidaInterna salidaInterna
        {
            get { return _salidaInterna; }
            set
            {
                SetProperty(ref _salidaInterna, value);
            }
        }

        public ObservableRangeCollection<ListadoPosicionesSalidaInternaModel> ListaDetallesSalidaInterna { get; set; }
        private List<ListadoPosicionesSalidaInternaModel> detalleSalidaInternaList;

        public ListadoPosicionesSalidaTraspasoViewModel(INavigationService navigationService, IDisplayAlertService displayAlertService,
            ISalidaInternaService salidaInternaService, ISettingsService settingsService)
        {
            this.navigationService = navigationService;
            this.displayAlertService = displayAlertService;
            this.salidaInternaService = salidaInternaService;
            this.settingsService = settingsService;
            Init();
        }

        private async void Init()
        {
            salidaInterna = navigationService.GetNavigationParams<ListadoPosicionesSalidaTraspasoView>() as SalidaInterna;
            Title = "Pedido " + salidaInterna.NumeroPedido;
            ListaDetallesSalidaInterna = new ObservableRangeCollection<ListadoPosicionesSalidaInternaModel>();
            FiltroPosicionCommand = new Command<string>(FiltroPosicion);
            FinalizarCommand = new Command(Finalizar);
            GoToCabeceraCommand = new Command(GoToCabecera);
            OnBackButtonPressedCommnad = new Command(OnBackButtonPressed);
            detalleSalidaInternaList = new List<ListadoPosicionesSalidaInternaModel>();

            FillListaDetallesSalidaInterna();
        }

        private async void OnBackButtonPressed()
        {
            var answer = await displayAlertService.Show("Aviso", "Si continua perderá el progreso ¿Desea continuar?", "Aceptar", "Cancelar");
            if (answer)
            {
                navigationService.PopAsync<ListadoPosicionesSalidaTraspasoView>();
            }
        }

        private void GoToCabecera()
        {
            navigationService.PushAsync<ListadoPosicionesSalidaTraspasoView, VisualizarInformacionTraspasoView>(salidaInterna);
        }

        private void FiltroPosicion(string value)
        {
            ListaDetallesSalidaInterna.ReplaceRange(detalleSalidaInternaList.Where(x => string.IsNullOrEmpty(value) || x.detalleSalidaInterna.Posicion.Contains(value)));
        }

        public void FillListaDetallesSalidaInterna()
        {
            ListaDetallesSalidaInterna.Clear();
            detalleSalidaInternaList.AddRange(salidaInterna.DetallesSalidaInterna.OrderBy(x => x.Posicion).Select(x => new ListadoPosicionesSalidaInternaModel()
            {
                EsContadoAction = async () => await Update(x),
                detalleSalidaInterna = x,
                EsContado = x.EsContado
            }));
            ListaDetallesSalidaInterna.AddRange(detalleSalidaInternaList);

        }

        public async Task Update(DetalleSalidaInterna detalleSalidaInterna)
        {
            detalleSalidaInterna.EsContado = !detalleSalidaInterna.EsContado;
            if (!salidaInternaService.Validate(detalleSalidaInterna))
            {
                Toast.ShowMessage("Por favor, complete los campos obligatorios de los materiales seleccionados");
                detalleSalidaInterna.EsContado = !detalleSalidaInterna.EsContado;
                return;
            }
            await salidaInternaService.Update(salidaInterna);
        }

        private async void Finalizar()
        {
            if (!Validate())
            {
                Toast.ShowMessage("El pedido contiene errores.");
                return;
            }

            var answer = await displayAlertService.Show("Finalizar pedido", "¿Desea finalizar el pedido?", "Aceptar", "Cancelar");
            if (answer)
            {
                try
                {
                    salidaInterna.Estado = EstadoMovimiento.PendienteAprobacionSap;
                    await salidaInternaService.Update(salidaInterna, Business.Synchronizer.SyncState.PendingToSync);
                    await settingsService.SetPendingToSync(true);
                    Toast.ShowMessage("El/los material/es ha/n sido ingresado/s con éxito");
                    navigationService.PushFromRootAsync<HomeView>();
                }
                catch (BusinessException be)
                {
                    Toast.ShowMessage(be.Mensaje);
                }
            }
        }

        private bool Validate()
        {
            return salidaInternaService.Validate(salidaInterna);
        }

        private void GoToDetalle(ListadoPosicionesSalidaInternaModel listadoPosicionesSalidaInternaModel)
        {
            if (listadoPosicionesSalidaInternaModel != null)
            {
                navigationService.PushAsync<ListadoPosicionesSalidaTraspasoView, DetalleMaterialSalidaTraspasoView>(listadoPosicionesSalidaInternaModel.detalleSalidaInterna);
            }
        }
    }
}
