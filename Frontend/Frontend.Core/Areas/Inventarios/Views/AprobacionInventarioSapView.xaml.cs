using Frontend.Core.Areas.Inventarios.IViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AprobacionInventarioSapView : ContentPage
	{
        private readonly IAprobacionInventarioSapViewModel aprobacionInventarioSapViewModel;

        public AprobacionInventarioSapView (IAprobacionInventarioSapViewModel aprobacionInventarioSapViewModel)
		{
			InitializeComponent ();
            BindingContext = this.aprobacionInventarioSapViewModel = aprobacionInventarioSapViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}