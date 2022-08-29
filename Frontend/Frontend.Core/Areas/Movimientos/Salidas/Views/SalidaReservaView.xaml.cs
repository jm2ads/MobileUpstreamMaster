using System.Reflection;

using Frontend.Core.Areas.Movimientos.Salidas.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SalidaReservaView : ContentPage
	{
        private readonly ISalidaReservaViewModel salidaReservaViewModel;

        public SalidaReservaView (ISalidaReservaViewModel salidaReservaViewModel)
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(ISalidaReservaViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.salidaReservaViewModel = salidaReservaViewModel;
        }
	}
}