using Frontend.Business.Commons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Business.Inventarios.Core
{
    public class InventarioDeleter
    {
        private readonly IRepository<Inventario> repository;

        public InventarioDeleter(IRepository<Inventario> repository)
        {
            this.repository = repository;
        }
        
        public async Task DeleteAll()
        {
            await repository.DeleteAll();
        }
        public async Task Delete(Inventario inventario)
        {
            await repository.Delete(inventario);
        }
    }
}
