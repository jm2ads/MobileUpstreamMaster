using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
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
    public partial class ListaPosicionesTraslado321View : ContentPage
    {
        private readonly IListaPosicionesTraslado321ViewModel listaPosicionesTraslado321ViewModel;

        public ListaPosicionesTraslado321View(IListaPosicionesTraslado321ViewModel listaPosicionesTraslado321ViewModel)
        {
            InitializeComponent();
            BindingContext = this.listaPosicionesTraslado321ViewModel = listaPosicionesTraslado321ViewModel;
            ListViewItems.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaPosicionesTraslado321ViewModel.FiltroPosicionCommand.Execute(e.NewTextValue);
        }

        protected override bool OnBackButtonPressed()
        {
            listaPosicionesTraslado321ViewModel.OnBackButtonPressedCommnad.Execute(null);
            return true;
        }
    }
}