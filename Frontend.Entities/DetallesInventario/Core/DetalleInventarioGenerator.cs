using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.DetallesInventario.Core
{
    public class DetalleInventarioGenerator
    {
        private readonly IRepository<DetalleInventario> repository;

        public DetalleInventarioGenerator(IRepository<DetalleInventario> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(DetalleInventario detalleInventario)
        {
            await repository.SaveWithChildren(detalleInventario);
        }
    }
}
