using Frontend.Business.Commons;
using Frontend.Business.Settings.Searchers;
using Frontend.Business.Synchronizer;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Traslados.Core
{
    public class TrasladoUpdater
    {
        private readonly IRepository<Traslado> repository;
        private readonly SettingSearcher settingSearcher;
        private readonly DetalleTrasladoUpdater detalleTrasladoUpdater;

        public TrasladoUpdater(IRepository<Traslado> repository, SettingSearcher settingSearcher,
            DetalleTrasladoUpdater detalleTrasladoUpdater)
        {
            this.repository = repository;
            this.settingSearcher = settingSearcher;
            this.detalleTrasladoUpdater = detalleTrasladoUpdater;
        }

        public async Task Update(Traslado traslado)
        {
            await repository.UpdateWithChildren(traslado);
        }

        public async Task Update(Traslado traslado, SyncState syncState = SyncState.Updated)
        {
            var setting = await settingSearcher.GetWithChildren();
            traslado.Usuario = setting.UsuarioActivo.IdRed;
            traslado.SyncState = syncState;
            await repository.UpdateWithChildren(traslado);
            await detalleTrasladoUpdater.Update(traslado.DetallesTraslado, syncState);
        }
    }
}
