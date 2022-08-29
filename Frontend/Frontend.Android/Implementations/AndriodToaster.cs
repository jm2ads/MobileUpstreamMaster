using Android.App;
using Android.Widget;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.Droid.Implementations;

[assembly: Xamarin.Forms.Dependency(typeof(AndriodToaster))]
namespace Frontend.Droid.Implementations
{
    public class AndriodToaster : IToastControl
    {
        public void ShowMessage(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
    }
}