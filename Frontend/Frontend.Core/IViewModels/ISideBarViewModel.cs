using System.Windows.Input;

namespace Frontend.Core.IViewModels
{
    public interface ISideBarViewModel
    {
        ICommand GetSettingsCommand { get; set; }
        ICommand CollapseCommand { get; set; }
        ICommand HeaderTapCommand { get; set; }
    }
}
