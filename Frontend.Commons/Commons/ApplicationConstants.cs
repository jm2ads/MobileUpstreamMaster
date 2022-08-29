using System;

namespace Frontend.Commons.Commons
{
    public class ApplicationConstants
    {        
        public const string ApplicationName = "QR Almacenes";

        public const string ApplicationNameSecurity = "Movilidad Almacenes";
        
        //public const string BaseApiRestAzure = "https://ypfwebmauprod01.ypfasepub01.p.azurewebsites.net/";

        //uat ->  https://magtest.ypf.com/apip/uat/movilidad

        //test -> https://magtest.ypf.com/apip/tst/movilidad 

        //dev -> https://magdesa.ypf.com/apip/movilidad  

        // prod https://magui.ypf.com/apip/movilidad 


        public const string BaseApiRestAzure = "https://magtest.ypf.com/apip/uat/movilidad/";

        public const string BaseApiRest = "https://ws.ypf.com.ar/MobileUpstreamWebApi/";

        public static readonly DateTime DefaultDateSync = new DateTime(2000, 01, 01);

        public const int TimeoutTime = 3000;

        public const string Enviroment = "Producción";

        public const string EmailInfo = "DesarrolloMobile@ypf.com";

        public const string ApplicationSecretiOS = "ios=123456";

        public const string ApplicationSecretAndroid = "android=0847a8c5-b606-454b-9459-f065af1ccc5b";

        public const int MaxVariableSqLite = 900;

        public const int LogsExpirationDays = 7;

        public const int DelaySyncMinutes = -30;

        public const string DatabaseName = "YPF_MA.db3";

        public const string DatabaseKey = "passphrase";

        public const int DefaultTimerSeconds = 600;

        public const int DefaultTimerMinutes = 10;

        public const int DefaultElapsedTimeMinutes = 10;

    }
}

