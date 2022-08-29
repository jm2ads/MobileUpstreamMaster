using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Frontend.Commons.Commons;
using Frontend.Commons.Commons.Errors;
using Newtonsoft.Json;

namespace Frontend.WebApi.Common
{
    public class YpfHttpClient
    {
        private HttpClient httpClient;

        private SecurityRequestFactory securityRequest;

        public YpfHttpClient(SecurityRequestFactory securityRequest)
        {
            this.securityRequest = securityRequest;
            httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(ApplicationConstants.TimeoutTime)
            };
        }

        public async Task<string> DoGet(Uri uri)
        {
            var response = await httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                return String.Empty;
            }
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DoGetWithParams(Uri uri, StringContent content)
        {
            throw new NotImplementedException();
        }

        public async Task<string> DoPost(Uri uri, StringContent content)
        {
            var request = await securityRequest.GetRequestPost(uri, content);
            try
            {
                var response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    await ValidateHttpRespose(response);
                    return String.Empty;
                }
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                ValidateException(ex);
                return String.Empty;
            }
        }

        public async Task DoUpdate(Uri uri, StringContent content)
        {
          await httpClient.PutAsync(uri, content); 
        }

        public async void DoDelete(Uri uri, int id)
        {
            await httpClient.DeleteAsync(uri + id.ToString());
        }

        private void ValidateException(Exception ex)
        {
            if (ex is BusinessException)
            {
                throw ex;
            }
            if (ex is TaskCanceledException)
            {
                throw new BusinessException(BusinessErrorCode.Timeout, "No se pudo conectar con el servidor");
            }
        }

        private async Task ValidateHttpRespose(HttpResponseMessage res)
        {
            var contentResponse = await res.Content.ReadAsStringAsync();
            switch (res.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new BusinessException("404", "Recurso no encontrado");
                // Business Exception de backend
                case (HttpStatusCode)418:                     
                    var businessException = JsonConvert.DeserializeObject<BusinessException>(contentResponse);
                    throw new BusinessException(businessException.Codigo, businessException.Mensaje);
                case (HttpStatusCode)500:
                    throw new BusinessException(BusinessErrorCode.ErrorInterno, "Error en el servidor.");
            }
        }
    }
}
