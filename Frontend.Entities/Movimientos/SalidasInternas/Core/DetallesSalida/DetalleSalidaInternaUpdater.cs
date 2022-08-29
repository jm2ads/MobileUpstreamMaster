using Frontend.Business.Commons;
using Frontend.Business.Synchronizer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.SalidasInternas.Core
{
    public class DetalleSalidaInternaUpdater
    {
        private readonly IRepository<DetalleSalidaInterna> repository;

        public DetalleSalidaInternaUpdater(IRepository<DetalleSalidaInterna> repository)
        {
            this.repository = repository;
        }

        public async Task Update(DetalleSalidaInterna detalleSalida)
        {
            await repository.UpdateWithChildren(detalleSalida);
        }

        public async Task Update(IList<DetalleSalidaInterna> list, SyncState syncState = SyncState.Updated)
        {
            foreach (var detalle in list)
            {
                detalle.SyncState = syncState;
                await Update(detalle);
            }
        }
    }
}
