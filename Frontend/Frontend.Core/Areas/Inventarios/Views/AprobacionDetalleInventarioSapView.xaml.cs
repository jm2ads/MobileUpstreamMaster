using Frontend.Core.Areas.Inventarios.IViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AprobacionDetalleInventarioSapView : ContentPage
	{
        private readonly IAprobacionDetalleInventarioSapViewModel aprobacionDetaleInventarioSapViewModel;

        public AprobacionDetalleInventarioSapView (IAprobacionDetalleInventarioSapViewModel aprobacionDetaleInventarioSapViewModel)
		{
			InitializeComponent ();
            BindingContext = this.aprobacionDetaleInventarioSapViewModel = aprobacionDetaleInventarioSapViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            aprobacionDetaleInventarioSapViewModel.GetInventarioSapCommand.Execute(null);
        }
    }
}