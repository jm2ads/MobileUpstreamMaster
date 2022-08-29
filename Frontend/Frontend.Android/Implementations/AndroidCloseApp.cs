using Frontend.Core.Commons.IPlataformControls;
using Frontend.Droid.Implementations;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidCloseApp))]
namespace Frontend.Droid.Implementations
{
    public class AndroidCloseApp : ICloseApplication
    {
        public void CloseApp()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}