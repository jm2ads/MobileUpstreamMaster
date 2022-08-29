using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Ingresos;
using Frontend.Core.Areas.Movimientos.Ingresos.IViewModels;
using Frontend.Core.Areas.Views;
using Frontend.Core.Commons.Navigation;
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
    public class IngresoCompraPorPedidoViewModel : BaseViewModel, IIngresoCompraPorPedidoViewModel
    {
        public ICommand GetAllPedidosCommand { get; set; }
        public ICommand GoToCabeceraDePedidoCommand { get; set; }
        private System.Collections.IList listNumeroDePedidos;
        public System.Collections.IList ListNumeroDePedidos
        {
            get { return listNumeroDePedidos; }
            set
            {
                SetProperty(ref listNumeroDePedidos, value);
            }
        }

        private IList<Pedido> listPedidos;
        public IList<Pedido> ListPedidos
        {
            get { return listPedidos; }
            set
            {
                SetProperty(ref listPedidos, value);
            }
        }

        private string searchValue;
        public string SearchValue
        {
            get
            {
                return searchValue;
            }
            set
            {
                SetProperty(ref searchValue, value);
            }
        }

        private readonly INavigationService navigationService;
        private readonly IPedidoService pedidoService;

        public IngresoCompraPorPedidoViewModel(INavigationService navigationService, IPedidoService pedidoService)
        {
            Title = "Pedido";
            this.navigationService = navigationService;
            this.pedidoService = pedidoService;
            Init();

        }

        private async void Init()
        {
            ListPedidos = new List<Pedido>();
            GetAllPedidosCommand = new Command(async () => await GetAllPedidos());
            GoToCabeceraDePedidoCommand = new Command(async () => await GoToCabeceraDePedido());
            await GetAllPedidos();
        }

        private async Task GetAllPedidos()
        {
            try
            {
                ListPedidos = await pedidoService.GetAllBy(EstadoMovimiento.Recibir);
                ListNumeroDePedidos = ListPedidos.Select(x => x.NumeroPedido.TrimStart('0')).Distinct().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async Task GoToCabeceraDePedido()
        {
            try
            {
                var pedido = await pedidoService.GetWithChildren(ListPedidos.FirstOrDefault(x => x.NumeroPedido == SearchValue).Id);
                navigationService.PushAsync<IngresoCompraPorPedidoView, CabeceraDePedidoView>(pedido);
            }
            catch (System.Exception e)
            {
                Toast.ShowMessage("Por favor, ingrese un número válido y vuelva a intentar.");
            }
        }
    }
}
