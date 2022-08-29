using Frontend.Business.Movimientos.Ingresos;
using Frontend.Business.Settings;
using Frontend.Core.Areas.Movimientos.Ingresos.IViewModels;
using Frontend.Core.Areas.Movimientos.Ingresos.Models;
using Frontend.Core.Areas.Views;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.ViewModels;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;

namespace Frontend.Core.Areas.Movimientos.Ingresos.ViewModels
{
    public class VisualizarInformacionPedidoViewModel : BaseViewModel, IVisualizarInformacionPedidoViewModel
    {
        private string _fechaDocumento;
        public string FechaDocumento
        {
            get { return _fechaDocumento; }
            set
            {
                SetProperty(ref _fechaDocumento, value);
            }
        }

        private string _fechaContabilizacion;
        public string FechaContabilizacion
        {
            get { return _fechaContabilizacion; }
            set
            {
                SetProperty(ref _fechaContabilizacion, value);
            }
        }

        private string _claseMovimiento;
        public string claseMovimiento
        {
            get { return _claseMovimiento; }
            set
            {
                SetProperty(ref _claseMovimiento, value);
            }
        }

        private Pedido pedido;
        public Pedido Pedido
        {
            get { return pedido; }
            set
            {
                SetProperty(ref pedido, value);
            }
        }

        private IDictionary<int, string> claseDeMovimientoPedido;

        private Setting _setting;
        public Setting setting
        {
            get { return _setting; }
            set
            {
                SetProperty(ref _setting, value);
            }
        }

        private NotaDeEntrega notaDeEntrega;
        public NotaDeEntrega NotaDeEntrega
        {
            get { return notaDeEntrega; }
            set
            {
                SetProperty(ref notaDeEntrega, value);
            }
        }

        private CabeceraDePedidoModel _cabeceraDePedidoModel;
        public CabeceraDePedidoModel cabeceraDePedidoModel
        {
            get { return _cabeceraDePedidoModel; }
            set
            {
                SetProperty(ref _cabeceraDePedidoModel, value);
            }
        }

        private readonly INavigationService navigationService;
        private readonly ISettingsService settingsService;

        public VisualizarInformacionPedidoViewModel(INavigationService navigationService, ISettingsService settingsService)
        {
            this.navigationService = navigationService;
            this.settingsService = settingsService;
            Init();
        }

        private async void Init()
        {
            cabeceraDePedidoModel = navigationService.GetNavigationParams<PosicionesDePedidoView>() as CabeceraDePedidoModel;
            NotaDeEntrega = cabeceraDePedidoModel.notaDeEntrega;
            claseMovimiento = cabeceraDePedidoModel.claseMovimiento;
            Pedido = notaDeEntrega.Pedido;
            FechaDocumento = notaDeEntrega.FechaDocumento.ToString("dd/MM/yyyy");
            FechaContabilizacion = notaDeEntrega.FechaContabilizacion.ToString("dd/MM/yyyy").Equals("01/01/0001") ? "-" : notaDeEntrega.FechaContabilizacion.ToString("dd/MM/yyyy");
            setting = await settingsService.GetWithChildren();
            Title = "Pedido " + pedido.NumeroPedido;
        }
    }
}
