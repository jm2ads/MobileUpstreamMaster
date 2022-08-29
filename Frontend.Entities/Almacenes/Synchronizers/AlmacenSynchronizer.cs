using Frontend.Business.Commons;
using Frontend.Business.IRestServices;
using Frontend.Business.Synchronizer;

namespace Frontend.Business.Almacenes.Synchronizers
{
    public class AlmacenSynchronizer : FullSynchronizer<Almacen>
    {
        public AlmacenSynchronizer(IRepository<Almacen> almacenRepository, ISyncRestService<Almacen> almacenRestService)
            : base(almacenRepository, almacenRestService)
        {

        }
    }
}