using DLToolkit.Forms.Controls;
using Frontend.Commons.Bootstrapper;
using Frontend.Commons.Commons;
using Frontend.Commons.Commons.Timer;
using Frontend.Core.Commons.CustomRenders;
using Frontend.Core.IViewModels;
using Frontend.Core.Views;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        private readonly IAppViewModel vm;
        private readonly Stopwatch stopWatch;

        public App(IBootstraperStartup bootstraperStartup)
        {
            InitializeComponent();
            FlowListView.Init();
            bootstraperStartup.ConfigureContainer();
            BindingContext = vm = ContainerManager.Resolve<IAppViewModel>();
            stopWatch = IdleTimer.GetInstance();
        }

        protected override void OnStart()
        {
            Microsoft.AppCenter.AppCenter.Start(ApplicationConstants.ApplicationSecretAndroid, typeof(Analytics), typeof(Crashes));
            
            if (!stopWatch.IsRunning)
            {
                stopWatch.Start();
            }

            Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                if (stopWatch.IsRunning && stopWatch.Elapsed.Minutes >= ApplicationConstants.DefaultElapsedTimeMinutes)
                {
                    Device.InvokeOnMainThreadAsync(() =>
                    {
                        if (Current.MainPage.GetType() != typeof(CustomNavigationPage)
                            || (Current.MainPage.GetType() == typeof(CustomNavigationPage)
                            && ((CustomNavigationPage)Current.MainPage).CurrentPage.GetType() != typeof(IngresoUsuarioView)))
                        {
                            Current.MainPage.DisplayAlert("Aviso", "Se ha cerrado la sesión por inactividad.", "Aceptar");
                            Current.MainPage = new CustomNavigationPage(ContainerManager.Resolve<IngresoUsuarioView>());
                        }
                    });
                    stopWatch.Restart();
                }
                return true;
            });

            base.OnStart();
        }

        protected override void OnResume()
        {
            base.OnResume();
            stopWatch.Start();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            stopWatch.Reset();
        }


    }
}