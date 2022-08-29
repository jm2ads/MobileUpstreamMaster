using Frontend.Business.Commons;
using Frontend.Business.DetallesInventario.Core;
using System.Threading.Tasks;

namespace Frontend.Business.Inventarios.Core
{
    public class InventarioGenerator
    {
        private readonly IRepository<Inventario> repository;
        private readonly DetalleInventarioGenerator detalleInventarioGenerator;

        public InventarioGenerator(IRepository<Inventario> repository, DetalleInventarioGenerator detalleInventarioGenerator)
        {
            this.repository = repository;
            this.detalleInventarioGenerator = detalleInventarioGenerator;
        }

        public async Task<Inventario> Generate(Inventario inventario)
        {
            return await repository.SaveWithChildren(inventario);
        }
    }
}
