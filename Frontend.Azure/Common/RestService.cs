using System;

namespace Frontend.Azure.Common
{
    public class RestService
    {
        public Uri ConvertToUri(string endpoint)
        {
            return new Uri(string.Format(endpoint));
        }
    }
}
