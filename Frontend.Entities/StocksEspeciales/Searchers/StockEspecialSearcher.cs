using Frontend.Business.Commons;
using Frontend.Business.StocksEspeciales;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Business.Inventarios.StockEspeciales.Searchers
{
    public class StockEspecialSearcher
    {
        private readonly IRepository<StockEspecial> repository;
        private IList<StockEspecial> StocksEspecial;

        public StockEspecialSearcher(IRepository<StockEspecial> repository)
        {
            this.repository = repository;
        }

        public async Task<StockEspecial> GetById(int id)
        {
            return await repository.GetWithChildren(id);
        }

        public async Task<IList<StockEspecial>> GetAll()
        {
            return await repository.GetAllWithChildren();
        }

        public async Task<StockEspecial> GetByCodigo(string codigo)
        {
            return await repository.FindFirstWithChildren(x=> x.Codigo == codigo);
        }

        public async Task<IList<StockEspecial>> GetByCodigos(params string[] codigos)
        {
            return await repository.FindWithChildren(x => codigos.Contains(x.Codigo));
        }

        //public async Task<StockEspecial> GetByRemoteId(int remoteId)
        //{
        //    return await repository.FindFirstWithChildren(x => x.RemoteId == remoteId);
        //}
    }
}
