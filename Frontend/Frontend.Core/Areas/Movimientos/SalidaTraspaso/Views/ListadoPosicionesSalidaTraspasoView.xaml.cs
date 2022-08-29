using Frontend.Core.Areas.Movimientos.SalidaTraspaso.IViewModels;
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
	public partial class ListadoPosicionesSalidaTraspasoView : ContentPage
	{
        private readonly IListadoPosicionesSalidaTraspasoViewModel listadoPosicionesSalidaTraspasoViewModel;

        public ListadoPosicionesSalidaTraspasoView(IListadoPosicionesSalidaTraspasoViewModel listadoPosicionesSalidaTraspasoViewModel)
        {
            InitializeComponent();
            BindingContext = this.listadoPosicionesSalidaTraspasoViewModel = listadoPosicionesSalidaTraspasoViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listadoPosicionesSalidaTraspasoViewModel.FiltroPosicionCommand.Execute(e.NewTextValue);
        }
    }
}