
using Frontend.Core.Areas.Inventarios.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecuentoDetalleInventarioView : ContentPage
	{
        private readonly IRecuentoDetalleInventarioViewModel recuentoDetalleInventarioViewModel;

        public RecuentoDetalleInventarioView (IRecuentoDetalleInventarioViewModel recuentoDetalleInventarioViewModel)
		{
			InitializeComponent ();
            BindingContext = this.recuentoDetalleInventarioViewModel = recuentoDetalleInventarioViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            recuentoDetalleInventarioViewModel.FiltroPosicionCommand.Execute(e.NewTextValue);
        }
    }
}