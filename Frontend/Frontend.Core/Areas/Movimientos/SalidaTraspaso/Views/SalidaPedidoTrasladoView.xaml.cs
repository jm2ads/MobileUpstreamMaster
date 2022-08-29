using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.Movimientos.SalidaTraspaso.IViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Areas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalidaPedidoTraspasoView : TabbedPage
    {
        private readonly ISalidaPedidoTraspasoViewModel salidaPedidoTraspasoViewModel;

        public SalidaPedidoTraspasoView(ISalidaPedidoTraspasoViewModel salidaPedidoTraspasoViewModel)
        {
            InitializeComponent();
            BindingContext = this.salidaPedidoTraspasoViewModel = salidaPedidoTraspasoViewModel;

            SalidaPorPedidoTraspasoPedidoView salidaTraspasoPedidoView = ContainerManager.Resolve(typeof(SalidaPorPedidoTraspasoPedidoView)) as SalidaPorPedidoTraspasoPedidoView;
            Children.Add(salidaTraspasoPedidoView);

            SalidaPedidoTraspasoMaterialView salidaTraspasoMaterialView = ContainerManager.Resolve(typeof(SalidaPedidoTraspasoMaterialView)) as SalidaPedidoTraspasoMaterialView;
            Children.Add(salidaTraspasoMaterialView);
        }
    }
}