using Frontend.Core.Areas.Inventarios.IViewModels;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VisualizarDetalleInventarioSapView : ContentPage
	{
        private readonly IVisualizarDetalleInventarioSapViewModel visualizarDetalleInventarioSapViewModel;

        public VisualizarDetalleInventarioSapView (IVisualizarDetalleInventarioSapViewModel visualizarDetalleInventarioSapViewModel)
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IInformacionInventarioViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.visualizarDetalleInventarioSapViewModel = visualizarDetalleInventarioSapViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}