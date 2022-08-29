using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Funcionalidades.Core
{
    public class FuncionalidadGenerator
    {
        private readonly IRepository<Funcionalidad> repository;

        public FuncionalidadGenerator(IRepository<Funcionalidad> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(Funcionalidad funcionalidad)
        {
            await repository.SaveWithChildren(funcionalidad);
        }
    }
}
