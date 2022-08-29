using System.Reflection;

using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.Movimientos.Devoluciones.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DevolucionView : TabbedPage
	{
        private readonly IDevolucionViewModel devolucionViewModel;

        public DevolucionView (IDevolucionViewModel devolucionViewModel)
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IVisualizarCabeceraDevolucionViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.devolucionViewModel = devolucionViewModel;

            DevolucionReservaView devolucionReservaView = ContainerManager.Resolve(typeof(DevolucionReservaView)) as DevolucionReservaView;
            Children.Add(devolucionReservaView);

            DevolucionMaterialView devolucionMaterialView = ContainerManager.Resolve(typeof(DevolucionMaterialView)) as DevolucionMaterialView;
            Children.Add(devolucionMaterialView);            
        }
	}
}