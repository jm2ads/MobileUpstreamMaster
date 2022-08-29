using System.Threading;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.iOS.Implementations;

[assembly: Xamarin.Forms.Dependency(typeof(IOSCloseApp))]
namespace Frontend.iOS.Implementations
{
    public class IOSCloseApp : ICloseApplication
    {
        public void CloseApp()
        {
            Thread.CurrentThread.Abort();
        }
    }
}