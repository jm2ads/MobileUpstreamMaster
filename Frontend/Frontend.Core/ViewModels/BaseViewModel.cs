using Frontend.Core.Commons.CustomRenders;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.Core.Commons.Observables;
using Rg.Plugins.Popup.Contracts;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Frontend.Core.ViewModels
{
    public class BaseViewModel : NotificationObject
    {

        #region ASOSA ACTIVITY INDICATOR
        private bool _isBusyOnDemand;

        public bool IsBusyOnDemand
        {
            get => _isBusyOnDemand;
            set
            {
                _isBusyOnDemand = value;
                OnPropertyChanged();
            }
        }
        #endregion



        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title;
        private readonly IPopupNavigation popupNavigation;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        
        public IToastControl Toast
        {
            get
            {
                return DependencyService.Get<IToastControl>();
            }
        }

        public async Task StartSpinner(string message = null)
        {
            await popupNavigation.PushAsync(new SpinnerView(message), true);
        }

        public async Task StopSpinner()
        {
            if (popupNavigation.PopupStack.Count > 0)
            {
                await popupNavigation.PopAllAsync();
            }
        }

        public BaseViewModel()
        {
            this.popupNavigation = Rg.Plugins.Popup.Services.PopupNavigation.Instance; 
        }
    }
}
