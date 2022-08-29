using Frontend.Business.Commons;
using Frontend.Business.Settings.Searchers;
using Frontend.Business.Synchronizer;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Ingresos.Core
{
    public class DetalleNotaDeEntregaPosicionUpdater
    {
        private readonly IRepository<DetalleNotaDeEntregaPosicion> repository;
        private readonly SettingSearcher settingSearcher;

        public DetalleNotaDeEntregaPosicionUpdater(IRepository<DetalleNotaDeEntregaPosicion> repository, SettingSearcher settingSearcher)
        {
            this.repository = repository;
            this.settingSearcher = settingSearcher;
        }

        public async Task Update(DetalleNotaDeEntregaPosicion detalleNotaDeEntregaPosicion, string claseMovimientoCodigo, SyncState syncState = SyncState.Updated)
        {
            var setting = await settingSearcher.GetWithChildren();
            detalleNotaDeEntregaPosicion.DetalleNotaDeEntrega.NotaDeEntrega.UsuarioCreacion = setting.UsuarioActivo.IdRed;
            detalleNotaDeEntregaPosicion.SyncState = syncState;
            detalleNotaDeEntregaPosicion.ClaseDeMovimientoCodigo = claseMovimientoCodigo;
            await repository.UpdateWithChildren(detalleNotaDeEntregaPosicion);
        }
    }
}
