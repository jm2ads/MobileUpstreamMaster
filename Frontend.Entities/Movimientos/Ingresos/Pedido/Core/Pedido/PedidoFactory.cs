using Frontend.Business.Centros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Business.Movimientos.Ingresos.Core
{
    public class PedidoFactory
    {
        public Pedido Create()
        {
            var pedido = new Pedido();
            pedido.NumeroPedido = DateTime.Now.ToString("ddMMyyhhmmssff");
            pedido.FechaModificacion = DateTime.Now;
            return pedido;
        }
    }
}
