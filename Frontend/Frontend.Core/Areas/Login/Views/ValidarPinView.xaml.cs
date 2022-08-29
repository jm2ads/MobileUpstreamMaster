
using Frontend.Core.Areas.Login.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ValidarPinView : ContentPage
	{
        private readonly IValidarPinViewModel validarPinViewModel;

        public ValidarPinView (IValidarPinViewModel validarPinViewModel)
		{
			InitializeComponent ();
            BindingContext = this.validarPinViewModel = validarPinViewModel;
        }
	}
}