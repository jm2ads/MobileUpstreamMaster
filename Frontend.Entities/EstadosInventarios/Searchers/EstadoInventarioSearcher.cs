using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.EstadosInventarios.Searchers
{
    public class EstadoInventarioSearcher
    {
        private readonly IRepository<EstadoInventario> repository;

        public EstadoInventarioSearcher(IRepository<EstadoInventario> repository)
        {
            this.repository = repository;
        }

        //public async Task<EstadoInventario> GetByRemoteId(int remoteId)
        //{
        //    return await repository.FindFirstWithChildren(x => x.RemoteId == remoteId);
        //}
    }
}
