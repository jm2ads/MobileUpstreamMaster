using Frontend.Business.Commons;
using Frontend.Commons.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Business.InventariosMasivos.Searchers
{
    public class InventarioMasivoSearcher
    {
        private readonly IRepository<InventarioMasivo> repository;

        public InventarioMasivoSearcher(IRepository<InventarioMasivo> repository)
        {
            this.repository = repository;
        }

        public async Task<IList<InventarioMasivo>> GetWithChildrenBy(params EstadoInventario[] estadoInventarios)
        {
            return await repository.FindWithChildren(x => estadoInventarios.Contains(x.Estado));
        }
    }
}
