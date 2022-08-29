using System.Reflection;

using Frontend.Core.Areas.Movimientos.Salidas.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalleMaterialSalidaView : ContentPage
	{
        private readonly IDetalleMaterialSalidaViewModel detalleMaterialSalidaViewModel;

        public DetalleMaterialSalidaView (IDetalleMaterialSalidaViewModel detalleMaterialSalidaViewModel)
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IDetalleMaterialSalidaViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.detalleMaterialSalidaViewModel = detalleMaterialSalidaViewModel;
        }
	}
}