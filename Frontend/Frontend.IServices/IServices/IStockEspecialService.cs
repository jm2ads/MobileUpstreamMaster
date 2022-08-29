using Frontend.Business.StocksEspeciales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IStockEspecialService
    {
        Task<IList<StockEspecial>> GetAll();
        Task<StockEspecial> GetByCodigo(string codigo); 
    }
}
