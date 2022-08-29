
using Frontend.Commons.Commons;
using System;
using System.Collections.Generic;

namespace Frontend.WebApi.Common
{
    public class UrlConstants
    {
        public const string ApiRestUrl = ApplicationConstants.BaseApiRest;

        public const string EmployeeApi = ApiRestUrl + "api/Employee/";

        public const string CreateEmployeeListApi = ApiRestUrl + "api/Employee/CreateList";

        public const string CountryApi = ApiRestUrl + "api/EmployeeCountry";
        
        public const string LoginUser = ApiRestUrl + "Api/Login/LoginUser";

        public const string RegisterUser = ApiRestUrl + "Api/Login/RegisterUser";

        public const string ValidateToken = ApiRestUrl + "Api/Login/ValidateUser";
    }
}
