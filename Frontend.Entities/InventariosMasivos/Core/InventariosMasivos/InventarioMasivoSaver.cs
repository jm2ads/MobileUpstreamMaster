using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.InventariosMasivos.Core.InventariosMasivos
{
    public class InventarioMasivoSaver
    {
        private readonly InventarioMasivoGenerator inventarioMasivoGenerator;
        private readonly InventarioMasivoUpdater inventarioMasivoUpdater;

        public InventarioMasivoSaver(InventarioMasivoGenerator inventarioMasivoGenerator, InventarioMasivoUpdater inventarioMasivoUpdater)
        {
            this.inventarioMasivoGenerator = inventarioMasivoGenerator;
            this.inventarioMasivoUpdater = inventarioMasivoUpdater;
        }

        public async Task Save(InventarioMasivo inventarioMasivo)
        {
            if (inventarioMasivo.Id == 0)
            {
                await inventarioMasivoGenerator.Generate(inventarioMasivo);
            }
            else
            {
                await inventarioMasivoUpdater.Update(inventarioMasivo);
            }
        }
    }
}
