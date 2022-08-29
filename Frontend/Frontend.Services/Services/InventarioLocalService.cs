using Frontend.Business.DetallesInventario.Core;
using Frontend.Business.DetallesInventarioLocal;
using Frontend.Business.Inventarios;
using Frontend.Business.InventariosLocales;
using Frontend.Business.InventariosLocales.Core;
using Frontend.Business.InventariosLocales.Searchers;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class InventarioLocalService : IInventarioLocalService
    {
        private readonly InventarioLocalFactory inventarioFactory;
        private readonly InventarioLocalGenerator inventarioLocalGenerator;
        private readonly InventarioLocalDeleter inventarioDeleter;
        private readonly InventarioLocalSearcher inventarioSearcher;
        private readonly InventarioLocalUpdater inventarioUpdater;
        private readonly DetalleInventarioLocalDeleter detalleInventarioLocalDeleter;
        private readonly ISettingsService settingsService;

        public InventarioLocalService(InventarioLocalFactory inventarioFactory, InventarioLocalGenerator inventarioLocalGenerator, ISettingsService settingsService, InventarioLocalDeleter inventarioDeleter,
            InventarioLocalSearcher inventarioSearcher, InventarioLocalUpdater inventarioUpdater, DetalleInventarioLocalDeleter detalleInventarioLocalDeleter)
        {
            this.inventarioFactory = inventarioFactory;
            this.inventarioLocalGenerator = inventarioLocalGenerator;
            this.settingsService = settingsService;
            this.inventarioDeleter = inventarioDeleter;
            this.inventarioSearcher = inventarioSearcher;
            this.inventarioUpdater = inventarioUpdater;
            this.detalleInventarioLocalDeleter = detalleInventarioLocalDeleter;
        }

        public async Task<InventarioLocal> Create()
        {
            var setting = await settingsService.GetWithChildren();
            return inventarioFactory.Create(setting.CentroActivo, setting.UsuarioActivo.IdRed);
        }

        public async Task<InventarioLocal> Save(InventarioLocal inventario)
        {
            if (inventario.Id != 0)
            {
                await inventarioUpdater.Update(inventario);
            }
            else
            {
                await inventarioLocalGenerator.Generate(inventario);
            }
            return inventario;
        }

        public async Task Delete(InventarioLocal inventario)
        {
            await inventarioDeleter.Delete(inventario);
        }

        public async Task<IList<InventarioLocal>> GetAllProvisorios()
        {
            return await inventarioSearcher.GetAllProvisorios();
        }

        public async Task<InventarioLocal> GetInventarioById(int id)
        {
            return await inventarioSearcher.GetById(id);
        }

        public async Task SetToPendienteAprobacion(InventarioLocal inventario)
        {
            await inventarioUpdater.SetPendienteAprobacion(inventario);
            await settingsService.SetPendingToSync(true);
        }

        public DetalleInventarioLocal GetDetalleInventarioDuplicated(InventarioLocal inventario, DetalleInventarioLocal detalleInventario)
        {
            return inventarioSearcher.GetDetalleInventarioDuplicated(inventario, detalleInventario);
        }

        public async Task SetToRechazadoParcial(Inventario inventario, string comentario)
        {
            var inventrarioRechazdo = inventarioFactory.CreateRechazado(inventario, comentario);
            await inventarioLocalGenerator.Generate(inventrarioRechazdo);
            await settingsService.SetPendingToSync(true);
        }

        public async Task DeleteDetalleLocal(DetalleInventarioLocal detalleInventarioLocal)
        {
            await detalleInventarioLocalDeleter.Delete(detalleInventarioLocal);
        }
    }
}
