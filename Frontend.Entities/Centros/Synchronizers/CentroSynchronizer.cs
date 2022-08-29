using Frontend.Business.Commons;
using Frontend.Business.IRestServices;
using Frontend.Business.Synchronizer;

namespace Frontend.Business.Centros.Synchronizers
{
    public class CentroSynchronizer : FullSynchronizer<Centro>
    {
        public CentroSynchronizer(IRepository<Centro> centroRepository, ISyncRestService<Centro> centroRestService)
            : base(centroRepository, centroRestService)
        {

        }
    }
}