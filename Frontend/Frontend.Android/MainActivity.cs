using Android.App;
using Android.Content.PM;
using Android.OS;
using Frontend.Bootstrapper;
using Frontend.Commons.Commons.Timer;
using Frontend.Core;
using Microsoft.WindowsAzure.MobileServices;
using System;
using Xamarin.Forms;

namespace Frontend.Droid
{
    [Activity(Label = "Frontend", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;

            // Initialize Azure Mobile Apps
            CurrentPlatform.Init();

            base.OnCreate(bundle);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            FormsMaterial.Init(this, bundle);

            var bootstraperStartup = new Startup();
            LoadApplication(new App(bootstraperStartup));
        }

        protected void HandleUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Console.WriteLine(args.ExceptionObject.ToString());
        }

        public override void OnUserInteraction()
        {
            base.OnUserInteraction();
            IdleTimer.GetInstance().Restart();
        }

    }
}

