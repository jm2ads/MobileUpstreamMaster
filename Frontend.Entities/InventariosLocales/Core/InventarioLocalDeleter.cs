using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.InventariosLocales.Core
{
    public class InventarioLocalDeleter
    {
        private readonly IRepository<InventarioLocal> repository;

        public InventarioLocalDeleter(IRepository<InventarioLocal> repository)
        {
            this.repository = repository;
        }

        public async Task DeleteAll()
        {
            await repository.DeleteAll();
        }
        public async Task Delete(InventarioLocal inventario)
        {
            await repository.Delete(inventario);
        }
    }
}
