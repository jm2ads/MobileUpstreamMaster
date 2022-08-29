using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Stocks.Core
{
    public class StockUpdater
    {
        private readonly IRepository<Stock> repository;

        public StockUpdater(IRepository<Stock> repository)
        {
            this.repository = repository;
        }

        public async Task Update(Stock stock)
        {
            await repository.UpdateWithChildren(stock);
        }
    }
}
