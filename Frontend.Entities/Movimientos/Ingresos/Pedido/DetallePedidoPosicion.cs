using Frontend.Business.Synchronizer;
using SQLiteNetExtensions.Attributes;

namespace Frontend.Business.Movimientos.Ingresos
{
    public class DetallePedidoPosicion : SyncEntity
    {
        public string DocumentoReferencia { get; set; }
        public int PosicionDocumento { get; set; }
        public double CantidadPendiente { get; set; }
        public int Ejercicio { get; set; }
        public string ClaseMovimientoCodigo { get; set; }

        [ForeignKey(typeof(DetallePedido))]
        public int DetallePedidoId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public DetallePedido DetallePedido { get; set; }
    }
}
