using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.ClasesDeValoracion.Deleters
{
    public class ClaseDeValoracionDeleter
    {
        private readonly IRepository<ClaseDeValoracion> repository;

        public ClaseDeValoracionDeleter(IRepository<ClaseDeValoracion> repository)
        {
            this.repository = repository;
        }

        public async Task DeleteAll()
        {
            await repository.DeleteAll();
        }
    }
}
