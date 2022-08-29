using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.DetallesInventario.Core
{
    public class DetalleInventarioSaver
    {
        private readonly DetalleInventarioUpdater detalleInventarioUpdater;
        private readonly DetalleInventarioGenerator detalleInventarioGenerator;

        public DetalleInventarioSaver(DetalleInventarioUpdater detalleInventarioUpdater, DetalleInventarioGenerator detalleInventarioGenerator)
        {
            this.detalleInventarioUpdater = detalleInventarioUpdater;
            this.detalleInventarioGenerator = detalleInventarioGenerator;
        }

        public async Task Save(DetalleInventario detalleInventario)
        {
            if (detalleInventario.Id != 0)
            {
                await detalleInventarioUpdater.Update(detalleInventario);
            }
            else
            {
                detalleInventario.Id = int .Parse(DateTime.UtcNow.ToString("ddhhmmssff"));
                await detalleInventarioGenerator.Generate(detalleInventario);
            }
        }
        
        public async Task Save(IList<DetalleInventario> listDetalleInventario)
        {
            foreach (var detalleInventario in listDetalleInventario)
            {
                await Save(detalleInventario);
            }
        }
    }
}
