using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaPosicionesTraslado542View : ContentPage
    {
        private readonly IListaPosicionesTraslado542ViewModel listaPosicionesTrasladoViewModel;

        public ListaPosicionesTraslado542View(IListaPosicionesTraslado542ViewModel listaPosicionesTrasladoViewModel)
        {
            InitializeComponent();
            BindingContext = this.listaPosicionesTrasladoViewModel = listaPosicionesTrasladoViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaPosicionesTrasladoViewModel.FiltroPosicionCommand.Execute(e.NewTextValue);
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
        protected override bool OnBackButtonPressed()
        {
            listaPosicionesTrasladoViewModel.OnBackButtonPressedCommnad.Execute(null);
            return true;
        }
    }
}