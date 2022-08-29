
using Frontend.Core.Areas.Login.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CrearPinView : ContentPage
	{
        private readonly ICrearPinViewModel crearPinViewModel;

        public CrearPinView (ICrearPinViewModel crearPinViewModel)
		{
			InitializeComponent ();
            BindingContext = this.crearPinViewModel = crearPinViewModel;
        }
	}
}