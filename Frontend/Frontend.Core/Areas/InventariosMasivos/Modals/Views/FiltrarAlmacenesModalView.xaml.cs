using Frontend.Core.Areas.InventariosMasivos.IViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FiltrarAlmacenesModalView : ContentPage
    {
        private readonly IFiltrarAlmacenesViewModel filtrarAlmacenesModalViewModel;

        public FiltrarAlmacenesModalView(IFiltrarAlmacenesViewModel filtrarAlmacenesModalViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IFiltrarAlmacenesViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.filtrarAlmacenesModalViewModel = filtrarAlmacenesModalViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            filtrarAlmacenesModalViewModel.FiltroAlmacenCommand.Execute(e.NewTextValue);
        }
    }
}