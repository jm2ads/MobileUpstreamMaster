
using System.Windows.Input;

namespace Frontend.Core.Areas.AboutUs.IViewModels
{
    public interface IAboutUsViewModel
    {
        ICommand OpenEmailAppCommand { get; set; }
    }
}
