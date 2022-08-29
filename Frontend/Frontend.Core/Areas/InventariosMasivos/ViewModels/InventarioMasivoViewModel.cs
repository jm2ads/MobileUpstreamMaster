using Frontend.Core.Areas.InventariosMasivos.IViewModels;
using Frontend.Core.ViewModels;

namespace Frontend.Core.Areas.InventariosMasivos.ViewModels
{
    public class InventarioMasivoViewModel: BaseViewModel, IInventarioMasivoViewModel
    {
        public InventarioMasivoViewModel()
        {
            Title = "Inventario masivo";
        }
    }
}
