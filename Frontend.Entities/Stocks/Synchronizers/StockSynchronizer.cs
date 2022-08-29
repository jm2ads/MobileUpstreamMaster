using Frontend.Business.Commons;
using Frontend.Business.IRestServices;
using Frontend.Business.Synchronizer;

namespace Frontend.Business.Stocks.Synchronizers
{
    public class StockSynchronizer : FullSynchronizer<Stock>
    {
        public StockSynchronizer(IRepository<Stock> repository, ISyncRestService<Stock> restService)
            : base(repository, restService)
        {

        }
    }
}
