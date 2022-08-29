using Frontend.Business.Commons;
using Frontend.Business.Synchronizer;
using System.Threading.Tasks;

namespace Frontend.Business.InventariosMasivos.Core
{
    public class InventarioMasivoGenerator
    {
        private readonly IRepository<InventarioMasivo> repository;

        public InventarioMasivoGenerator(IRepository<InventarioMasivo> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(InventarioMasivo inventarioMasivo, SyncState syncState = SyncState.New)
        {
            inventarioMasivo.SyncState = syncState;
            await repository.SaveWithChildren(inventarioMasivo);
        }
    }
}
