using System.Windows.Input;

namespace Frontend.Core.Areas.Inventarios.IViewModels
{
    public interface IAprobacionInventarioProvisorioViewModel
    {
        ICommand GetInventariosProvisoriosAprobacionCommand { get; set; }
    }
}
