using Frontend.Business.Stocks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.IAzureRestServices
{
    public interface IStockAzureRestService
    {
        Task<IList<Stock>> GetAll();
        Task<IList<Stock>> GetByCentro(int centroId);
    }
}
