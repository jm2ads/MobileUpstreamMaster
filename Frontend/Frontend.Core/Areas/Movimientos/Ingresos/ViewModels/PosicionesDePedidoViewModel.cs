using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Ingresos;
using Frontend.Commons.Commons.Errors;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Movimientos.Ingresos.IViewModels;
using Frontend.Core.Areas.Movimientos.Ingresos.Models;
using Frontend.Core.Areas.Views;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.ViewModels;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Movimientos.Ingresos.ViewModels
{
    public class PosicionesDePedidoViewModel : BaseViewModel, IPosicionesDePedidoViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly INotaDeEntregaService notaDeEntregaService;
        private readonly ISettingsService settingsService;

        public ICommand FinalizarCommand { get; set; }
        public ICommand FiltroPosicionCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand GoToInformacionPedidoCommand { get; set; }

        private IList<PosicionesDePedidoModel> posicionesDePedidoViewList;
        public ObservableRangeCollection<PosicionesDePedidoModel> ListaDetallesDePedido { get; set; }

        private CabeceraDePedidoModel _cabeceraDePedidoModel;
        public CabeceraDePedidoModel cabeceraDePedidoModel
        {
            get { return _cabeceraDePedidoModel; }
            set
            {
                SetProperty(ref _cabeceraDePedidoModel, value);
            }
        }

        private PosicionesDePedidoModel detallePedidoSelected;
        public PosicionesDePedidoModel DetallePedidoSelected
        {
            get { return detallePedidoSelected; }
            set
            {
                SetProperty(ref detallePedidoSelected, value);
                GoToDetallePedido(detallePedidoSelected);
            }
        }
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }
        
        private bool _hasDetalles;
        public bool HasDetalles
        {
            get { return _hasDetalles; }
            set
            {
                SetProperty(ref _hasDetalles, value);
            }
        }
        public NotaDeEntrega notaDeEntrega{ get; set; }

        public PosicionesDePedidoViewModel(INavigationService navigationService, IDisplayAlertService displayAlertService, INotaDeEntregaService notaDeEntregaService,
            ISettingsService settingsService)
        {
            this.navigationService = navigationService;
            this.displayAlertService = displayAlertService;
            this.notaDeEntregaService = notaDeEntregaService;
            this.settingsService = settingsService;
            Init();
        }

        private void Init()
        {
            FinalizarCommand = new Command(async () => await Finalizar());
            RefreshCommand = new Command(async () => await Refresh());
            FiltroPosicionCommand = new Command<string>(FiltroPosicion);
            GoToInformacionPedidoCommand = new Command(async () => await GoToInformacionPedido());
            posicionesDePedidoViewList = new List<PosicionesDePedidoModel>();
            cabeceraDePedidoModel = navigationService.GetNavigationParams<PosicionesDePedidoView>() as CabeceraDePedidoModel;
            notaDeEntrega = cabeceraDePedidoModel.notaDeEntrega;
            Title = "Pedido " + notaDeEntrega.Pedido.NumeroPedido;
            ListaDetallesDePedido = new ObservableRangeCollection<PosicionesDePedidoModel>();
            InitListaDetallesDePedido();
        }

        private async Task Refresh()
        {
            if (IsRefreshing)
            {
                return;
            }
            IsRefreshing = true;
            var delay = Task.Delay(2000);
            ListaDetallesDePedido.Clear();
            ListaDetallesDePedido.AddRange(posicionesDePedidoViewList
                .Select(x => new PosicionesDePedidoModel()
                {
                    EsContadoAction = async () => await Update(x.detalleNotaDeEntregaPosicion),
                    detalleNotaDeEntregaPosicion = x.detalleNotaDeEntregaPosicion,
                    EsContado = x.detalleNotaDeEntregaPosicion.EsContado
                }));
            HasDetalles = ListaDetallesDePedido.Count > 0;
            await delay;
            IsRefreshing = false;
        }

        private void InitListaDetallesDePedido()
        {
            ListaDetallesDePedido.Clear();
            posicionesDePedidoViewList.Clear();
            foreach (var detalleNotaDeEntrega in notaDeEntrega.DetalleNotaDeEntrega.OrderBy(x=>x.DetallePedido.Posicion))
            {
                var detallesNotaEntregaPosicion = detalleNotaDeEntrega.DetalleNotaDeEntregaPosicion;
                if (cabeceraDePedidoModel.claseMovimiento == ClaseDeMovimientoPedido.ClaseDeMovimiento[ClaseDeMovimientoPedido.CLASE_103])
                {
                    detallesNotaEntregaPosicion = detallesNotaEntregaPosicion.Where(x => x.DetallePedidoPosicion.ClaseMovimientoCodigo == ClaseDeMovimientoPedido.ClaseDeMovimiento[ClaseDeMovimientoPedido.CLASE_103]).ToList();
                }
                else
                {
                    detallesNotaEntregaPosicion = detallesNotaEntregaPosicion.Where(x => string.IsNullOrWhiteSpace(x.DetallePedidoPosicion.ClaseMovimientoCodigo)).ToList();
                }
                foreach (var detalleNotaDeEntregaPosicion in detallesNotaEntregaPosicion)
                {
                    posicionesDePedidoViewList.Add(new PosicionesDePedidoModel()
                    {
                        EsContadoAction = async () => await Update(detalleNotaDeEntregaPosicion),
                        detalleNotaDeEntregaPosicion = detalleNotaDeEntregaPosicion,
                        EsContado = notaDeEntregaService.ValidatePosicion(detalleNotaDeEntrega)
                    });
                }
            }
            ListaDetallesDePedido.AddRange(posicionesDePedidoViewList);
        }

        private void FiltroPosicion(string filtro)
        {
            ListaDetallesDePedido.Clear();
            ListaDetallesDePedido.AddRange(posicionesDePedidoViewList.Where(x => String.IsNullOrEmpty(filtro) || x.detalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.DetallePedido.Posicion.Contains(filtro)));
        }

        private async Task Finalizar()
        {
            if (Validate())
            {
                Toast.ShowMessage("Debe seleccionar al menos una posición");
                return;
            }

            var answer = await displayAlertService.Show("Finalizar pedido", "¿Desea finalizar el pedido?", "Aceptar", "Cancelar");
            if (answer)
            {
                try
                {
                    notaDeEntrega.Pedido.Estado = EstadoMovimiento.PendienteAprobacionSap;
                    var detalles = notaDeEntrega.DetalleNotaDeEntrega.SelectMany(x => x.DetalleNotaDeEntregaPosicion);
                    var detallesNoContados = detalles.Where(x => !x.EsContado).ToList();
                    await notaDeEntregaService.DeleteDetalle(detallesNoContados);
                    notaDeEntrega.DetalleNotaDeEntrega.ForEach(x => x.DetalleNotaDeEntregaPosicion.RemoveAll(dnep => !dnep.EsContado));
                    notaDeEntrega.DetalleNotaDeEntrega.ForEach(x => x.DetalleNotaDeEntregaPosicion.ForEach(dnep => dnep.ClaseDeMovimientoCodigo = cabeceraDePedidoModel.claseMovimiento));
                    notaDeEntrega.DetalleNotaDeEntrega.RemoveAll(x => x.DetalleNotaDeEntregaPosicion.Count() == 0);
                    await notaDeEntregaService.Update(notaDeEntrega, syncState: Business.Synchronizer.SyncState.PendingToSync);
                    await settingsService.SetPendingToSync(true);

                    #region ASOSA flagSync Ingreso de material
                    Frontend.Core.Commons.Globals.flagSync = "PosicionesDePedidoViewModel";
                    #endregion
                    Toast.ShowMessage("El pedido ha sido finalizado con éxito");
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
            return !notaDeEntrega.DetalleNotaDeEntrega.Any(x => x.DetalleNotaDeEntregaPosicion.Any(dnp => dnp.EsContado));
        }

        private void GoToDetallePedido(PosicionesDePedidoModel posicionDePedidoModel)
        {
            if (posicionDePedidoModel != null)
            {
                navigationService.PushAsync<PosicionesDePedidoView, DetalleDeMaterialPedidoView>(posicionDePedidoModel.detalleNotaDeEntregaPosicion);
            }
        }

        private async Task GoToInformacionPedido()
        {
            if (notaDeEntrega.Pedido != null)
            {
                navigationService.PushAsync<PosicionesDePedidoView, VisualizarInformacionPedidoView>(cabeceraDePedidoModel);
            }
        }

        public async Task Update(DetalleNotaDeEntregaPosicion detalleNotaDeEntregaPosicion)
        {
            if (!notaDeEntregaService.Validate(detalleNotaDeEntregaPosicion.DetalleNotaDeEntrega))
            {
                Toast.ShowMessage("Por favor, complete los campos obligatorios de los materiales seleccionados");
                return;
            }
            detalleNotaDeEntregaPosicion.EsContado = !detalleNotaDeEntregaPosicion.EsContado;
            await notaDeEntregaService.Update(detalleNotaDeEntregaPosicion);
        }
    }
}
