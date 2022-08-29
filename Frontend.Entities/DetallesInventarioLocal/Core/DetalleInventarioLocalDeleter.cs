using Frontend.Business.Commons;
using Frontend.Business.DetallesInventarioLocal;
using System.Threading.Tasks;

namespace Frontend.Business.DetallesInventario.Core
{
    public class DetalleInventarioLocalDeleter
    {
        private readonly IRepository<DetalleInventarioLocal> repository;

        public DetalleInventarioLocalDeleter(IRepository<DetalleInventarioLocal> repository)
        {
            this.repository = repository;
        }

        public async Task DeleteAll()
        {
            await repository.DeleteAll();
        }

        public async Task Delete(DetalleInventarioLocal detalleInventario)
        {
            await repository.Delete(detalleInventario);
        }
    }
}
