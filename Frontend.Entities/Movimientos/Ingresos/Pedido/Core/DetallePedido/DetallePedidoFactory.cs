using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Business.Movimientos.Ingresos.Core
{
    public class DetallePedidoFactory
    {
        public DetallePedido Create()
        {
            return new DetallePedido();
        }
    }
}
