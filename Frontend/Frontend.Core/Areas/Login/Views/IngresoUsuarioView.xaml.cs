
using Frontend.Core.Areas.Login.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IngresoUsuarioView : ContentPage
	{
        private readonly IIngresoUsuarioViewModel ingresoUsuarioViewModel;

        public IngresoUsuarioView (IIngresoUsuarioViewModel ingresoUsuarioViewModel)
		{
			InitializeComponent ();
            BindingContext = this.ingresoUsuarioViewModel = ingresoUsuarioViewModel;
            NavigationPage.SetHasNavigationBar(this, false);
        }
	}
}