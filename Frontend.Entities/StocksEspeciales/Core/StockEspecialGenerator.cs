using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.StocksEspeciales.Core
{
    public class StockEspecialGenerator
    {
        private readonly IRepository<StockEspecial> repository;

        public StockEspecialGenerator(IRepository<StockEspecial> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(StockEspecial stockEspecial)
        {
            await repository.SaveWithChildren(stockEspecial);
        }
    }
}
