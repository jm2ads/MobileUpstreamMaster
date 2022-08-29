using Frontend.Business.StocksEspeciales;
using Frontend.Business.Synchronizer;
using SQLiteNetExtensions.Attributes;

namespace Frontend.Business.DetallesStocksEspeciales
{
    public class DetalleStockEspecial : SyncEntity
    {
        public string Detalle { get; set; }

        [ForeignKey(typeof(StockEspecial))]
        public int IdStockEspecial { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public StockEspecial StockEspecial { get; set; }
    }
}
