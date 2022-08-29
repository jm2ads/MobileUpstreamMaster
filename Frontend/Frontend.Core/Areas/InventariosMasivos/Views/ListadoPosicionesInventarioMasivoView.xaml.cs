
using Frontend.Core.Areas.InventariosMasivos.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListadoPosicionesInventarioMasivoView : ContentPage
    {
        private readonly IListadoPosicionesInventarioMasivoViewModel listadoPocisionesInventarioMasivoViewModel;

        public ListadoPosicionesInventarioMasivoView(IListadoPosicionesInventarioMasivoViewModel listadoPocisionesInventarioMasivoViewModel)
        {
            InitializeComponent();
            BindingContext = this.listadoPocisionesInventarioMasivoViewModel = listadoPocisionesInventarioMasivoViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listadoPocisionesInventarioMasivoViewModel.FiltroPosicionCommand.Execute(e.NewTextValue);
        }
        protected override bool OnBackButtonPressed()
        {
            listadoPocisionesInventarioMasivoViewModel.OnBackButtonPressedCommnad.Execute(null);
            return true;
        }
    }
}