using Frontend.Business.Inventarios.StockEspeciales.Searchers;
using Frontend.Business.StocksEspeciales;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class StockEspecialService : IStockEspecialService
    {
        private readonly StockEspecialSearcher stockEspecialSearcher;

        public StockEspecialService(StockEspecialSearcher stockEspecialSearcher)
        {
            this.stockEspecialSearcher = stockEspecialSearcher;
        }

        public async Task<IList<StockEspecial>> GetAll()
        {
            return await stockEspecialSearcher.GetAll();
        }

        public async Task<StockEspecial> GetByCodigo(string codigo)
        {
            return await stockEspecialSearcher.GetByCodigo(codigo);
        }
    }
}
