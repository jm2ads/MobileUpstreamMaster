using Frontend.Business.DetallesStocksEspeciales;
using Frontend.Business.DetallesStocksEspeciales.Searchers;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class DetalleStockEspecialService : IDetalleStockEspecialService
    {
        private readonly DetalleStockEspecialSearcher detalleStockEspecialSearcher;

        public DetalleStockEspecialService(DetalleStockEspecialSearcher detalleStockEspecialSearcher)
        {
            this.detalleStockEspecialSearcher = detalleStockEspecialSearcher;
        }

        public async Task<IList<DetalleStockEspecial>> GetByStockEspecialId(int stockEspecialId)
        {
            return await detalleStockEspecialSearcher.GetByStockEspecialId(stockEspecialId);
        }

        public async Task<DetalleStockEspecial> GetByPEP(string pep)
        {
            return await detalleStockEspecialSearcher.GetBy(pep);
        }
    }
}
