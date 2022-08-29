using Frontend.Business.Almacenes;
using Frontend.Business.Centros;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.Stocks;
using Frontend.Business.StocksEspeciales;
using Frontend.Business.Synchronizer;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Frontend.Business.Movimientos.Traslados
{
    public class DetalleTraslado : SyncLocalEntity
    {
        [ForeignKey(typeof(Stock))]
        public int StockId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Stock Stock { get; set; }

        [ForeignKey(typeof(Centro))]
        public int CentroId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Centro Centro { get; set; }

        [ForeignKey(typeof(Almacen))]
        public int? AlmacenId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Almacen Almacen { get; set; }

        [ForeignKey(typeof(ClaseDeValoracion))]
        public int ClaseDeValoracionId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public ClaseDeValoracion ClaseDeValoracion { get; set; }

        [ForeignKey(typeof(StockEspecial))]
        public int? StockEspecialId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public StockEspecial StockEspecial { get; set; }

        [ForeignKey(typeof(Traslado))]
        public int TrasladoId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Traslado Traslado { get; set; }

        public string Proveedor { get; set; }
        public string ElementoPEP { get; set; }
        public int Posicion { get; set; }
        public string CodigoMaterial { get; set; }
        public double Cantidad { get; set; }
        public string TextoBreve { get; set; }

        [Ignore]
        public string DisplayPosicion => Posicion.ToString().PadLeft(5,'0');
        [Ignore]
        public string DisplayCantidad => Cantidad + " / " + Stock.CantidadAlmacen + " " + Stock.Material.UnidadDeMedidaBase;
        [Ignore]
        public string DisplayCantidadCalidad => (Cantidad > 0 && Cantidad < Stock.CantidadCalidad) 
                                                ? Cantidad + " / " + Stock.CantidadCalidad + " " + Stock.Material.UnidadDeMedidaBase 
                                                : Stock.CantidadCalidad + " / " + Stock.CantidadCalidad + " " + Stock.Material.UnidadDeMedidaBase;
    }
}
