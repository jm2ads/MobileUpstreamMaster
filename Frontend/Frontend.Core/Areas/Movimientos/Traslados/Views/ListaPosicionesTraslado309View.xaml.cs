
using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaPosicionesTraslado309View : ContentPage
    {
        private readonly IListaPosicionesTraslado309ViewModel listaPosicionesTraslado309ViewModel;

        public ListaPosicionesTraslado309View(IListaPosicionesTraslado309ViewModel listaPosicionesTraslado309ViewModel)
        {
            InitializeComponent();
            BindingContext = this.listaPosicionesTraslado309ViewModel = listaPosicionesTraslado309ViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }


        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaPosicionesTraslado309ViewModel.FiltroPosicionCommand.Execute(e.NewTextValue);
        }

        protected override bool OnBackButtonPressed()
        {
            listaPosicionesTraslado309ViewModel.OnBackButtonPressedCommnad.Execute(null);
            return true;
        }
    }
}