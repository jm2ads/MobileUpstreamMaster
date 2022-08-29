using Frontend.Business.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.InventariosMasivos.Searchers
{
    public class InventarioMasivoOrdenSearcher
    {
        private readonly IRepository<InventarioMasivoOrden> repository;

        public InventarioMasivoOrdenSearcher(IRepository<InventarioMasivoOrden> repository)
        {
            this.repository = repository;
        }

        public async Task<IList<InventarioMasivoOrden>> Get()
        {
            return await repository.GetAllWithChildren();
        }
    }
}
