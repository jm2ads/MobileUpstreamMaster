using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.StocksEspeciales.Core
{
    public class StockEspecialUpdater
    {
        private readonly IRepository<StockEspecial> repository;

        public StockEspecialUpdater(IRepository<StockEspecial> repository)
        {
            this.repository = repository;
        }

        public async Task Update(StockEspecial stockEspecial)
        {
            await repository.UpdateWithChildren(stockEspecial);
        }
    }
}
