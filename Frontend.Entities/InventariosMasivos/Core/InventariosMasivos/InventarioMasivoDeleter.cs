using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.InventariosMasivos.Core
{
    public class InventarioMasivoDeleter
    {
        private readonly IRepository<InventarioMasivo> repository;
        private readonly DetalleInventarioMasivoDeleter detalleInventarioMasivoDeleter;

        public InventarioMasivoDeleter(IRepository<InventarioMasivo> repository, DetalleInventarioMasivoDeleter detalleInventarioMasivoDeleter)
        {
            this.repository = repository;
            this.detalleInventarioMasivoDeleter = detalleInventarioMasivoDeleter;
        }

        public async Task Delete(InventarioMasivo inventarioMasivo)
        {
            await repository.Delete(inventarioMasivo);
            await detalleInventarioMasivoDeleter.Delete(inventarioMasivo.DetallesInventarioMasivo);
        }
    }
}
