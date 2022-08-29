using Frontend.Core.Areas.Movimientos.SalidaTraspaso.IViewModels;
using Frontend.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Core.Areas.Movimientos.SalidaTraspaso.ViewModels
{
    public class SalidaPedidoTraspasoViewModel : BaseViewModel, ISalidaPedidoTraspasoViewModel
    {
        public SalidaPedidoTraspasoViewModel()
        {
            Title = "Salida por pedido de traspaso";
        }
    }
}
