using Frontend.Core.Areas.Inventarios.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AprobacionDetalleInventarioProvisorioView : ContentPage
	{
        private readonly IAprobacionDetalleInventarioProvisorioViewModel aprobacionDetalleInventarioProvisorioViewModel;

        public AprobacionDetalleInventarioProvisorioView (IAprobacionDetalleInventarioProvisorioViewModel aprobacionDetalleInventarioProvisorioViewModel)
		{
			InitializeComponent ();
            BindingContext = this.aprobacionDetalleInventarioProvisorioViewModel = aprobacionDetalleInventarioProvisorioViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            aprobacionDetalleInventarioProvisorioViewModel.FiltroPosicionCommand.Execute(e.NewTextValue);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            aprobacionDetalleInventarioProvisorioViewModel.GetInventarioProvisorioCommand.Execute(null);
        }
    }
}