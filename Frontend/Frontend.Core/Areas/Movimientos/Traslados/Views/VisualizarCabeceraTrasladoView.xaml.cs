using System.Reflection;

using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VisualizarCabeceraTrasladoView : ContentPage
	{
        private readonly IVisualizarCabeceraTrasladoViewModel visualizarCabeceraTrasladoViewModel;

        public VisualizarCabeceraTrasladoView (IVisualizarCabeceraTrasladoViewModel visualizarCabeceraTrasladoViewModel)
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IVisualizarCabeceraTrasladoViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.visualizarCabeceraTrasladoViewModel = visualizarCabeceraTrasladoViewModel;
        }
	}
}