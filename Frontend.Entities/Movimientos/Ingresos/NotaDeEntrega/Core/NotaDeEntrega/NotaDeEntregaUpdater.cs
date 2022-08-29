using Frontend.Business.Commons;
using Frontend.Business.Settings.Searchers;
using Frontend.Business.Synchronizer;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Ingresos.Core
{
    public class NotaDeEntregaUpdater
    {
        private readonly IRepository<NotaDeEntrega> repository;
        private readonly SettingSearcher settingSearcher;
        private readonly PedidoUpdater pedidoUpdater;
        private readonly DetalleNotaDeEntregaUpdater detalleNotaDeEntregaUpdater;

        public NotaDeEntregaUpdater(IRepository<NotaDeEntrega> repository, SettingSearcher settingSearcher, PedidoUpdater pedidoUpdater, DetalleNotaDeEntregaUpdater detalleNotaDeEntregaUpdater)
        {
            this.repository = repository;
            this.settingSearcher = settingSearcher;
            this.pedidoUpdater = pedidoUpdater;
            this.detalleNotaDeEntregaUpdater = detalleNotaDeEntregaUpdater;
        }
        public async Task Update(NotaDeEntrega notaDeEntrega, SyncState syncState = SyncState.Updated)
        {
            var setting = await settingSearcher.GetWithChildren();
            notaDeEntrega.UsuarioCreacion= setting.UsuarioActivo.IdRed;
            notaDeEntrega.SyncState = syncState;
            await repository.UpdateWithChildren(notaDeEntrega);
            await pedidoUpdater.Update(notaDeEntrega.Pedido);
            await detalleNotaDeEntregaUpdater.UpdateAll(notaDeEntrega.DetalleNotaDeEntrega, syncState);
        }
    }
}
