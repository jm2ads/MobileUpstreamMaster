using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Traslados.Searchers
{
    public class TrasladoSearcher
    {
        private readonly IRepository<Traslado> repository;

        public TrasladoSearcher(IRepository<Traslado> repository)
        {
            this.repository = repository;
        }

        public async Task<Traslado> GetById(int id)
        {
            return await repository.GetWithChildren(id);
        }
    }
}
