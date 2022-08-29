using Android.App;
using Android.Content;
using Frontend.Commons.Commons;
using Frontend.Droid.Implementations;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidApplicationData))]
namespace Frontend.Droid.Implementations
{
    public class AndroidApplicationData : Application, IApplicationInformation
    {
        public string GetBuildNumber()
        {
            return Context.PackageManager.GetPackageInfo(Context.PackageName, 0).VersionCode.ToString();
        }

        public string GetVersionNumber()
        {
            return Context.PackageManager.GetPackageInfo(Context.PackageName, 0).VersionName;
        }
    }
}