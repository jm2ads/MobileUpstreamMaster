using Frontend.Core.Areas.InventariosAprobacionMasiva.IViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListadoDeMaterialesAprobacionView : ContentPage
    {
        private readonly IListadoDeMaterialesAprobacionViewModel listadoDeMaterialesAprobacionViewModel;

        public ListadoDeMaterialesAprobacionView(IListadoDeMaterialesAprobacionViewModel listadoDeMaterialesAprobacionViewModel)
        {
            InitializeComponent();
            BindingContext = this.listadoDeMaterialesAprobacionViewModel = listadoDeMaterialesAprobacionViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listadoDeMaterialesAprobacionViewModel.FiltroMaterialCommand.Execute(e.NewTextValue);
        }
    }
}