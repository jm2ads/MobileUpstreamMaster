using System.Threading.Tasks;
using Xamarin.Forms;

namespace Frontend.Core.Commons.Alerts
{
    public class DisplayAlertService : IDisplayAlertService
    {
        public void Show(string title, string message, string accept)
        {
            Application.Current.MainPage.DisplayAlert(title, message, accept);
        }

        public async Task<bool> Show(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            return Application.Current.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
        }
    }
}
