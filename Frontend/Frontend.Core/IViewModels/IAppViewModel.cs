using System.Windows.Input;

namespace Frontend.Core.IViewModels
{
    public interface IAppViewModel
    {
        ICommand ValidateUserSettingsCommand { get; }
    }
}
