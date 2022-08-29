using Frontend.Business.Commons;
using Frontend.Business.Movimientos.Reservas.Core.DetallesReservas;
using Frontend.Business.Synchronizer;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Frontend.Business.Movimientos.NotasDeReservas.Core.DetallesNotasDeReservas
{
    public class DetalleNotaDeReservaUpdater
    {
        private readonly IRepository<DetalleNotaDeReserva> repository;
        private readonly DetalleReservaUpdater detalleReservaUpdater;

        public DetalleNotaDeReservaUpdater(IRepository<DetalleNotaDeReserva> repository, DetalleReservaUpdater detalleReservaUpdater)
        {
            this.repository = repository;
            this.detalleReservaUpdater = detalleReservaUpdater;
        }

        public async Task UpdateAll(IList<DetalleNotaDeReserva> detalleNotaDeReservaList, SyncState syncState = SyncState.Updated)
        {
            detalleNotaDeReservaList.ForEach(x=>x.SyncState = syncState);
            await repository.UpdateAll(detalleNotaDeReservaList);
        }

        public async Task Update(DetalleNotaDeReserva detalleNotaDeReserva, SyncState syncState = SyncState.Updated)
        {
            detalleNotaDeReserva.SyncState = syncState;
            await repository.UpdateWithChildren(detalleNotaDeReserva);
            await detalleReservaUpdater.Update(detalleNotaDeReserva.DetalleReserva);
        }
    }
}
