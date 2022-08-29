using Frontend.Business.Commons;
using Frontend.Business.Synchronizer;
using System.Threading.Tasks;

namespace Frontend.Business.InventariosMasivos.Core
{
    public class InventarioMasivoUpdater
    {
        private readonly IRepository<InventarioMasivo> repository;
        private readonly DetalleInventarioMasivoSaver detalleInventarioMasivoSaver;

        public InventarioMasivoUpdater(IRepository<InventarioMasivo> repository, DetalleInventarioMasivoSaver detalleInventarioMasivoSaver)
        {
            this.repository = repository;
            this.detalleInventarioMasivoSaver = detalleInventarioMasivoSaver;
        }

        public async Task Update(InventarioMasivo inventarioMasivo, SyncState syncState = SyncState.Updated)
        {
            inventarioMasivo.SyncState = syncState;
            await repository.Update(inventarioMasivo);
            await detalleInventarioMasivoSaver.Save(inventarioMasivo.DetallesInventarioMasivo);
        }
    }
}
