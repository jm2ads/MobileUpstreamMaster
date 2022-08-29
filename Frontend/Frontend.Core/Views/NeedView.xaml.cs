using System;
using Frontend.Core.Commons.Container;
using Frontend.Core.IViewModels;
using Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NeedView : ContentPage
    {
        public NeedView()
        {
            InitializeComponent();
            BindingContext = AppContainer.container.Resolve<INeedViewModel>();
        }
    }
}