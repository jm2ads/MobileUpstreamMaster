using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.ClasesDeValoracion.Core
{
    public class ClaseDeValoracionUpdater
    {
        private readonly IRepository<ClaseDeValoracion> repository;

        public ClaseDeValoracionUpdater(IRepository<ClaseDeValoracion> repository)
        {
            this.repository = repository;
        }

        public async Task Update(ClaseDeValoracion claseDeValoracion)
        {
            await repository.UpdateWithChildren(claseDeValoracion);
        }
    }
}
