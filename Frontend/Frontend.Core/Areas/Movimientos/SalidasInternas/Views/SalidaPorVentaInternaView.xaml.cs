using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.Movimientos.SalidasInternas.IViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Areas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalidaPorVentaInternaView : TabbedPage
    {
        private readonly ISalidaPorVentaInternaViewModel salidaPorVentaInternaViewModel;

        public SalidaPorVentaInternaView(ISalidaPorVentaInternaViewModel salidaPorVentaInternaViewModel)
        {
            InitializeComponent();
            BindingContext = this.salidaPorVentaInternaViewModel = salidaPorVentaInternaViewModel;

            SalidaPorVentaInternaPedidoView salidaPorVentaInternaPedidoView = ContainerManager.Resolve(typeof(SalidaPorVentaInternaPedidoView)) as SalidaPorVentaInternaPedidoView;
            Children.Add(salidaPorVentaInternaPedidoView);

            SalidaPorVentaInternaMaterialView salidaPorVentaInternaMaterialView = ContainerManager.Resolve(typeof(SalidaPorVentaInternaMaterialView)) as SalidaPorVentaInternaMaterialView;
            Children.Add(salidaPorVentaInternaMaterialView);
        }
    }
}