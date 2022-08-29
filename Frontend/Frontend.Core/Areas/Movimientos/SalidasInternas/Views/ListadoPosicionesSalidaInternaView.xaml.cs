
using Frontend.Core.Areas.Movimientos.SalidasInternas.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListadoPosicionesSalidaInternaView : ContentPage
    {
        private readonly IListadoPosicionesSalidaInternaViewModel listadoPosicionesSalidaInternaViewModel;

        public ListadoPosicionesSalidaInternaView(IListadoPosicionesSalidaInternaViewModel listadoPosicionesSalidaInternaViewModel)
        {
            InitializeComponent();
            BindingContext = this.listadoPosicionesSalidaInternaViewModel = listadoPosicionesSalidaInternaViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listadoPosicionesSalidaInternaViewModel.FiltroPosicionCommand.Execute(e.NewTextValue);
        }
    }
}