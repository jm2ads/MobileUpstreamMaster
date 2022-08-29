using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.ViewModels;

namespace Frontend.Core.Areas.Inventarios.ViewModels
{
    public class AprobacionInventarioViewModel : BaseViewModel, IAprobacionInventarioViewModel
    {
        public AprobacionInventarioViewModel()
        {
            Title = "Aprobación de inventario";
        }
    }
}
