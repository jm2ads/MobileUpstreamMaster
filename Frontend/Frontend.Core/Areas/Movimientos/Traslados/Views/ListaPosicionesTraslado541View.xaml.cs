
using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaPosicionesTraslado541View : ContentPage
	{
        private readonly IListaPosicionesTraslado541ViewModel listaPosicionesTraslado541ViewModel;

        public ListaPosicionesTraslado541View (IListaPosicionesTraslado541ViewModel listaPosicionesTraslado541ViewModel)
		{
			InitializeComponent ();
            BindingContext = this.listaPosicionesTraslado541ViewModel = listaPosicionesTraslado541ViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaPosicionesTraslado541ViewModel.FiltroPosicionCommand.Execute(e.NewTextValue);
        }

        protected override bool OnBackButtonPressed()
        {
            listaPosicionesTraslado541ViewModel.OnBackButtonPressedCommnad.Execute(null);
            return true;
        }
    }
}