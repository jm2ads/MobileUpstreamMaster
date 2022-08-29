using System.Reflection;

using Frontend.Core.Areas.Inventarios.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalleStockView : ContentPage
	{
        private readonly IDetalleStockViewModel detalleStockViewModel;

        public DetalleStockView (IDetalleStockViewModel detalleStockViewModel)
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IInformacionInventarioViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.detalleStockViewModel = detalleStockViewModel;
        }
	}
}