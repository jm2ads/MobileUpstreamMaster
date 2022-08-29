using System;
using System.Net;
using System.Text.RegularExpressions;
using Frontend.Core.Commons.IPlataformControls;

[assembly: Xamarin.Forms.Dependency(typeof(Frontend.iOS.Implementations.IOSUrlEmail))]
namespace Frontend.iOS.Implementations
{
    public class IOSUrlEmail : IUrlEmailConverter
    {

        public string GetUrl(string email, string appname, string version, string release, string enviroment)
        {
            var subj = appname + "%20/%20V" + version + "%20/%20B" + release + "%20/%20+" + enviroment;
            string shareurl = String.Empty;
            var subject = Regex.Replace(subj, @"[^\u0000-\u00FF]", string.Empty);
            shareurl = "mailto:" + email + "?subject=" + WebUtility.UrlEncode(subject);

            return shareurl;
        }
    }
}
