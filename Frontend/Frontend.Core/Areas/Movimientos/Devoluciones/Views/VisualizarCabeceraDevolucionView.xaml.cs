using System.Reflection;

using Frontend.Core.Areas.Movimientos.Devoluciones.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VisualizarCabeceraDevolucionView : ContentPage
	{
        private readonly IVisualizarCabeceraDevolucionViewModel visualizarCabeceraDevolucionViewModel;

        public VisualizarCabeceraDevolucionView (IVisualizarCabeceraDevolucionViewModel visualizarCabeceraDevolucionViewModel)
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IVisualizarCabeceraDevolucionViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.visualizarCabeceraDevolucionViewModel = visualizarCabeceraDevolucionViewModel;
        }
	}
}