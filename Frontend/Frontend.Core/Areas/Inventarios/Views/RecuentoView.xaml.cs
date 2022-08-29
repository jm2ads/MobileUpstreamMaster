using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.Inventarios.IViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecuentoView : TabbedPage
	{
        private readonly IRecuentoViewModel recuentoViewModel;

        public RecuentoView (IRecuentoViewModel recuentoViewModel)
		{
			InitializeComponent ();

            BindingContext = this.recuentoViewModel = recuentoViewModel;

            RecuentoMaterialView recuentoMaterialView = ContainerManager.Resolve(typeof(RecuentoMaterialView)) as RecuentoMaterialView;
            Children.Add(recuentoMaterialView);

            RecuentoInventarioView recuentoInventarioView = ContainerManager.Resolve(typeof(RecuentoInventarioView)) as RecuentoInventarioView;
            Children.Add(recuentoInventarioView);
        }
	}
}