using Frontend.Core.Areas.Movimientos.Salidas.IViewModels;
using Frontend.Core.ViewModels;

namespace Frontend.Core.Areas.Movimientos.Salidas.ViewModels
{
    public class SalidaViewModel : BaseViewModel, ISalidaViewModel
    {
        public SalidaViewModel()
        {
            Title = "Salida de materiales";
        }
    }
}
