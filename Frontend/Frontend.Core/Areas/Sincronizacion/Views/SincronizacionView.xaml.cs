using Frontend.Commons.Bootstrapper;
using Frontend.Commons.Commons;
using Frontend.Core.Areas.Sincronizacion.IViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Areas.Sincronizacion.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SincronizacionView : ContentPage
	{
        ISincronizacionViewModel vm;

        public SincronizacionView ()
		{
			InitializeComponent ();
            Title = ApplicationConstants.ApplicationName;

            BindingContext = vm = ContainerManager.Resolve<ISincronizacionViewModel>();

            btnSincronizar.Clicked += (sender, e) =>
            {
                vm.SyncCommand.Execute(null);
            };

            btnSincronizacionParcial.Clicked += (sender, e) =>
            {
                vm.SyncParcialCommand.Execute(null);
            };
        }
	}
}