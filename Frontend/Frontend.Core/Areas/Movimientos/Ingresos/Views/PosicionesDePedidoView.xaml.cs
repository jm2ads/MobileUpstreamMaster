using Frontend.Core.Areas.Movimientos.Ingresos.IViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Areas.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PosicionesDePedidoView : ContentPage
	{
        private readonly IPosicionesDePedidoViewModel posicionesDePedidoViewModel;

        public PosicionesDePedidoView(IPosicionesDePedidoViewModel posicionesDePedidoViewModel)
        {
            InitializeComponent();
            BindingContext = this.posicionesDePedidoViewModel = posicionesDePedidoViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            posicionesDePedidoViewModel.FiltroPosicionCommand.Execute(e.NewTextValue);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            posicionesDePedidoViewModel.RefreshCommand.Execute(null);
        }
    }
}