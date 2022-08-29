using Frontend.Core.Areas.Log.IViewModels;
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
    public partial class InventariosLogView : ContentPage
    {
        private readonly IInventariosLogViewModel inventariosLogViewModel;

        public InventariosLogView(IInventariosLogViewModel inventariosLogViewModel)
        {
            InitializeComponent();
            BindingContext = this.inventariosLogViewModel = inventariosLogViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}