using Frontend.Business.Commons;
using Frontend.Business.IRestServices;
using Frontend.Business.Synchronizer;

namespace Frontend.Business.StocksEspeciales.Synchronizers
{
    public class StockEspecialSynchronizer : FullSynchronizer<StockEspecial>
    {
        public StockEspecialSynchronizer(IRepository<StockEspecial> repository, ISyncRestService<StockEspecial> restService)
            : base(repository, restService)
        {

        }
    }
}
