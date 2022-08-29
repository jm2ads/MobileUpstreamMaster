using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.DetallesInventarioLocal.Core
{
    public class DetalleInventarioLocalGenerator
    {
        private readonly IRepository<DetalleInventarioLocal> repository;

        public DetalleInventarioLocalGenerator(IRepository<DetalleInventarioLocal> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(DetalleInventarioLocal detalleInventario)
        {
            await repository.SaveWithChildren(detalleInventario);
        }
    }
}
