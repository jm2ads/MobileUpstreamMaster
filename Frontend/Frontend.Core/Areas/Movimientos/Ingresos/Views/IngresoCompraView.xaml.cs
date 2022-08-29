using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.Movimientos.Ingresos.IViewModels;
using Frontend.Core.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Areas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngresoCompraView : TabbedPage
    {
        private readonly IIngresoCompraViewModel ingresoPorPedidoViewModel;

        public IngresoCompraView(IIngresoCompraViewModel ingresoPorPedidoViewModel)
        {
            InitializeComponent();
            BindingContext = this.ingresoPorPedidoViewModel = ingresoPorPedidoViewModel;

            IngresoCompraPorPedidoView ingresoCompraPorPedidoView = ContainerManager.Resolve(typeof(IngresoCompraPorPedidoView)) as IngresoCompraPorPedidoView;
            Children.Add(ingresoCompraPorPedidoView);

            IngresoCompraPorMaterialView ingresoCompraPorMaterialView = ContainerManager.Resolve(typeof(IngresoCompraPorMaterialView)) as IngresoCompraPorMaterialView;
            Children.Add(ingresoCompraPorMaterialView);
        }
    }
}
