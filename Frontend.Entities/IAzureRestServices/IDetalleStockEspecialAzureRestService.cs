using Frontend.Business.DetallesStocksEspeciales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.IAzureRestServices
{
    public interface IDetalleStockEspecialAzureRestService
    {
        Task<IList<DetalleStockEspecial>> GetAll();
    }
}
