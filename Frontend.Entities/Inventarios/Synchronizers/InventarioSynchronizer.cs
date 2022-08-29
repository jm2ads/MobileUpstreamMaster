using Frontend.Business.Commons;
using Frontend.Business.IRestServices;
using Frontend.Business.Synchronizer;

namespace Frontend.Business.Inventarios.Synchronizers
{
    public class InventarioSynchronizer : FullSynchronizer<Inventario>
    {
        public InventarioSynchronizer(IRepository<Inventario> repository, ISyncRestService<Inventario> restService)
            : base(repository, restService)
        {

        }
    }
}