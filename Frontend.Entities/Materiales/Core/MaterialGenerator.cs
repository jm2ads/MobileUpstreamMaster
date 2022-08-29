using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Materiales.Core
{
    public class MaterialGenerator
    {
        private readonly IRepository<Material> repository;

        public MaterialGenerator(IRepository<Material> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(Material material)
        {
            await repository.SaveWithChildren(material);
        }
    }
}
