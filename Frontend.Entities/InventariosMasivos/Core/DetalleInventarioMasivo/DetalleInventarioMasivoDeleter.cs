using Frontend.Business.Commons;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.InventariosMasivos.Core
{
    public class DetalleInventarioMasivoDeleter
    {
        private readonly IRepository<DetalleInventarioMasivo> repository;

        public DetalleInventarioMasivoDeleter(IRepository<DetalleInventarioMasivo> repository)
        {
            this.repository = repository;
        }

        public async Task Delete(DetalleInventarioMasivo detalleInventarioMasivo)
        {
            await repository.Delete(detalleInventarioMasivo);
        }

        public async Task Delete(List<DetalleInventarioMasivo> detallesInventarioMasivo)
        {
            foreach (var detalleInventarioMasivo in detallesInventarioMasivo)
            {
                await this.Delete(detalleInventarioMasivo);
            }
        }
    }
}
