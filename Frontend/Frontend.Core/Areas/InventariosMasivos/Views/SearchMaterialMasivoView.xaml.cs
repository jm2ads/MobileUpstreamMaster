using Frontend.Core.Areas.InventariosMasivos.IViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchMaterialMasivoView : ContentPage
	{
        public ISearchMaterialMasivoViewModel searchMaterialMasivoViewModel;
        public SearchMaterialMasivoView (ISearchMaterialMasivoViewModel searchMaterialMasivoViewModel)
		{
			InitializeComponent ();
            BindingContext = this.searchMaterialMasivoViewModel = searchMaterialMasivoViewModel;
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            autocompleteCodigo.Focus();
        }
    }
}