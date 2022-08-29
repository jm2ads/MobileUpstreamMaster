using System.Reflection;

using Frontend.Core.Areas.Movimientos.Devoluciones.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalleMaterialDevolucionView : ContentPage
	{
        private readonly IDetalleMaterialDevolucionViewModel detalleMaterialDevolucionViewModel;

        public DetalleMaterialDevolucionView (IDetalleMaterialDevolucionViewModel detalleMaterialDevolucionViewModel)
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IDetalleMaterialDevolucionViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.detalleMaterialDevolucionViewModel = detalleMaterialDevolucionViewModel;
        }
	}
}