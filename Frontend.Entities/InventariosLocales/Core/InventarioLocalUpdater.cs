using Frontend.Business.Commons;
using Frontend.Business.DetallesInventarioLocal.Core;
using Frontend.Business.Settings.Searchers;
using Frontend.Business.Synchronizer;
using System;
using System.Threading.Tasks;

namespace Frontend.Business.InventariosLocales.Core
{
    public class InventarioLocalUpdater
    {
        private readonly IRepository<InventarioLocal> repository;
        private readonly DetalleInventarioLocalSaver detalleInventarioSaver;
        private readonly SettingSearcher settingSearcher;

        public InventarioLocalUpdater(IRepository<InventarioLocal> repository, DetalleInventarioLocalSaver detalleInventarioSaver, SettingSearcher settingSearcher)
        {
            this.repository = repository;
            this.detalleInventarioSaver = detalleInventarioSaver;
            this.settingSearcher = settingSearcher;
        }

        public async Task Update(InventarioLocal inventario)
        {
            var setting = await settingSearcher.GetWithChildren();
            inventario.UsuarioModificacion = setting.UsuarioActivo.IdRed;
            inventario.FechaModificacion = DateTime.Now;
            await repository.UpdateWithChildren(inventario);
            await detalleInventarioSaver.Save(inventario.DetallesInventario);
        }

        public async Task SetPendienteAprobacion(InventarioLocal inventario)
        {
            inventario.Estado = Frontend.Commons.Enums.EstadoInventario.PendienteAprobacion;
            inventario.SyncState = SyncState.PendingToSync;
            await Update(inventario);
        }
    }
}
