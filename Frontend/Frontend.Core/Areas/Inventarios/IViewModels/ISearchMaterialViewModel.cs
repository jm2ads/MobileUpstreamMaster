using System.Windows.Input;

namespace Frontend.Core.IViewModels
{
    public interface ISearchMaterialViewModel
    {
        ICommand GetAllMaterialCommand { get; set; }
    }
}
