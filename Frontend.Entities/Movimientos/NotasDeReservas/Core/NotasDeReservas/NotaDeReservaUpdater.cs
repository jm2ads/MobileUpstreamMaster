using Frontend.Business.Commons;
using Frontend.Business.Movimientos.NotasDeReservas.Core.DetallesNotasDeReservas;
using Frontend.Business.Movimientos.Reservas.Core.Reservas;
using Frontend.Business.Settings.Searchers;
using Frontend.Business.Synchronizer;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.NotasDeReservas.Core.NotasDeReservas
{
    public class NotaDeReservaUpdater
    {
        private readonly IRepository<NotaDeReserva> repository;
        private readonly DetalleNotaDeReservaUpdater detalleNotaDeReservaUpdater;
        private readonly SettingSearcher settingSearcher;
        private readonly ReservaUpdater reservaUpdater;

        public NotaDeReservaUpdater(IRepository<NotaDeReserva> repository, DetalleNotaDeReservaUpdater detalleNotaDeReservaUpdater, SettingSearcher settingSearcher,
            ReservaUpdater reservaUpdater)
        {
            this.repository = repository;
            this.detalleNotaDeReservaUpdater = detalleNotaDeReservaUpdater;
            this.settingSearcher = settingSearcher;
            this.reservaUpdater = reservaUpdater;
        }

        public async Task Update(NotaDeReserva notaDeReserva, SyncState syncState = SyncState.Updated)
        {
            var setting = await settingSearcher.GetWithChildren();
            notaDeReserva.UsuarioReserva = setting.UsuarioActivo.IdRed;
            notaDeReserva.SyncState = syncState;
            await repository.UpdateWithChildren(notaDeReserva);
            await reservaUpdater.Update(notaDeReserva.Reserva);
            await detalleNotaDeReservaUpdater.UpdateAll(notaDeReserva.DetallesNotasDeReservas, syncState);
        }
    }
}
