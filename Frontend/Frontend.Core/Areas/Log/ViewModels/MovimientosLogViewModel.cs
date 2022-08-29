using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Reservas;
using Frontend.Business.Movimientos.SalidasInternas;
using Frontend.Business.Movimientos.Traslados;
using Frontend.Core.Areas.Log.IViewModels;
using Frontend.Core.Areas.Views;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Log.ViewModels
{
    public class MovimientosLogViewModel : BaseViewModel, IMovimientosLogViewModel
    {
        private readonly IDisplayAlertService displayAlertService;
        private readonly INavigationService navigationService;
        private readonly IMovimientoLogService movimientoLogService;
        private readonly IReservaService reservaService;
        private readonly IPedidoService pedidoService;
        private readonly ISalidaInternaService salidaInternaService;
        private readonly ITrasladoService trasladoService;
        private readonly IInventarioService inventarioService;
        public ICommand GetMovmientosLogsCommand { get; set; }

        public ObservableRangeCollection<MovimientoLog> ListaMovimientoLog { get; set; }
        public Task Refresh { get; set; }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }

        private bool _hasItems = false;
        public bool HasItems
        {
            get { return _hasItems; }
            set
            {
                SetProperty(ref _hasItems, value);
            }
        }


        private MovimientoLog _movimientoLogSelected;
        public MovimientoLog movimientoLogSelected
        {
            get { return _movimientoLogSelected; }
            set
            {
                SetProperty(ref _movimientoLogSelected, value);
                GoToDetalle(_movimientoLogSelected);
            }
        }
        public MovimientosLogViewModel(IDisplayAlertService displayAlertService, INavigationService navigationService, IMovimientoLogService movimientoLogService,
            IReservaService reservaService, IPedidoService pedidoService, ISalidaInternaService salidaInternaService, ITrasladoService trasladoService)
        {
            this.displayAlertService = displayAlertService;
            this.navigationService = navigationService;
            this.movimientoLogService = movimientoLogService;
            this.reservaService = reservaService;
            this.pedidoService = pedidoService;
            this.salidaInternaService = salidaInternaService;
            this.trasladoService = trasladoService;
            Init();
        }

        private void Init()
        {
            Title = "Movimiento";
            GetMovmientosLogsCommand = new Command(async () => await RefreshLogsMovimientos());
            ListaMovimientoLog = new ObservableRangeCollection<MovimientoLog>();
            Refresh = GetAllLogs();
        }

        private async Task RefreshLogsMovimientos()
        {
            IsRefreshing = true;
            await GetAllLogs();
            IsRefreshing = false;
        }

        private async Task GetAllLogs()
        {
            ListaMovimientoLog.Clear();
            ListaMovimientoLog.AddRange(await movimientoLogService.GetAllMovimientoLogError());
            HasItems = ListaMovimientoLog.Count > 0;
        }

        private async void GoToDetalle(MovimientoLog movimientoLog)
        {
            if (movimientoLog == null)
            {
                return;
            }

            try
            {
                if (movimientoLog.TipoMovimiento == TipoMovimiento.Reserva)
                {
                    var reserva = await reservaService.GetWithChildren(movimientoLog.IdRemoto);
                    if (reserva.TipoReserva == TipoReserva.Devolucion)
                    {
                        navigationService.PushAsync<MovimientosLogView, CabeceraDevolucionView>(reserva);
                    }
                    else if (reserva.TipoReserva == TipoReserva.Salida)
                    {
                        navigationService.PushAsync<MovimientosLogView, CabeceraSalidaView>(reserva);
                    }
                }
                else if (movimientoLog.TipoMovimiento == TipoMovimiento.Pedido)
                {
                    var pedido = await pedidoService.GetWithChildren(movimientoLog.IdRemoto);
                    navigationService.PushAsync<MovimientosLogView, CabeceraDePedidoView>(pedido);
                }
                else if (movimientoLog.TipoMovimiento == TipoMovimiento.SalidaInterna)
                {
                    var salidaInterna = await salidaInternaService.GetWithChildren(movimientoLog.IdRemoto);
                    if (salidaInterna.ClaseDeMovimientoCodigo == ClaseDeMovimientoSalidaInterna.ClaseDeMovimiento[ClaseDeMovimientoSalidaInterna.CLASE_351])
                    {
                        navigationService.PushAsync<MovimientosLogView, CabeceraSalidaTraspasoView>(salidaInterna);
                    }
                    else if (salidaInterna.ClaseDeMovimientoCodigo == ClaseDeMovimientoSalidaInterna.ClaseDeMovimiento[ClaseDeMovimientoSalidaInterna.CLASE_643])
                    {
                        navigationService.PushAsync<MovimientosLogView, CabeceraDeSalidaInternaView>(salidaInterna);
                    }
                }
                else if (movimientoLog.TipoMovimiento == TipoMovimiento.Traslado)
                {
                    var claseDeMovimientoTrasladoPages = new Dictionary<string, Type>();
                    claseDeMovimientoTrasladoPages.Add(ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_309], typeof(Traslado309PorMaterialView));
                    claseDeMovimientoTrasladoPages.Add(ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_311], typeof(Traslado311PorMaterialView));
                    claseDeMovimientoTrasladoPages.Add(ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_541], typeof(Traslado541PorMaterialView));
                    claseDeMovimientoTrasladoPages.Add(ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_542], typeof(Traslado542PorMaterialView));
                    var traslado = await trasladoService.GetWithChildren(movimientoLog.IdRemoto);

                    Type type = claseDeMovimientoTrasladoPages[traslado.ClaseDeMovimientoCodigo];
                    navigationService.PushFromAsync(typeof(MovimientosLogView), type, traslado);
                }

            }
            catch (Exception e)
            {
                Toast.ShowMessage("El movimiento no se encuentra en el dispositivo.");
            }
        }
    }
}
