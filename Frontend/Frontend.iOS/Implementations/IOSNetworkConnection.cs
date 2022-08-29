using Frontend.Core.Commons.IPlataformControls;
using Frontend.iOS.Implementations;
using Frontend.iOS.Network;

[assembly: Xamarin.Forms.Dependency(typeof(IOSNetworkConnection))]
namespace Frontend.iOS.Implementations
{
    public class IOSNetworkConnection :INetworkConnection
    {
        public bool IsConnected { get; set; }

        public void CheckConnection()
        {
            var status = Reachability.InternetConnectionStatus();
            IsConnected = status == NetworkStatus.ReachableViaWiFiNetwork || status == NetworkStatus.ReachableViaCarrierDataNetwork ;
        }
    }
}