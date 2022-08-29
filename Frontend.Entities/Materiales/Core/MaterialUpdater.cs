using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Materiales.Core
{
    public class MaterialUpdater
    {
        private readonly IRepository<Material> repository;

        public MaterialUpdater(IRepository<Material> repository)
        {
            this.repository = repository;
        }

        public async Task Update(Material material)
        {
            await repository.UpdateWithChildren(material);
        }
    }
}
