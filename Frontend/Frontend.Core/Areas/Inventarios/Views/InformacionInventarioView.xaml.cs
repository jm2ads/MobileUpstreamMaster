using System.Reflection;

using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InformacionInventarioView : ContentPage
	{
        private readonly IInformacionInventarioViewModel informacionInventarioViewModel;

        public InformacionInventarioView (IInformacionInventarioViewModel informacionInventarioViewModel)
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IInformacionInventarioViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.informacionInventarioViewModel = informacionInventarioViewModel;
        }
	}
}