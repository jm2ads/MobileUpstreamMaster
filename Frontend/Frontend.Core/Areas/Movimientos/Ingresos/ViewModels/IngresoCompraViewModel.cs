using Frontend.Core.Areas.Movimientos.Ingresos.IViewModels;
using Frontend.Core.ViewModels;
using Frontend.IServices.IServices;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Movimientos.Ingresos.ViewModels
{
    public class IngresoCompraViewModel : BaseViewModel, IIngresoCompraViewModel
    {
        private readonly ISyncService syncService;

        public ICommand UploadCommand { get; set; }

        public IngresoCompraViewModel(ISyncService syncService)
        {
            Title = "Ingreso por pedido de compras";
            UploadCommand = new Command(Upload);
            this.syncService = syncService;
        }

        private async void Upload()
        {
            await StartSpinner("Sicronizando pedidos");
            await syncService.UploadPedidos();
            await StopSpinner();
        }
    }
}
