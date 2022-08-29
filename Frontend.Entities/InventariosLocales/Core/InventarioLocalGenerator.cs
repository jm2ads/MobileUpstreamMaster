using Frontend.Business.Commons;
using Frontend.Business.DetallesInventarioLocal;
using System.Threading.Tasks;

namespace Frontend.Business.InventariosLocales.Core
{
    public class InventarioLocalGenerator
    {
        private readonly IRepository<InventarioLocal> repository;
        private readonly IRepository<DetalleInventarioLocal> repositoryDetalle;

        public InventarioLocalGenerator(IRepository<InventarioLocal> repository, IRepository<DetalleInventarioLocal> repositoryDetalle)
        {
            this.repository = repository;
            this.repositoryDetalle = repositoryDetalle;
        }

        public async Task<InventarioLocal> Generate(InventarioLocal inventario)
        {
            return await repository.SaveWithChildren(inventario);
        }
    }
}
