using Frontend.Business.Commons;
using Frontend.Business.IRestServices;
using Frontend.Business.Synchronizer;

namespace Frontend.Business.Materiales.Synchronizers
{
    public class MaterialSynchronizer : FullSynchronizer<Material>
    {
        public MaterialSynchronizer(IRepository<Material> repository, ISyncRestService<Material> restService)
            : base(repository, restService)
        {

        }
    }
}