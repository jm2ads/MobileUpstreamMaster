using Frontend.Business.StocksEspeciales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.IAzureRestServices
{
    public interface IStockEspecialAzureRestService
    {
        Task<IList<StockEspecial>> GetAll();
    }
}
