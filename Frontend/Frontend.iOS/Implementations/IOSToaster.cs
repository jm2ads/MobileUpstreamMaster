using Frontend.Core.Commons.IPlataformControls;
using Foundation;
using Frontend.iOS.Implementations;
using UIKit;


[assembly: Xamarin.Forms.Dependency(typeof(IOSToaster))]
namespace Frontend.iOS.Implementations
{
    public class IOSToaster : IToastControl
    {
        const double SHORT_DELAY = 2.0;
        NSTimer alertDelay;
        UIAlertController alert;

        public void ShowMessage(string message)
        {
            alertDelay = NSTimer.CreateScheduledTimer(SHORT_DELAY, (obj) =>
            {
                dismissMessage();
            });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        void dismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}