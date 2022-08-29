using Frontend.Business.Synchronizer;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Frontend.Business.Movimientos.Ingresos
{
    public class Pedido : SyncEntity
    {
        public string NumeroPedido { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaEntrega{ get; set; }
        public EstadoMovimiento Estado { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<DetallePedido> DetallesPedido { get; set; }
    }
}
