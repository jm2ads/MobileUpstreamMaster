
using Frontend.Core.Areas.Movimientos.Devoluciones.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleDevolucionView : ContentPage
    {
        private readonly IDetalleDevolucionViewModel detalleDevolucionViewModel;

        public DetalleDevolucionView(IDetalleDevolucionViewModel detalleDevolucionViewModel)
        {
            InitializeComponent();
            BindingContext = this.detalleDevolucionViewModel = detalleDevolucionViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            detalleDevolucionViewModel.FiltroPosicionCommand.Execute(e.NewTextValue);
        }
    }
}