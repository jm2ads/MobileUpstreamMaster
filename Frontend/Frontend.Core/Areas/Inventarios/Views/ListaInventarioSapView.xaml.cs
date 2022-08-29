using Frontend.Core.Areas.Inventarios.IViewModels;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaInventarioSapView : ContentPage
    {
        private readonly IListaInventarioSapViewModel listaInventarioSapViewModel;

        public ListaInventarioSapView(IListaInventarioSapViewModel listaInventarioSapViewModel)
        {
            InitializeComponent();
            BindingContext = this.listaInventarioSapViewModel = listaInventarioSapViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}