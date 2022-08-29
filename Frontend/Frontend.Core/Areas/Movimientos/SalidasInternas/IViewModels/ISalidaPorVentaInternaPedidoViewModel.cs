using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Frontend.Core.Areas.Movimientos.SalidasInternas
{
    public interface ISalidaPorVentaInternaPedidoViewModel
    {
        ICommand GetAllSalidasCommand { get; set; }
    }
}
