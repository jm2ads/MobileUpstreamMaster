using Frontend.Core.Areas.Log.IViewModels;
using Frontend.Core.ViewModels;
using Frontend.IServices.IServices;

namespace Frontend.Core.Areas.Log.ViewModels
{
    public class LogViewModel : BaseViewModel, ILogViewModel
    {
        private readonly ISettingsService settingsService;

        public LogViewModel(ISettingsService settingsService)
        {
            Title = "Lista de fallas";
            this.settingsService = settingsService;
            settingsService.ResetHasSyncWithError();
        }
    }
}
