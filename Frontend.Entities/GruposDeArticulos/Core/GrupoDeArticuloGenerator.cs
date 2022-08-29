using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.GruposDeArticulos.Core
{
    public class GrupoDeArticuloGenerator
    {
        private readonly IRepository<GrupoDeArticulo> repository;

        public GrupoDeArticuloGenerator(IRepository<GrupoDeArticulo> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(GrupoDeArticulo grupoDeArticulo)
        {
            await repository.SaveWithChildren(grupoDeArticulo);
        }
    }
}
