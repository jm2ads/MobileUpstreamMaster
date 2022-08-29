using System.Reflection;

using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalleMaterialTraslado541View : ContentPage
	{
        private readonly IDetalleMaterialTraslado541ViewModel detalleMaterialTraslado541ViewModel;

        public DetalleMaterialTraslado541View (IDetalleMaterialTraslado541ViewModel detalleMaterialTraslado541ViewModel)
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IDetalleMaterialTraslado541ViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.detalleMaterialTraslado541ViewModel = detalleMaterialTraslado541ViewModel;
        }
	}
}