using Android.Content;
using Android.Provider;
using Android.Support.V7.App;
using Android.Text.Format;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.Droid.Implementations;
using Android.App.Usage;
using Android.App;
using Frontend.Core.Commons.Timer;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidTimer))]
namespace Frontend.Droid.Implementations
{
    public class AndroidTimer : Application, ITimerConfiguration
    {
        public TimerMode GetTimeFormat()
        {
            return DateFormat.Is24HourFormat(Context) ? TimerMode.TwentyFourHours : TimerMode.TwelveHours;
        }
    }
}