using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Reservas;
using Frontend.Core.Areas.Movimientos.Reservas.IViewModels;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Movimientos.Reservas.ViewModels
{
    public class BusquedaReservaViewModel : BaseViewModel, IBusquedaReservaViewModel
    {
        public ICommand SearchCommand { get; set; }
        public ICommand GetAllCommand { get; set; }

        private System.Collections.IList _listCodigoReserva;
        public System.Collections.IList ListCodigoReserva
        {
            get { return _listCodigoReserva; }
            set
            {
                SetProperty(ref _listCodigoReserva, value);
            }
        }
        public IList<Tuple<string, int>> NumeroReservaList { get; set; }

        private string _searchValue;
        private readonly INavigationService navigationService;
        private readonly IReservaService reservaService;

        public string SearchValue
        {
            get { return _searchValue; }
            set { SetProperty(ref _searchValue, value); }
        }

        public BusquedaReservaViewModel(INavigationService navigationService, IReservaService reservaService)
        {
            this.navigationService = navigationService;
            this.reservaService = reservaService;
            Init();
        }
        private async void Init()
        {
            Title = "Reserva";
            SearchCommand = new Command(Search);
            NumeroReservaList = new List<Tuple<string, int>>();
            await InitAsync();
        }

        private async Task InitAsync()
        {
            var reservas = await reservaService.GetAllBy(EstadoMovimiento.Recibir, EstadoMovimiento.RechazadoSap);

            foreach (var item in reservas)
            {
                NumeroReservaList.Add(new Tuple<string, int>(item.Numero.TrimStart('0'), item.Id));
            }

            ListCodigoReserva = NumeroReservaList?.Select(x => x.Item1).ToList();
        }

        private async void Search()
        {
            try
            {
                var reserva = await reservaService.GetWithChildren(NumeroReservaList.FirstOrDefault(x => x.Item1 == SearchValue).Item2);
                if (reserva.TipoReserva == TipoReserva.Devolucion)
                {
                    navigationService.PushAsync<BusquedaReservaView, CabeceraDevolucionView>(reserva);
                }
                else
                {
                    navigationService.PushAsync<BusquedaReservaView, CabeceraSalidaView>(reserva);
                }
            }
            catch (System.Exception e)
            {
                Toast.ShowMessage("Por favor, ingrese un número válido y vuelva a intentar.");
            }
        }
    }
}
