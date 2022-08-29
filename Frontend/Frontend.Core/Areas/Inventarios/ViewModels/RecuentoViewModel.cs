using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.ViewModels;

namespace Frontend.Core.Areas.Inventarios.ViewModels
{
    public class RecuentoViewModel : BaseViewModel, IRecuentoViewModel
    {
        public RecuentoViewModel()
        {
            Title = "Recuento inventario";
        }
    }
}
