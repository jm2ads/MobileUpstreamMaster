using System.Windows.Input;

namespace Frontend.Core.Areas.Inventarios.IViewModels
{
    public interface IRecuentoDetalleMaterialViewModel
    {
        ICommand GetClasesDeValoracionCommand { get; set; }
    }
}
