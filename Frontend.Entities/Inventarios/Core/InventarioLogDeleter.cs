using Frontend.Business.Commons;
using Frontend.Business.Inventarios.Searchers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.Inventarios.Core
{
    public class InventarioLogDeleter
    {
        private readonly IRepository<InventarioLog> repository;
        private readonly InventarioLogSearcher inventarioLogSearcher;

        public InventarioLogDeleter(IRepository<InventarioLog> repository, InventarioLogSearcher inventarioLogSearcher)
        {
            this.repository = repository;
            this.inventarioLogSearcher = inventarioLogSearcher;
        }

        public async Task Delete(int id)
        {
            await repository.Delete(id);
        }

        public async Task Delete(IList<int> listIdRemoto)
        {
            foreach (var idRemoto in listIdRemoto)
            {
                var listInventarioLog = await inventarioLogSearcher.GetAllBy(idRemoto);
                foreach (var inventarioLog in listInventarioLog)
                {
                    await Delete(inventarioLog.Id);
                }
            }
        }

        public async Task DeleteOlderThan(int cantidadDias)
        {
            var listInventarioLog = await inventarioLogSearcher.GetOlderThan(cantidadDias);
            foreach (var inventarioLog in listInventarioLog)
            {
                await Delete(inventarioLog.Id);
            }
        }
    }
}
