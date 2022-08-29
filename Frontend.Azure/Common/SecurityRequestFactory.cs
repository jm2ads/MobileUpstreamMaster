using Frontend.Business.Commons;
using Frontend.Business.Settings;
using Frontend.Commons.Commons;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Frontend.Azure.Common
{
    public class SecurityRequestFactory
    {
        #region Private Properties

        private readonly ISettingsService settingsService;

        #endregion

        #region Public Methods

        public SecurityRequestFactory(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public async Task<IDictionary<string, string>> GetHeaders()
        {
            var headers = new Dictionary<string, string>();

            var device = DependencyService.Get<IDeviceInformation>();
            var setting = await settingsService.GetWithChildren();

            if (setting != null)
            {
                headers.Add("User", setting.UsuarioActivo.IdRed);
                headers.Add("Token", setting.UsuarioActivo.Token);
                headers.Add("App", ApplicationConstants.ApplicationNameSecurity);
                headers.Add("Serial", device.GetSerial());
                headers.Add("Uuid", device.GetUuid());
            }

            return headers;
        }

        #endregion
    }
}
