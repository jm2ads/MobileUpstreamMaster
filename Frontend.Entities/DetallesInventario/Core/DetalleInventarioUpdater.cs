using Frontend.Business.Commons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Business.DetallesInventario.Core
{
    public class DetalleInventarioUpdater
    {
        private readonly IRepository<DetalleInventario> repository;

        public DetalleInventarioUpdater(IRepository<DetalleInventario> repository)
        {
            this.repository = repository;
        }

        public async Task Update(DetalleInventario detalleInventario)
        {
            await repository.UpdateWithChildren(detalleInventario);
        }

        public async Task Update(IList<DetalleInventario> listDetalleInventario)
        {
            foreach (var detalleInventario in listDetalleInventario)
            {
                await Update(detalleInventario);
            }
        }
    }
}
