
using Frontend.Commons.Commons;
using Frontend.iOS.Implementations;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(IosDeviceInformation))]
namespace Frontend.iOS.Implementations
{
    public class IosDeviceInformation : IDeviceInformation
    {
        public string GetSerial()
        {
            return GetUuid();
        }

        public string GetManufacturer()
        {
            return "apple";
        }

        public string GetUuid()
        {
            return UIDevice.CurrentDevice.IdentifierForVendor.AsString();
        }
    }
}
