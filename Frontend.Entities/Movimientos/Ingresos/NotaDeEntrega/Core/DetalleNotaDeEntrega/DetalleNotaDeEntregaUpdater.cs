using Frontend.Business.Commons;
using Frontend.Business.Settings.Searchers;
using Frontend.Business.Synchronizer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Frontend.Business.Movimientos.Ingresos.Core
{
    public class DetalleNotaDeEntregaUpdater
    {
        private readonly IRepository<DetalleNotaDeEntrega> repository;
        private readonly DetalleNotaDeEntregaPosicionUpdater detalleNotaDeEntregaPosicionUpdater;
        private readonly SettingSearcher settingSearcher;

        public DetalleNotaDeEntregaUpdater(IRepository<DetalleNotaDeEntrega> repository, DetalleNotaDeEntregaPosicionUpdater detalleNotaDeEntregaPosicionUpdater, SettingSearcher settingSearcher)
        {
            this.repository = repository;
            this.detalleNotaDeEntregaPosicionUpdater = detalleNotaDeEntregaPosicionUpdater;
            this.settingSearcher = settingSearcher;
        }

        public async Task UpdateAll(IList<DetalleNotaDeEntrega> detalleNotaDeEntregaList, SyncState syncState = SyncState.Updated)
        {
            detalleNotaDeEntregaList.ForEach(async x => await Update(x, syncState));
            await repository.SaveAllWithChildren(detalleNotaDeEntregaList);
        }

        public async Task Update(DetalleNotaDeEntrega detallenotaDeEntrega, SyncState syncState)
        {
            var setting = await settingSearcher.GetWithChildren();
            detallenotaDeEntrega.SyncState = syncState;
            detallenotaDeEntrega.CentroId = setting.CentroActivoId;
            await repository.SaveWithChildren(detallenotaDeEntrega);
        }
    }
}
