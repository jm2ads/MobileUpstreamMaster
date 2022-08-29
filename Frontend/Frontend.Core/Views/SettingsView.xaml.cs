using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.Core.Commons.Container;
using Frontend.Core.IViewModels;
using Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage
    {
        public SettingsView()
        {
            InitializeComponent();
            BindingContext = AppContainer.container.Resolve<ISettingsViewModel>();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}