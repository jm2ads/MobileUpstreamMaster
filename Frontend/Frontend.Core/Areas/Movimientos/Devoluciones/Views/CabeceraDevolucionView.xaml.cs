using Frontend.Core.Areas.Movimientos.Devoluciones.IViewModels;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CabeceraDevolucionView : ContentPage
	{
        private readonly ICabeceraDevolucionViewModel cabeceraDevolucionViewModel;

        public CabeceraDevolucionView (ICabeceraDevolucionViewModel cabeceraDevolucionViewModel)
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(ICabeceraDevolucionViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.cabeceraDevolucionViewModel = cabeceraDevolucionViewModel;
        }
	}
}