using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.NotasDeReservas;
using Frontend.Commons.Commons.Errors;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Movimientos.Salidas.IViewModels;
using Frontend.Core.Areas.Movimientos.Salidas.Models;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.Home.IViewModels;
namespace Frontend.Core.Areas.Movimientos.Salidas.ViewModels
{
    public class DetalleSalidaViewModel : BaseViewModel, IDetalleSalidaViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly IReservaService reservaService;
        private readonly INotaDeReservaService notaDeReservaService;
        private readonly ISettingsService settingsService;
        private readonly IStockEspecialService stockEspecialService;
        private readonly IStockService stockService;

        public ICommand FiltroPosicionCommand { get; set; }
        public ICommand FinalizarCommand { get; set; }
        public ICommand GoToCabeceraCommand { get; set; }

        private DetalleReservaModel detalleReservaModelSelected;
        public DetalleReservaModel DetalleReservaModelSelected
        {
            get { return detalleReservaModelSelected; }
            set
            {
                SetProperty(ref detalleReservaModelSelected, value);
                GoToDetalleSalida(detalleReservaModelSelected);
            }
        }

        private NotaDeReserva _notaDeReserva;
        public NotaDeReserva notaDeReserva
        {
            get { return _notaDeReserva; }
            set
            {
                SetProperty(ref _notaDeReserva, value);
            }
        }

        public ObservableRangeCollection<DetalleReservaModel> ListaDetallesNotaDeReservas { get; set; }
        private IList<DetalleReservaModel> detalleReservaViewList;

        public DetalleSalidaViewModel(INavigationService navigationService, IDisplayAlertService displayAlertService,
            IReservaService reservaService, INotaDeReservaService notaDeReservaService, ISettingsService settingsService,
            IStockEspecialService stockEspecialService, IStockService stockService)
        {
            this.navigationService = navigationService;
            this.displayAlertService = displayAlertService;
            this.reservaService = reservaService;
            this.notaDeReservaService = notaDeReservaService;
            this.settingsService = settingsService;
            this.stockEspecialService = stockEspecialService;
            this.stockService = stockService;
            Init();
        }

        private async void Init()
        {
            notaDeReserva = navigationService.GetNavigationParams<DetalleSalidaView>() as NotaDeReserva;
            Title = "Salida " + notaDeReserva.Reserva.Numero;
            ListaDetallesNotaDeReservas = new ObservableRangeCollection<DetalleReservaModel>();
            FiltroPosicionCommand = new Command<string>(FiltroPosicion);
            FinalizarCommand = new Command(Finalizar);
            GoToCabeceraCommand = new Command(GoToCabecera);
            detalleReservaViewList = new List<DetalleReservaModel>();

            await InitAsync();
        }

        private void GoToCabecera(object obj)
        {
            navigationService.PushAsync<DetalleDevolucionView, VisualizarCabeceraSalidaView>(notaDeReserva);
        }

        private void FiltroPosicion(string value)
        {
            ListaDetallesNotaDeReservas.ReplaceRange(detalleReservaViewList.Where(x => String.IsNullOrEmpty(value) || x.DetalleNotaDeReserva.DetalleReserva.Posicion.Contains(value)));
        }

        public async Task InitAsync()
        {
            ListaDetallesNotaDeReservas.Clear();
            foreach (var item in notaDeReserva.DetallesNotasDeReservas)
            {
                //detalleReservaViewList.Add(new DetalleReservaModel()
                //{
                //    EsContadoAction = async () => await Update(item),
                //    DetalleNotaDeReserva = item,
                //    EsContado = item.EsContado
                //});
                #region ASOSA UBICACION EN LA LISTA
                DetalleReservaModel detalleReservaModel = new DetalleReservaModel();
                detalleReservaModel.EsContadoAction = async () => await Update(item);
                detalleReservaModel.DetalleNotaDeReserva = item;
                detalleReservaModel.EsContado = item.EsContado;

                string UbicacionItem;
                try
                {
                    UbicacionItem = await stockService.GetUbicacionBy(item.DetalleReserva.CentroId, item.DetalleReserva.MaterialId, (int)item.DetalleReserva.AlmacenId, (int)item.DetalleReserva.ClaseDeValoracionId);
                    if (string.IsNullOrEmpty(UbicacionItem))
                    {
                        UbicacionItem = "-";
                    }
                }
                catch (Exception e)
                {
                    UbicacionItem = "-";
                }
                detalleReservaModel.Ubicacion = UbicacionItem;
                detalleReservaViewList.Add(detalleReservaModel);
                #endregion
            }
            ListaDetallesNotaDeReservas.AddRange(detalleReservaViewList.OrderBy(x => x.DetalleNotaDeReserva.DetalleReserva.Posicion));
        }

        public async Task Update(DetalleNotaDeReserva detalleNotaDeReserva)
        {
            if (!notaDeReservaService.Validate(detalleNotaDeReserva))
            {
                Toast.ShowMessage("Por favor, complete los campos obligatorios de los materiales seleccionados");
                return;
            }
            detalleNotaDeReserva.EsContado = !detalleNotaDeReserva.EsContado;
            await notaDeReservaService.Update(notaDeReserva);
        }

        private async void Finalizar()
        {
            if (Validate())
            {
                Toast.ShowMessage("Debe seleccionar al menos una posición");
                return;
            }

            var answer = await displayAlertService.Show("Finalizar salida", "¿Desea finalizar la salida?", "Aceptar", "Cancelar");
            if (answer)
            {
                try
                {
                    notaDeReserva.Reserva.Estado = EstadoMovimiento.PendienteAprobacionSap;
                    await notaDeReservaService.DeleteDetalle(notaDeReserva.DetallesNotasDeReservas.Where(x => !x.EsContado).ToList());
                    notaDeReserva.DetallesNotasDeReservas.RemoveAll(x => !x.EsContado);
                    await notaDeReservaService.Update(notaDeReserva, Business.Synchronizer.SyncState.PendingToSync);
                    await settingsService.SetPendingToSync(true);

                    #region ASOSA flagSync Devolucion/Salida de Material
                    Frontend.Core.Commons.Globals.flagSync = "DetalleSalidaViewModel";
                    #endregion

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
            return !notaDeReserva.DetallesNotasDeReservas.Any(x => x.EsContado);
        }

        private void GoToDetalleSalida(DetalleReservaModel detalleReservaModel)
        {
            if (detalleReservaModel != null)
            {
                navigationService.PushAsync<DetalleSalidaView, DetalleMaterialSalidaView>(detalleReservaModel.DetalleNotaDeReserva);
            }
        }
    }
}
