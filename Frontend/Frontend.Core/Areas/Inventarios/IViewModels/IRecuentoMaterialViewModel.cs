using System.Windows.Input;

namespace Frontend.Core.Areas.Inventarios.IViewModels
{
    public interface IRecuentoMaterialViewModel
    {
        ICommand GetAllMaterialCommand { get; set; }
    }
}
