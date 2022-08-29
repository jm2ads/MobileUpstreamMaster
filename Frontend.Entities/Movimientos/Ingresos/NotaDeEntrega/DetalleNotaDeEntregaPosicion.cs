using Frontend.Business.Synchronizer;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Frontend.Business.Movimientos.Ingresos
{
    public class DetalleNotaDeEntregaPosicion : SyncLocalEntity
    {
        public double CantidadRecibida { get; set; }
        public string ClaseDeMovimientoCodigo { get; set; }
        public int Ejercicio { get; set; }
        public bool EsContado { get; set; }
        public string DocumentoReferencia { get; set; }
        public int PosicionDocumento { get; set; }

        [ForeignKey(typeof(DetalleNotaDeEntrega))]
        public int DetalleNotaDeEntregaId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public DetalleNotaDeEntrega DetalleNotaDeEntrega { get; set; }

        [ForeignKey(typeof(DetallePedidoPosicion))]
        public int DetallePedidoPosicionId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public DetallePedidoPosicion DetallePedidoPosicion { get; set; }

        [Ignore]
        public string DisplayCantidad => CantidadRecibida + " / " + DetallePedidoPosicion.CantidadPendiente + " " + DetalleNotaDeEntrega.DetallePedido.Unidad;
    }
}
