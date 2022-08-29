using System;
using System.Linq;

namespace Frontend.Business.Movimientos.Ingresos.Core
{
    public class NotaDeEntregaFactory
    {
        private readonly DetalleNotaDeEntregaFactory detalleNotaDeEntregaFactory;

        public NotaDeEntregaFactory(DetalleNotaDeEntregaFactory detalleNotaDeEntregaFactory)
        {
            this.detalleNotaDeEntregaFactory = detalleNotaDeEntregaFactory;
        }

        public NotaDeEntrega Create(Pedido pedido)
        {
            return new NotaDeEntrega()
            {
                Pedido = pedido,
                PedidoId = pedido.Id,
                FechaDocumento = DateTime.Now,
                FechaContabilizacion = DateTime.Now,
                DetalleNotaDeEntrega = detalleNotaDeEntregaFactory.Create(pedido.DetallesPedido).ToList(),
                //usuario creacion
                SyncState = Synchronizer.SyncState.New
            };
        }
    }
}
