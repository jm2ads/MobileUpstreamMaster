using Android.OS;
using Frontend.Commons.Commons;
using Frontend.Droid.Implementations;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidDeviceData))]
namespace Frontend.Droid.Implementations
{
    public class AndroidDeviceData : IDeviceInformation
    {
        public string GetSerial()
        {
            return Build.Serial == Build.Unknown ? string.Empty : Build.Serial;
        }

        public string GetManufacturer()
        {
            return Build.Manufacturer;
        }

        public string GetUuid()
        {
            return Build.Serial == Build.Unknown ? string.Empty : Build.Serial + Build.Id;
        }

        public string Manufacturer()
        {
            return Build.Manufacturer;
        }
    }
}