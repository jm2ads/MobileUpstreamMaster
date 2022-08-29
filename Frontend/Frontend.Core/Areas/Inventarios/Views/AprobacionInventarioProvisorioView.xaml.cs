using Frontend.Core.Areas.Inventarios.IViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AprobacionInventarioProvisorioView : ContentPage
    {
        private readonly IAprobacionInventarioProvisorioViewModel aprobacionInventarioProvisorioViewModel;

        public AprobacionInventarioProvisorioView(IAprobacionInventarioProvisorioViewModel aprobacionInventarioProvisorioViewModel)
        {
            InitializeComponent();
            BindingContext = this.aprobacionInventarioProvisorioViewModel = aprobacionInventarioProvisorioViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}