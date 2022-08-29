
using Frontend.Core.Areas.Inventarios.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaInventarioRechazadoView : ContentPage
	{
        private readonly IListaInventarioRechazadoViewModel listaInventarioRechazadoViewModel;

        public ListaInventarioRechazadoView (IListaInventarioRechazadoViewModel listaInventarioRechazadoViewModel)
		{
			InitializeComponent ();
            BindingContext = this.listaInventarioRechazadoViewModel = listaInventarioRechazadoViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}