using Frontend.Core.Areas.Movimientos.Devoluciones.IViewModels;
using Frontend.Core.ViewModels;

namespace Frontend.Core.Areas.Movimientos.Devoluciones.ViewModels
{
    public class DevolucionViewModel : BaseViewModel, IDevolucionViewModel
    {
        public DevolucionViewModel()
        {
            Title = "Devolución de material";
        }
    }
}
