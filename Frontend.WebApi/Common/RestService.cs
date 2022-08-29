using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Frontend.WebApi.Common
{
    public class RestService
    {
        protected Uri baseUri { get; set; }

        protected StringContent SerializeObjectToJson<T>(T objectToSerialize)
        {
            var json = JsonConvert.SerializeObject(objectToSerialize);

            var httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return httpContent;
        }

        protected Uri ConvertToUri(string endpoint)
        {
            return new Uri(string.Format(endpoint));
        }
    }
}
