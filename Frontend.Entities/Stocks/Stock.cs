using Frontend.Business.Almacenes;
using Frontend.Business.Centros;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.DetallesInventario;
using Frontend.Business.DetallesInventarioLocal;
using Frontend.Business.DetallesStocksEspeciales;
using Frontend.Business.Materiales;
using Frontend.Business.Synchronizer;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Frontend.Business.Stocks
{
    public class Stock : SyncEntity
    {
        public double CantidadAlmacen { get; set; }
        public double CantidadBloqueado { get; set; }
        public double CantidadCalidad { get; set; }
        public string Ubicacion { get; set; }

        [ForeignKey(typeof(DetalleInventario))]
        public int DetalleInventarioId { get; set; }

        [ForeignKey(typeof(DetalleStockEspecial))]
        public int IdDetalleStockEspecial { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public DetalleStockEspecial DetalleStockEspecial { get; set; }

        [ForeignKey(typeof(Material))]
        public int IdMaterial { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Material Material { get; set; }

        [ForeignKey(typeof(Almacen))]
        public int? IdAlmacen { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Almacen Almacen { get; set; }

        [ForeignKey(typeof(Centro))]
        public int IdCentro { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Centro Centro { get; set; }

        [ForeignKey(typeof(ClaseDeValoracion))]
        public int IdClaseDeValoracion { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public ClaseDeValoracion ClaseDeValoracion { get; set; }

        [Ignore]
        public double CantidadTotal => CantidadAlmacen + CantidadBloqueado + CantidadCalidad;
    }
}
