using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.DetallesInventarioLocal.Core
{
    public class DetalleInventarioLocalSaver
    {
        private readonly DetalleInventarioLocalUpdater detalleInventarioUpdater;
        private readonly DetalleInventarioLocalGenerator detalleInventarioGenerator;

        public DetalleInventarioLocalSaver(DetalleInventarioLocalUpdater detalleInventarioUpdater, DetalleInventarioLocalGenerator detalleInventarioGenerator)
        {
            this.detalleInventarioUpdater = detalleInventarioUpdater;
            this.detalleInventarioGenerator = detalleInventarioGenerator;
        }

        public async Task Save(DetalleInventarioLocal detalleInventario)
        {
            if (detalleInventario.Id != 0)
            {
                await detalleInventarioUpdater.Update(detalleInventario);
            }
            else
            {
                await detalleInventarioGenerator.Generate(detalleInventario);
            }
        }

        public async Task Save(IList<DetalleInventarioLocal> listDetalleInventario)
        {
            foreach (var detalleInventario in listDetalleInventario)
            {
                await Save(detalleInventario);
            }
        }
    }
}
