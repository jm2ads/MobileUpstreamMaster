
using Frontend.Core.Areas.InventariosMasivos.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListadoInventarioMasivoView : ContentPage
    {
        private readonly IListadoInventarioMasivoViewModel listadoInventarioMasivoViewModel;

        public ListadoInventarioMasivoView(IListadoInventarioMasivoViewModel listadoInventarioMasivoViewModel)
        {
            InitializeComponent();
            BindingContext = this.listadoInventarioMasivoViewModel = listadoInventarioMasivoViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listadoInventarioMasivoViewModel.RefreshListCommnad.Execute(null);
        }
    }
}