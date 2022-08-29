using Frontend.Business.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.DetallesInventarioLocal.Core
{
    public class DetalleInventarioLocalUpdater
    {
        private readonly IRepository<DetalleInventarioLocal> repository;

        public DetalleInventarioLocalUpdater(IRepository<DetalleInventarioLocal> repository)
        {
            this.repository = repository;
        }

        public async Task Update(DetalleInventarioLocal detalleInventario)
        {
            await repository.UpdateWithChildren(detalleInventario);
        }

        public async Task Update(IList<DetalleInventarioLocal> listDetalleInventario)
        {
            foreach (var detalleInventario in listDetalleInventario)
            {
                await Update(detalleInventario);
            }
        }
    }
}
