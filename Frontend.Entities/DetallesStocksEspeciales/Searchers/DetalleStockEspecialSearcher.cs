using Frontend.Business.Commons;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Business.DetallesStocksEspeciales.Searchers
{
    public class DetalleStockEspecialSearcher
    {
        private readonly IRepository<DetalleStockEspecial> repository;

        public DetalleStockEspecialSearcher(IRepository<DetalleStockEspecial> repository)
        {
            this.repository = repository;
        }

        public async Task<DetalleStockEspecial> GetById(int id)
        {
            return await repository.GetWithChildren(id);
        }

        public async Task<IList<DetalleStockEspecial>> GetByStockEspecialId(int idStockEspecial)
        {
            return await repository.Where(x => x.IdStockEspecial == idStockEspecial);
        }

        public async Task<DetalleStockEspecial> GetBy(string pep)
        {
            return await repository.FindFirstWithChildren(x => x.Detalle == pep);
        }

        public async Task<IList<DetalleStockEspecial>> GetExceptByStockEspecialId(int idStockEspecial)
        {
            return await repository.Where(x => x.IdStockEspecial != idStockEspecial);
        }

        public async Task<IList<DetalleStockEspecial>> GetExceptByStockEspecialId(IList<int> listIdStockEspecial)
        {
            return await repository.Where(x => !listIdStockEspecial.Contains(x.IdStockEspecial));
        }

        public async Task<IList<DetalleStockEspecial>> GetAll()
        {
            return await repository.GetAll();
        }

        public async Task<IList<int>> GetAllByCodigos(List<int> stockEspecialesIds)
        {
            return (await repository.Where(x => stockEspecialesIds.Contains(x.IdStockEspecial))).Select(x => x.Id).ToList();
        }
    }
}
