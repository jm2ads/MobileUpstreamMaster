using Frontend.Core.Areas.CambiosUbicacion.Modals.IViewModels;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeleccionarAlmacenesView : ContentPage
    {
        private readonly ISeleccionarAlmacenesViewModel seleccionarAlmacenesViewModel;

        public SeleccionarAlmacenesView(ISeleccionarAlmacenesViewModel seleccionarAlmacenesViewModel)
        {
            InitializeComponent(); 
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(ISeleccionarAlmacenesViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));

            BindingContext = this.seleccionarAlmacenesViewModel = seleccionarAlmacenesViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            seleccionarAlmacenesViewModel.FiltroAlmacenCommand.Execute(e.NewTextValue);
        }
    }
}