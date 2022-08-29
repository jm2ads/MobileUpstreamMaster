using System.Windows.Input;

namespace Frontend.Core.Areas.Inventarios.IViewModels
{
    public interface IRecuentoInventarioViewModel
    {
        ICommand GetAllInventarioCommand { get; set; }
    }
}
