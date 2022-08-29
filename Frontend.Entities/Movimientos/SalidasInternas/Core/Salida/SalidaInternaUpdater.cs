using Frontend.Business.Commons;
using Frontend.Business.Settings.Searchers;
using Frontend.Business.Synchronizer;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.SalidasInternas.Core
{
    public class SalidaInternaUpdater
    {
        private readonly IRepository<SalidaInterna> repository;
        private readonly SettingSearcher settingSearcher;
        private readonly DetalleSalidaInternaUpdater detalleSalidaInternaUpdater;

        public SalidaInternaUpdater(IRepository<SalidaInterna> repository, SettingSearcher settingSearcher,
            DetalleSalidaInternaUpdater detalleSalidaInternaUpdater)
        {
            this.repository = repository;
            this.settingSearcher = settingSearcher;
            this.detalleSalidaInternaUpdater = detalleSalidaInternaUpdater;
        }

        public async Task Update(SalidaInterna salida)
        {
            await repository.UpdateWithChildren(salida);
        }

        public async Task Update(SalidaInterna salidaInterna, SyncState syncState = SyncState.Updated)
        {
            var setting = await settingSearcher.GetWithChildren();
            salidaInterna.Usuario = setting.UsuarioActivo.IdRed;
            salidaInterna.SyncState = syncState;
            await repository.UpdateWithChildren(salidaInterna);
            await detalleSalidaInternaUpdater.Update(salidaInterna.DetallesSalidaInterna, syncState);
        }
    }
}
