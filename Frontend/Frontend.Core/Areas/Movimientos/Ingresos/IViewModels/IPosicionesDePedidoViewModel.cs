using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Frontend.Core.Areas.Movimientos.Ingresos.IViewModels
{
    public interface IPosicionesDePedidoViewModel
    {
        ICommand FiltroPosicionCommand { get; set; }
        ICommand RefreshCommand { get; set; }
    }
}
