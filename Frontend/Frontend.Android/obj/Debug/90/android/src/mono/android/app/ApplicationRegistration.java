package mono.android.app;

public class ApplicationRegistration {

	public static void registerApplications ()
	{
				// Application and Instrumentation ACWs must be registered first.
		mono.android.Runtime.register ("Frontend.Droid.MainApplication, Frontend.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", crc64b744f3c1e2c1490e.MainApplication.class, crc64b744f3c1e2c1490e.MainApplication.__md_methods);
		mono.android.Runtime.register ("Frontend.Droid.Implementations.AndroidApplicationData, Frontend.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", crc64915676ffe8aca814.AndroidApplicationData.class, crc64915676ffe8aca814.AndroidApplicationData.__md_methods);
		mono.android.Runtime.register ("Frontend.Droid.Implementations.AndroidTimer, Frontend.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", crc64915676ffe8aca814.AndroidTimer.class, crc64915676ffe8aca814.AndroidTimer.__md_methods);
		
	}
}
