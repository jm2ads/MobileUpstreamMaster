using Frontend.Business.Commons;
using Frontend.Business.IRestServices;
using Frontend.Business.Synchronizer;

namespace Frontend.Business.DetallesStocksEspeciales.Synchronizers
{
    public class DetalleStockEspecialSynchronizer : FullSynchronizer<DetalleStockEspecial>
    {
        public DetalleStockEspecialSynchronizer(IRepository<DetalleStockEspecial> repository, ISyncRestService<DetalleStockEspecial> restService)
            : base(repository, restService)
        {

        }
    }
}
