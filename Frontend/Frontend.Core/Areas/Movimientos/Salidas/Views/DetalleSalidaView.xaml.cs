
using Frontend.Core.Areas.Movimientos.Salidas.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalleSalidaView : ContentPage
	{
        private readonly IDetalleSalidaViewModel detalleSalidaViewModel;

        public DetalleSalidaView (IDetalleSalidaViewModel detalleSalidaViewModel)
		{
			InitializeComponent ();
            BindingContext = this.detalleSalidaViewModel = detalleSalidaViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            detalleSalidaViewModel.FiltroPosicionCommand.Execute(e.NewTextValue);
        }
    }
}