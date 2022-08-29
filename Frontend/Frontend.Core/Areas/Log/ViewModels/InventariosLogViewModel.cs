using Frontend.Business.Inventarios;
using Frontend.Core.Areas.Log.IViewModels;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.ViewModels;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Log.ViewModels
{
    public class InventariosLogViewModel : BaseViewModel, IInventariosLogViewModel 
    {
        private readonly IDisplayAlertService displayAlertService;
        private readonly INavigationService navigationService;
        private readonly IInventarioService inventarioService;

        public ICommand GetInventariosLogsCommand { get; set; }

        public ObservableRangeCollection<InventarioLog> ListaInventarioLog { get; set; }
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

        private bool _hasInventario = false;
        public bool HasInventario
        {
            get { return _hasInventario; }
            set
            {
                SetProperty(ref _hasInventario, value);
            }
        }

        public InventariosLogViewModel(IDisplayAlertService displayAlertService, INavigationService navigationService, IInventarioService inventarioService)
        {
            this.displayAlertService = displayAlertService;
            this.navigationService = navigationService;
            this.inventarioService = inventarioService;
            Init();
        }

        private void Init()
        {
            Title = "Inventario";
            GetInventariosLogsCommand = new Command(async () => await RefreshLogsInventarios());
            ListaInventarioLog = new ObservableRangeCollection<InventarioLog>();
            Refresh = GetAllLogs();
        }

        private async Task RefreshLogsInventarios()
        {
            IsRefreshing = true;
            await GetAllLogs();
            IsRefreshing = false;
        }

        private async Task GetAllLogs()
        {
            ListaInventarioLog.Clear();
            List<InventarioLog> list = (List<InventarioLog>)await inventarioService.GetAllInventarioLogError();
            list = list.OrderByDescending(i => i.Id).ToList();
            ListaInventarioLog.AddRange(list);
            HasInventario = ListaInventarioLog.Count > 0;
        }
    }
}
