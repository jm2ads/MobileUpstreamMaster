using Frontend.Business.DetallesStocksEspeciales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IDetalleStockEspecialService
    {
        Task<IList<DetalleStockEspecial>> GetByStockEspecialId(int stockEspecialId);
        Task<DetalleStockEspecial> GetByPEP(string pep);
    }
}
