using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.Inventarios.IViewModels;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VisualizarInformacionInventarioView : ContentPage
	{
        private readonly IVisualizarInformacionInventarioViewModel visualizarInformacionInventarioViewModel;

        public VisualizarInformacionInventarioView ()
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IVisualizarInformacionInventarioViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.visualizarInformacionInventarioViewModel = ContainerManager.Resolve<IVisualizarInformacionInventarioViewModel>();
        }
	}
}