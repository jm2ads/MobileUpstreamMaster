using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Reservas;
using Frontend.Core.Areas.Movimientos.Devoluciones.IViewModels;
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

namespace Frontend.Core.Areas.Movimientos.Devoluciones.ViewModels
{
    public class DevolucionReservaViewModel : BaseViewModel, IDevolucionReservaViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IReservaService reservaService;
        private readonly ISettingsService settingsService;

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

        public string SearchValue
        {
            get { return _searchValue; }
            set { SetProperty(ref _searchValue, value); }
        }

        public DevolucionReservaViewModel(INavigationService navigationService, IReservaService reservaService,
            ISettingsService settingsService)
        {
            Title = "Reserva";
            this.navigationService = navigationService;
            this.reservaService = reservaService;
            this.settingsService = settingsService;
            Init();
        }

        private async void Init()
        {
            SearchCommand = new Command(Search);
            NumeroReservaList = new List<Tuple<string, int>>();
            await InitAsync();
        }

        private async Task InitAsync()
        {
            var reservas = await reservaService.GetAllBy(TipoReserva.Devolucion, EstadoMovimiento.Recibir, EstadoMovimiento.RechazadoSap);

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
                navigationService.PushAsync<DevolucionReservaView, CabeceraDevolucionView>(reserva);
            }
            catch (System.Exception e)
            {
                Toast.ShowMessage("Por favor, ingrese un número válido y vuelva a intentar.");
            }
        }
    }
}
