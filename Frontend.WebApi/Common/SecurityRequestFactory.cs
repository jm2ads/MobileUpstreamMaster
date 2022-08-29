
using Frontend.Business.Settings.Searchers;
using Frontend.Commons.Commons;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Frontend.WebApi.Common
{
    public class SecurityRequestFactory
    {
        private readonly SettingSearcher settingSearcher;

        public SecurityRequestFactory(SettingSearcher settingSearcher)
        {
            this.settingSearcher = settingSearcher;
        }

        public async Task<HttpRequestMessage> GetRequestPost(Uri uri, HttpContent content)
        {
            var device = DependencyService.Get<IDeviceInformation>();
            var setting = await settingSearcher.GetWithChildren();
            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            request.Headers.Add("User", setting.UsuarioActivo?.IdRed);
            request.Headers.Add("Token", setting.UsuarioActivo?.Token);
            request.Headers.Add("App", ApplicationConstants.ApplicationNameSecurity);
            request.Headers.Add("Serial", device.GetSerial());
            request.Headers.Add("Uuid", device.GetUuid());

            request.Content = content;

            return request;
        }

    }
}
