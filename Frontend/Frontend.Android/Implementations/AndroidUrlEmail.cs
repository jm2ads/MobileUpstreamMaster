using System;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.Droid.Implementations;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidUrlEmail))]
namespace Frontend.Droid.Implementations
{
    public class AndroidUrlEmail : IUrlEmailConverter
    {
        public string GetUrl(string email, string appname, string version, string release, string enviroment)
        {
            var subject = appname + "%20/%20V" + version + "%20/%20B" + release + "%20/%20+" + enviroment;
            return "mailto:" + email + "?subject=" + subject; 
        }
    }
}