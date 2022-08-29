using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Frontend.Commons.Commons
{
    public static class ApplicationHelper
    {
        public static Dictionary<string, string> ParseObjectToDictionary(Object someObject)
        {
            var result = someObject?.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .ToDictionary(prop => prop.Name, prop => (prop.GetValue(someObject, null) == null) ? string.Empty : prop.GetValue(someObject, null).ToString());

            return result;
        }
    }
}
