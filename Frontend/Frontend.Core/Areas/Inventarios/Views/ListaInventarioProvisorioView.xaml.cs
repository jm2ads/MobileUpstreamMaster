using Frontend.Core.Areas.Inventarios.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaInventarioProvisorioView : ContentPage
    {
        private readonly IListaInventarioProvisorioViewModel listaInventarioProvisorioViewModel;

        public ListaInventarioProvisorioView(IListaInventarioProvisorioViewModel listaInventarioProvisorioViewModel)
        {
            InitializeComponent();
            BindingContext = this.listaInventarioProvisorioViewModel = listaInventarioProvisorioViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}