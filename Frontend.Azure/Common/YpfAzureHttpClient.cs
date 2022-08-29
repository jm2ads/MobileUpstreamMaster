using Frontend.Commons.Commons;
using Frontend.Commons.Commons.Errors;
using Microsoft.AppCenter.Crashes;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Frontend.Azure.Common
{
    public class YpfAzureHttpClient
    {
        #region Private Properties

        //private MobileServiceClient httpClientAzure;

        private HttpClient httpClient;

        private SecurityRequestFactory securityRequest;

        #endregion

        #region Private Methods

        private StringContent SerializeObjectToJson(object objectToSerialize)
        {
            var json = JsonConvert.SerializeObject(objectToSerialize);
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return httpContent;
        }

        private void ValidateException(Exception ex, string apiName)
        {
            if (ex is BusinessException)
            {
                string mensaje;
                var be = ex as BusinessException;
                mensaje = be.Mensaje;

                Crashes.TrackError(ex, new Dictionary<string, string>{
                        { "Message", mensaje },
                        { "Location", apiName },
                        { "Title", "Error en sincronización" }
                    });
                throw ex;
            }
            if (ex is TaskCanceledException)
            {
                Crashes.TrackError(ex, new Dictionary<string, string>{
                        { "Message", ex.Message },
                        { "Location", apiName },
                        { "Title", "Error en sincronización" }
                    });
                throw new BusinessException(BusinessErrorCode.Timeout, "No se pudo conectar con el servidor");
            }

            Crashes.TrackError(ex, new Dictionary<string, string>{
                        { "Location", apiName },
                        { "Message", ex.Message},
                        { "Title", "Error en sincronización" }
                    });
            if (ex is MobileServiceInvalidOperationException && (ex as MobileServiceInvalidOperationException).Response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new BusinessException(BusinessErrorCode.UsuarioContraseñaInvalidos, "Token inválido, vuelva a ingresar las credenciales.");
            }
            throw new BusinessException(BusinessErrorCode.ErrorInterno, "Error del servidor no detectado.");
        }

        #endregion

        #region Public Methods

        public YpfAzureHttpClient(SecurityRequestFactory securityRequest)
        {
            this.securityRequest = securityRequest;

            //httpClientAzure = new MobileServiceClient(UrlConstants.ApiRestUrl);

            //TODO. Determinar si se elimina este client.
            httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(ApplicationConstants.TimeoutTime)
            };
        }

        public async Task<E> CallWithHeaders<E>(string apiName, object body, HttpMethod method, object parameters)
        {
            try
            {
                var headers = await securityRequest.GetHeaders();
                var parametersDictionary = ApplicationHelper.ParseObjectToDictionary(parameters);
                var body2 = SerializeObjectToJson(body);
                var httpClientAzure = new MobileServiceClient(UrlConstants.ApiRestUrl);
                var response = await httpClientAzure.InvokeApiAsync(apiName, body2, method, headers, parametersDictionary);
                var result = await response.Content.ReadAsStringAsync();
                var ret = JsonConvert.DeserializeObject<E>(result);
                return ret;
            }
            catch (Exception ex)
            {
                ValidateException(ex, apiName);
                return default(E);
            }
        }

        public async Task CallWithHeaders(string apiName, object body, HttpMethod method, object parameters)
        {
            try
            {
                var headers = await securityRequest.GetHeaders();
                var parametersDictionary = ApplicationHelper.ParseObjectToDictionary(parameters);
                var body2 = SerializeObjectToJson(body);
                var httpClientAzure = new MobileServiceClient(UrlConstants.ApiRestUrl);
                var response = await httpClientAzure.InvokeApiAsync(apiName, body2, method, headers, parametersDictionary);
                var result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                ValidateException(ex, apiName);
            }
        }

        public async Task<E> Call<E>(string apiName, object body, HttpMethod method, object parameters)
        {
            try
            {
                var parametersDictionary = ApplicationHelper.ParseObjectToDictionary(parameters);
                var bodyJson = SerializeObjectToJson(body);
                var httpClientAzure = new MobileServiceClient(UrlConstants.ApiRestUrl);
                var response = await httpClientAzure.InvokeApiAsync(apiName, bodyJson, method, null, parametersDictionary);
                var result = await response.Content.ReadAsStringAsync();
                var ret = JsonConvert.DeserializeObject<E>(result);
                return ret;
            }
            catch (Exception ex)
            {
                ValidateException(ex, apiName);
                return default(E);
            }
        }

        #endregion
    }
}
