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
    public partial class MovimientosLogView : ContentPage
    {
        private readonly IMovimientosLogViewModel movimientosLogViewModel;

        public MovimientosLogView(IMovimientosLogViewModel movimientosLogViewModel)
        {
            InitializeComponent();
            BindingContext = this.movimientosLogViewModel = movimientosLogViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}