
using Frontend.Core.Areas.Movimientos.Devoluciones.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DevolucionReservaView : ContentPage
	{
        private readonly IDevolucionReservaViewModel devolucionReservaViewModel;

        public DevolucionReservaView (IDevolucionReservaViewModel devolucionReservaViewModel)
		{
			InitializeComponent ();
            BindingContext = this.devolucionReservaViewModel = devolucionReservaViewModel;
        }
	}
}