using Frontend.Business.Commons;
using Frontend.Business.Synchronizer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Traslados.Core
{
    public class DetalleTrasladoUpdater
    {
        private readonly IRepository<DetalleTraslado> repository;

        public DetalleTrasladoUpdater(IRepository<DetalleTraslado> repository)
        {
            this.repository = repository;
        }

        public async Task Update(DetalleTraslado detalleTraslado)
        {
            await repository.UpdateWithChildren(detalleTraslado);
        }

        public async Task Update(IList<DetalleTraslado> detalleTrasladoList, SyncState syncState = SyncState.Updated)
        {
            foreach (var detalleTraslado in detalleTrasladoList)
            {
                detalleTraslado.SyncState = syncState;
                await Update(detalleTraslado);
            }    
        }
    }
}
