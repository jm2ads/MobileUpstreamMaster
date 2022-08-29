using Frontend.Business.Commons;
using Frontend.Business.IRestServices;
using Frontend.Business.Synchronizer;

namespace Frontend.Business.EstadosInventarios.Synchronizers
{
    public class EstadoInventarioSynchronizer : FullSynchronizer<EstadoInventario>
    {
        public EstadoInventarioSynchronizer(IRepository<EstadoInventario> repository, ISyncRestService<EstadoInventario> restService)
            : base(repository, restService)
        {

        }
    }
}
