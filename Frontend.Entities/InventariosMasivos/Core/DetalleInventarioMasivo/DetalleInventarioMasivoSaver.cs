using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.InventariosMasivos.Core
{
    public class DetalleInventarioMasivoSaver
    {
        private readonly DetalleInventarioMasivoUpdater detalleInventarioMasivoUpdater;
        private readonly DetalleInventarioMasivoGenerator detalleInventarioMasivoGenerator;

        public DetalleInventarioMasivoSaver(DetalleInventarioMasivoUpdater detalleInventarioMasivoUpdater, DetalleInventarioMasivoGenerator detalleInventarioMasivoGenerator)
        {
            this.detalleInventarioMasivoUpdater = detalleInventarioMasivoUpdater;
            this.detalleInventarioMasivoGenerator = detalleInventarioMasivoGenerator;
        }


        public async Task Save(IList<DetalleInventarioMasivo> list)
        {
            foreach (var detalleInventarioMasivo in list)
            {
                await Save(detalleInventarioMasivo);
            }
        }

        public async Task Save(DetalleInventarioMasivo detalleInventarioMasivo)
        {
            if (detalleInventarioMasivo.Id != 0)
            {
                await detalleInventarioMasivoUpdater.Update(detalleInventarioMasivo);
            }
            else
            {
                await detalleInventarioMasivoGenerator.Generate(detalleInventarioMasivo);
            }
        }
    }
}
