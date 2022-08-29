using System.Windows.Input;

namespace Frontend.Core.Areas.Home.IViewModels
{
    public interface IHomeViewModel
    {
        ICommand AlertCommand { get; set; }
        ICommand RefreshSettingsCommand { get; set; }
       
        ICommand SyncCommand { get; set; }

        ICommand SyncOnDemandCommand { get; set; }

    }
}
