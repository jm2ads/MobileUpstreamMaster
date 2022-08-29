using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.ClasesDeValoracion.Core
{
    public class ClaseDeValoracionGenerator
    {
        private readonly IRepository<ClaseDeValoracion> repository;

        public ClaseDeValoracionGenerator(IRepository<ClaseDeValoracion> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(ClaseDeValoracion claseDeValoracion)
        {
            await repository.SaveWithChildren(claseDeValoracion);
        }
    }
}
