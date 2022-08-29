using Frontend.Business.Commons;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.InventariosMasivos.Core
{
    public class DetalleInventarioMasivoUpdater
    {
        private readonly IRepository<DetalleInventarioMasivo> repository;

        public DetalleInventarioMasivoUpdater(IRepository<DetalleInventarioMasivo> repository)
        {
            this.repository = repository;
        }

        public async Task Update(DetalleInventarioMasivo detalleInventarioMasivo)
        {
            await repository.Update(detalleInventarioMasivo);
        }

        public async Task Update(IList<DetalleInventarioMasivo> list)
        {
            foreach (var detalleInventarioMasivo in list)
            {
                await Update(detalleInventarioMasivo);
            }
        }
    }
}
