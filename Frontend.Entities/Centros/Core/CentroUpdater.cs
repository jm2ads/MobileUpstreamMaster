using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Centros.Core
{
    public class CentroUpdater
    {
        private readonly IRepository<Centro> repository;

        public CentroUpdater(IRepository<Centro> repository)
        {
            this.repository = repository;
        }

        public async Task Update(Centro centro)
        {
            await repository.UpdateWithChildren(centro);
        }
    }
}
