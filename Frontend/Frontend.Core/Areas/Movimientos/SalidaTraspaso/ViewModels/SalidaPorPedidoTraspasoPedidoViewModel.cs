using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.SalidasInternas;
using Frontend.Business.Movimientos.SalidasInternas.Core;
using Frontend.Core.Areas.Movimientos.SalidaTraspaso.IViewModels;
using Frontend.Core.Areas.Views;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.ViewModels;
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
    public class SalidaPorPedidoTraspasoPedidoViewModel : BaseViewModel, ISalidaPorPedidoTraspasoPedidoViewModel
    {
        public ICommand GetAllSalidasCommand { get; set; }
        public ICommand GoToCabeceraDeSalidaInternaCommand { get; set; }
        private System.Collections.IList listNumeroDeSalidas;
        public System.Collections.IList ListNumeroDeSalidas
        {
            get { return listNumeroDeSalidas; }
            set
            {
                SetProperty(ref listNumeroDeSalidas, value);
            }
        }

        private IList<SalidaInterna> listSalidas;
        public IList<SalidaInterna> ListSalidas
        {
            get { return listSalidas; }
            set
            {
                SetProperty(ref listSalidas, value);
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
        private readonly ISalidaInternaService salidaInternaService;
        private readonly SalidaInternaFactory salidaInternaFactory;
        public SalidaPorPedidoTraspasoPedidoViewModel(INavigationService navigationService, ISalidaInternaService salidaInternaService, SalidaInternaFactory salidaInternaFactory)
        {
            Title = "Pedido";
            this.navigationService = navigationService;
            this.salidaInternaService = salidaInternaService;
            this.salidaInternaFactory = salidaInternaFactory;
            Init();

        }

        private async void Init()
        {
            ListSalidas = new List<SalidaInterna>();
            GetAllSalidasCommand = new Command(async () => await GetAllSalidas());
            GoToCabeceraDeSalidaInternaCommand = new Command(async () => await GoToCabeceraDeSalidaInterna());
            await GetAllSalidas();
        }

        private async Task GetAllSalidas()
        {
            try
            {
                ListSalidas = await salidaInternaService.GetAllBy(EstadoMovimiento.Recibir, ClaseDeMovimientoSalidaInterna.ClaseDeMovimiento[ClaseDeMovimientoSalidaInterna.CLASE_351]);
                ListNumeroDeSalidas = ListSalidas.Select(x => x.NumeroPedido.TrimStart('0')).Distinct().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async Task GoToCabeceraDeSalidaInterna()
        {
            try
            {
                var salida = await salidaInternaService.GetWithChildren(ListSalidas.FirstOrDefault(x => x.NumeroPedido == SearchValue).Id);
                navigationService.PushAsync<SalidaPedidoTraspasoView, CabeceraSalidaTraspasoView>(salida);
            }
            catch (System.Exception e)
            {
                Toast.ShowMessage("Por favor, ingrese un número válido y vuelva a intentar.");
            }
        }
    }
}
