using System;
using Frontend.Core.Areas.AboutUs.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Frontend.Commons.Bootstrapper;
using Xamarin.Forms.StyleSheets;
using System.Reflection;

namespace Frontend.Core.Areas.AboutUs.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutUsView : ContentPage
	{
        private readonly IAboutUsViewModel vm;

        public AboutUsView ()
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(this.GetType()).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = vm = ContainerManager.Resolve<IAboutUsViewModel>();
        }

        public void OnLabelTapped(object sender, EventArgs args)
        {
            vm.OpenEmailAppCommand.Execute(null);
        }
    }
}