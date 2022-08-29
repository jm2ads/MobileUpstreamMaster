
using Foundation;
using Frontend.Commons.Commons;
using Frontend.iOS.Implementations;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(IOSApplicationData))]
namespace Frontend.iOS.Implementations
{
    public class IOSApplicationData : IApplicationInformation
    {
        public string GetBuildNumber()
        {
            var a = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();
            return a;
        }

        public string GetVersionNumber()
        {
            var a = NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString();
            return a;
        }
    }
}