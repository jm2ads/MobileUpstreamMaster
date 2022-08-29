using Frontend.Business.Synchronizer;

namespace Frontend.Business.StocksEspeciales
{
    public class StockEspecial : SyncEntity
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string NombreCampo { get; set; }
    }
}
