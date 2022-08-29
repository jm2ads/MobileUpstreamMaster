using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.GruposDeArticulos.Core
{
    public class GrupoDeArticuloUpdater
    {
        private readonly IRepository<GrupoDeArticulo> repository;

        public GrupoDeArticuloUpdater(IRepository<GrupoDeArticulo> repository)
        {
            this.repository = repository;
        }

        public async Task Update(GrupoDeArticulo grupoDeArticulo)
        {
            await repository.UpdateWithChildren(grupoDeArticulo);
        }
    }
}
