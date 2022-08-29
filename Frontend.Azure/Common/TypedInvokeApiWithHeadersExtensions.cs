using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Azure.Common
{
    public static class TypedInvokeApiWithHeadersExtensions
    {
        public static Task<T> InvokeApiWithHeaders<T>(this MobileServiceClient client, string apiName, IDictionary<string, string> httpHeaders)
        {
            var client2 = new MobileServiceClient(UrlConstants.ApiRestUrl, new AddHeadersHandler(httpHeaders));
            return client2.InvokeApiAsync<T>(apiName);
        }

        public static Task<T> InvokeApiWithHeaders<T>(this MobileServiceClient client, string apiName, HttpMethod method, IDictionary<string, string> httpHeaders, IDictionary<string, string> queryParameters)
        {
            var client2 = new MobileServiceClient(UrlConstants.ApiRestUrl, new AddHeadersHandler(httpHeaders));
            return client2.InvokeApiAsync<T>(apiName, method, queryParameters);
        }

        class AddHeadersHandler : DelegatingHandler
        {
            IDictionary<string, string> headers;

            public AddHeadersHandler(IDictionary<string, string> headers)
            {
                this.headers = headers;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
            {
                foreach (var header in headers)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }

                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}
