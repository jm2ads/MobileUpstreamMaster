using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.DetallesStocksEspeciales;
using Frontend.Business.InventariosLocales;
using Frontend.Business.Stocks;
using Frontend.Business.Synchronizer;
using Frontend.Commons.Enums;
using SQLiteNetExtensions.Attributes;

namespace Frontend.Business.DetallesInventarioLocal
{
    public class DetalleInventarioLocal : SyncLocalEntity
    {
        public double Cantidad { get; set; }
        public double CantidadContada { get; set; }
        public bool EsContado { get; set; }
        public string Posicion { get; set; }
        public int TipoStockId { get; set; }
        public string Ubicacion { get; set; }
        public string Comentario { get; set; }
        public string UnidadAlmacen { get; set; }
        public EstadoConteoEnum EstadoConteo { get; set; }

        [ForeignKey(typeof(InventarioLocal))]
        public int InventarioId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public InventarioLocal Inventario { get; set; }

        [ForeignKey(typeof(Stock))]
        public int StockId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Stock Stock { get; set; }

        [ForeignKey(typeof(ClaseDeValoracion))]
        public int ClaseDeValoracionId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public ClaseDeValoracion Lote { get; set; }

        [ForeignKey(typeof(DetalleStockEspecial))]
        public int DetalleStockEspecialId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public DetalleStockEspecial DetalleStockEspecial { get; set; }

        public DetalleInventarioLocal(InventarioLocal inventario, Stock stock)
        {
            Posicion = "0000";
            EsContado = false;
            UnidadAlmacen = stock.Material.UnidadDeMedidaBase;
            InventarioId = inventario.Id;
            Inventario = inventario;
            StockId = stock.Id;
            Stock = stock;
            Ubicacion = string.Empty;
        }

        public DetalleInventarioLocal()
        {
        }
    }
}
