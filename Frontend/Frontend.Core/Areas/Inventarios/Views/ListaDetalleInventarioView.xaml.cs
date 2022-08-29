using Frontend.Core.IViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaDetalleInventarioView : ContentPage
	{
        private readonly IListaDetalleInventarioViewModel listaDetalleInventarioViewModel;

        public ListaDetalleInventarioView (IListaDetalleInventarioViewModel listaDetalleInventarioViewModel)
		{
			InitializeComponent ();
            BindingContext = this.listaDetalleInventarioViewModel = listaDetalleInventarioViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}