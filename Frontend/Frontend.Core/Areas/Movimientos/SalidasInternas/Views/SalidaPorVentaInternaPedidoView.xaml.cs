using Frontend.Core.Areas.Movimientos.SalidasInternas;
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
	public partial class SalidaPorVentaInternaPedidoView : ContentPage
	{
        private readonly ISalidaPorVentaInternaPedidoViewModel salidaPorVentaInternaPedidoViewModel;

        public SalidaPorVentaInternaPedidoView(ISalidaPorVentaInternaPedidoViewModel salidaPorVentaInternaPedidoViewModel)
        {
            InitializeComponent();
            BindingContext = this.salidaPorVentaInternaPedidoViewModel = salidaPorVentaInternaPedidoViewModel;
        }
	}
}