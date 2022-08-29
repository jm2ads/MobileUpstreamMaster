
using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaPosicionesTraslado311View : ContentPage
	{
        private readonly IListaPosicionesTraslado311ViewModel listaPosicionesTraslado311ViewModel;

        public ListaPosicionesTraslado311View (IListaPosicionesTraslado311ViewModel listaPosicionesTraslado311ViewModel)
		{
			InitializeComponent ();
            BindingContext = this.listaPosicionesTraslado311ViewModel = listaPosicionesTraslado311ViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaPosicionesTraslado311ViewModel.FiltroPosicionCommand.Execute(e.NewTextValue);
        }

        protected override bool OnBackButtonPressed()
        {
            listaPosicionesTraslado311ViewModel.OnBackButtonPressedCommnad.Execute(null);
            return true;
        }
    }
}