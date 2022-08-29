using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Stocks.Core
{
    public class StockGenerator
    {
        private readonly IRepository<Stock> repository;

        public StockGenerator(IRepository<Stock> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(Stock stock)
        {
            await repository.SaveWithChildren(stock);
        }
    }
}
