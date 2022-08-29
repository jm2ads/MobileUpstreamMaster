using System.Reflection;

using Frontend.Core.Areas.Inventarios.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConsultaStockView : ContentPage
	{
        private readonly IConsultaStockViewModel consultaStockViewModel;

        public ConsultaStockView (IConsultaStockViewModel consultaStockViewModel)
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IConsultaStockViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.consultaStockViewModel = consultaStockViewModel;
        }
	}
}