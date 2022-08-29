using Frontend.Business.Almacenes;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.Materiales;
using Frontend.Business.Stocks;
using Frontend.Business.Synchronizer;
using Frontend.Commons.Enums;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Frontend.Business.InventariosMasivos
{
    public class DetalleInventarioMasivo : SyncLocalEntity
    {
        public int Posicion { get; set; }
        public double Cantidad { get; set; }
        public string Ubicacion { get; set; }
        public string Unidad { get; set; }
        public string PEP{ get; set; }
        public int TipoStockId { get; set; }
        public bool HayConteoErroneo { get; set; }
        public TipoLote TipoLote { get; set; }
        public EstadoConteoEnum EstadoConteo { get; set; }

        [ForeignKey(typeof(Almacen))]
        public int? IdAlmacen { get; set; }
        [ManyToOne(foreignKey: "IdAlmacen", CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Almacen Almacen { get; set; }


        [ForeignKey(typeof(ClaseDeValoracion))]
        public int? IdLote { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public ClaseDeValoracion Lote { get; set; }

        [ForeignKey(typeof(InventarioMasivo))]
        public int InventarioId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public InventarioMasivo InventarioMasivo { get; set; }

        [ForeignKey(typeof(Material))]
        public int IdMaterial { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Material Material { get; set; }

        [ForeignKey(typeof(Stock))]
        public int? IdStock { get; set; }
        [ManyToOne(foreignKey: "IdStock", CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Stock Stock { get; set; }

        [Ignore]
        public string DisplayPosicion => Posicion.ToString().PadLeft(5, '0');

        [Ignore]
        public string DisplayCantidad => Cantidad + " " + Unidad;
    }
}