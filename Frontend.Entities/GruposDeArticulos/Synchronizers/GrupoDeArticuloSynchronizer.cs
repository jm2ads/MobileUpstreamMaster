using Frontend.Business.Commons;
using Frontend.Business.IRestServices;
using Frontend.Business.Synchronizer;

namespace Frontend.Business.GruposDeArticulos.Synchronizers
{
    public class GrupoDeArticuloSynchronizer : FullSynchronizer<GrupoDeArticulo>
    {
        public GrupoDeArticuloSynchronizer(IRepository<GrupoDeArticulo> repository, ISyncRestService<GrupoDeArticulo> restService)
            : base(repository, restService)
        {

        }
    }
}
