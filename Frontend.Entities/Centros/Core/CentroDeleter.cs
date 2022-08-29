using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Centros.Core
{
    public class CentroDeleter
    {
        private readonly IRepository<Centro> repository;

        public CentroDeleter(IRepository<Centro> repository)
        {
            this.repository = repository;
        }

        public async Task DeleteAll()
        {
            await repository.DeleteAll();
        }
    }
}
