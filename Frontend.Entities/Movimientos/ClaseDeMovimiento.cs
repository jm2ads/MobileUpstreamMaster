using Frontend.Business.Synchronizer;
using SQLiteNetExtensions.Attributes;

namespace Frontend.Business.Movimientos
{
    public class ClaseDeMovimiento : SyncEntity
    {
        public string Codigo { get; set; }

        [ForeignKey(typeof(Movimiento))]
        public int MovimientoId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Movimiento Movimiento { get; set; }
    }
}
