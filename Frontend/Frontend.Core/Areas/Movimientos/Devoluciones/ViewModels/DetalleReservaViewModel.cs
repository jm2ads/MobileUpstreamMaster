using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.NotasDeReservas;
using Frontend.Commons.Commons.Errors;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Movimientos.Devoluciones.IViewModels;
using Frontend.Core.Areas.Movimientos.Devoluciones.Models;
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

namespace Frontend.Core.Areas.Movimientos.Devoluciones.ViewModels
{
    public class DetalleDevolucionViewModel : BaseViewModel, IDetalleDevolucionViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly IReservaService reservaService;
        private readonly INotaDeReservaService notaDeReservaService;
        private readonly ISettingsService settingsService;

        public ICommand FiltroPosicionCommand { get; set; }
        public ICommand FinalizarCommand { get; set; }
        public ICommand GoToCabeceraCommand { get; set; }

        private DetalleDevolucionModel detalleReservaModelSelected;
        public DetalleDevolucionModel DetalleReservaModelSelected
        {
            get { return detalleReservaModelSelected; }
            set
            {
                SetProperty(ref detalleReservaModelSelected, value);
                GoToDetalleDevolucion(detalleReservaModelSelected);
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

        public ObservableRangeCollection<DetalleDevolucionModel> ListaDetallesNotaDeReservas { get; set; }
        private IList<DetalleDevolucionModel> detalleReservaViewList;

        public DetalleDevolucionViewModel(INavigationService navigationService, IDisplayAlertService displayAlertService,
            IReservaService reservaService, INotaDeReservaService notaDeReservaService, ISettingsService settingsService)
        {
            this.navigationService = navigationService;
            this.displayAlertService = displayAlertService;
            this.reservaService = reservaService;
            this.notaDeReservaService = notaDeReservaService;
            this.settingsService = settingsService;
            Init();
        }

        private async void Init()
        {
            notaDeReserva = navigationService.GetNavigationParams<DetalleDevolucionView>() as NotaDeReserva;
            Title = "Devolución " + notaDeReserva.Reserva.Numero;
            ListaDetallesNotaDeReservas = new ObservableRangeCollection<DetalleDevolucionModel>();
            FiltroPosicionCommand = new Command<string>(FiltroPosicion);
            FinalizarCommand = new Command(Finalizar);
            GoToCabeceraCommand = new Command(GoToCabecera);
            detalleReservaViewList = new List<DetalleDevolucionModel>();

            await InitAsync();
        }

        private void GoToCabecera(object obj)
        {
            navigationService.PushAsync<DetalleDevolucionView, VisualizarCabeceraDevolucionView>(notaDeReserva);
        }

        private void FiltroPosicion(string value)
        {
            ListaDetallesNotaDeReservas.Clear();
            ListaDetallesNotaDeReservas.AddRange(detalleReservaViewList.Where(x => String.IsNullOrEmpty(value) || x.DetalleNotaDeReserva.DetalleReserva.Posicion.Contains(value)));
        }

        public async Task InitAsync()
        {
            ListaDetallesNotaDeReservas.Clear();
            foreach (var item in notaDeReserva.DetallesNotasDeReservas)
            {
                detalleReservaViewList.Add(new DetalleDevolucionModel()
                {
                    EsContadoAction = async () => await Update(item),
                    DetalleNotaDeReserva = item,
                    EsContado = item.EsContado
                });
            }
            ListaDetallesNotaDeReservas.AddRange(detalleReservaViewList);
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

            var answer = await displayAlertService.Show("Finalizar devolución", "¿Desea finalizar la devolución?", "Aceptar", "Cancelar");
            if (answer)
            {
                try
                {
                    notaDeReserva.Reserva.Estado = EstadoMovimiento.PendienteAprobacionSap;
                    await notaDeReservaService.DeleteDetalle(notaDeReserva.DetallesNotasDeReservas.Where(x => !x.EsContado).ToList());
                    notaDeReserva.DetallesNotasDeReservas.RemoveAll(x=>!x.EsContado);
                    await notaDeReservaService.Update(notaDeReserva, Business.Synchronizer.SyncState.PendingToSync);
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
            return !notaDeReserva.DetallesNotasDeReservas.Any(x => x.EsContado);
        }

        private void GoToDetalleDevolucion(DetalleDevolucionModel detalleReservaModel)
        {
            if (detalleReservaModel != null)
            {
                navigationService.PushAsync<DetalleDevolucionView, DetalleMaterialDevolucionView>(detalleReservaModel.DetalleNotaDeReserva);
            }
        }
    }
}
