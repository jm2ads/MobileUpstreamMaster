using Frontend.Business.Almacenes;
using Frontend.Business.Centros;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.StocksEspeciales;
using Frontend.Business.Synchronizer;
using SQLiteNetExtensions.Attributes;

namespace Frontend.Business.InventariosMasivos
{
    public class InventarioMasivoOrden: SyncEntity
    {
        public int Orden { get; set; }

        [ForeignKey(typeof(Centro))]
        public int CentroId { get; set; }
        [ManyToOne(foreignKey: "CentroId", CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Centro Centro { get; set; }

        [ForeignKey(typeof(StockEspecial))]
        public int StockEspecialId { get; set; }
        [ManyToOne(foreignKey: "StockEspecialId", CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public StockEspecial StockEspecial { get; set; }

        [ForeignKey(typeof(Almacen))]
        public int AlmacenId { get; set; }
        [ManyToOne(foreignKey: "AlmacenId", CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Almacen Almacen { get; set; }

        [ForeignKey(typeof(ClaseDeValoracion))]
        public int ClaseDeValoracionId { get; set; }
        [ManyToOne(foreignKey: "ClaseDeValoracionId", CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public ClaseDeValoracion ClaseDeValoracion { get; set; }
    }
}
