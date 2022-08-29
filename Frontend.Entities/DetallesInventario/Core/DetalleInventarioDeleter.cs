using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.DetallesInventario.Core
{
    public class DetalleInventarioDeleter
    {
        private readonly IRepository<DetalleInventario> repository;

        public DetalleInventarioDeleter(IRepository<DetalleInventario> repository)
        {
            this.repository = repository;
        }

        public async Task DeleteAll()
        {
            await repository.DeleteAll();
        }

        public async Task Delete(DetalleInventario detalleInventario)
        {
            await repository.Delete(detalleInventario.Id);
        }
    }
}
