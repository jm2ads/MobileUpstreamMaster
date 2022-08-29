
using Frontend.Core.Areas.Inventarios.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResultadoConsultaStockView : ContentPage
	{
        private readonly IResultadoConsultaStockViewModel resultadoConsultaStockViewModel;

        public ResultadoConsultaStockView (IResultadoConsultaStockViewModel resultadoConsultaStockViewModel)
		{
			InitializeComponent ();
            BindingContext = this.resultadoConsultaStockViewModel = resultadoConsultaStockViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}