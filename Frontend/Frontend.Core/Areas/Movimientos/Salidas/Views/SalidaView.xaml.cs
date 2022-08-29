
using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.Movimientos.Salidas.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SalidaView : TabbedPage
    {
        private readonly ISalidaViewModel salidaViewModel;

        public SalidaView (ISalidaViewModel salidaViewModel)
		{
			InitializeComponent ();
            BindingContext = this.salidaViewModel = salidaViewModel;

            SalidaReservaView salidaReservaView = ContainerManager.Resolve(typeof(SalidaReservaView)) as SalidaReservaView;
            Children.Add(salidaReservaView);

            SalidaMaterialView salidaMaterialView = ContainerManager.Resolve(typeof(SalidaMaterialView)) as SalidaMaterialView;
            Children.Add(salidaMaterialView);
        }
    }
}