using Frontend.Core.Areas.Movimientos.Reservas.IViewModels;
using Frontend.Core.ViewModels;

namespace Frontend.Core.Areas.Movimientos.Reservas.ViewModels
{
    public class ReservaViewModel : BaseViewModel, IReservaViewModel
    {
        public ReservaViewModel()
        {
            Title = "Devolución/Salida de materiales";
        }
    }
}
