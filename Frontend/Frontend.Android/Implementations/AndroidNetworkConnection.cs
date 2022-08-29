using Android.App;
using Android.Content;
using Android.Net;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.Droid.Implementations;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidNetworkConnection))]
namespace Frontend.Droid.Implementations
{
    public class AndroidNetworkConnection : INetworkConnection
    {
        public bool IsConnected { get; set; }

        public void CheckConnection()
        {
            var connectivityManager = Application.Context.GetSystemService(Context.ConnectivityService) as ConnectivityManager;
            var activeNetwork = connectivityManager.ActiveNetworkInfo;
            IsConnected = activeNetwork != null && activeNetwork.IsConnected && IsValidTypeConnection(activeNetwork);
        }

        private bool IsValidTypeConnection(NetworkInfo network)
        {
            return network.Type == ConnectivityType.Mobile || network.Type == ConnectivityType.Wifi;
        }
    }
}