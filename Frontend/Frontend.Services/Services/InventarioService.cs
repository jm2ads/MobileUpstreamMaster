using Frontend.Business.DetallesInventario;
using Frontend.Business.Inventarios;
using Frontend.Business.Inventarios.Core;
using Frontend.Business.Inventarios.Searchers;
using Frontend.Business.Inventarios.Validations;
using Frontend.Business.Materiales;
using Frontend.Commons.Enums;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class InventarioService : IInventarioService
    {
        private readonly InventarioFactory inventarioFactory;
        private readonly InventarioGenerator inventarioGenerator;
        private readonly InventarioUpdater inventarioUpdater;
        private readonly InventarioValidator inventarioValidator;
        private readonly InventarioSearcher inventarioSearcher;
        private readonly InventarioDeleter inventarioDeleter;
        private readonly ISettingsService settingsService;
        private readonly InventarioLogGenerator inventarioLogGenerator;
        private readonly InventarioLogSearcher inventarioLogSearcher;
        private readonly InventarioLogDeleter inventarioLogDeleter;

        public InventarioService(InventarioFactory inventarioFactory, InventarioGenerator inventarioGenerator, InventarioUpdater inventarioUpdater, InventarioValidator inventarioValidator,
            InventarioSearcher inventarioSearcher, InventarioDeleter inventarioDeleter, ISettingsService settingsService, InventarioLogGenerator inventarioLogGenerator,
            InventarioLogSearcher inventarioLogSearcher, InventarioLogDeleter inventarioLogDeleter)
        {
            this.inventarioFactory = inventarioFactory;
            this.inventarioGenerator = inventarioGenerator;
            this.inventarioUpdater = inventarioUpdater;
            this.inventarioValidator = inventarioValidator;
            this.inventarioSearcher = inventarioSearcher;
            this.inventarioDeleter = inventarioDeleter;
            this.settingsService = settingsService;
            this.inventarioLogGenerator = inventarioLogGenerator;
            this.inventarioLogSearcher = inventarioLogSearcher;
            this.inventarioLogDeleter = inventarioLogDeleter;
        }

        public async Task<Inventario> Create()
        {
            var setting = await settingsService.GetWithChildren();
            return inventarioFactory.Create(setting.CentroActivo, setting.UsuarioActivo.IdRed);
        }

        public Inventario CreateRechazado(Inventario inventario)
        {
            return inventarioFactory.CreateRechazado(inventario);
        }

        public async Task<Inventario> Generate(Inventario inventario)
        {
            return await inventarioGenerator.Generate(inventario);
        }

        public async Task<IList<InventarioLog>> Generate(IList<InventarioLog> listInventarioLog)
        {
            return await inventarioLogGenerator.Generate(listInventarioLog);
        }

        public async Task<Inventario> Save(Inventario inventario)
        {
            if (inventario.Id != 0)
            {
                await inventarioUpdater.UpdateWithChildren(inventario);
            }
            else
            {
                await inventarioGenerator.Generate(inventario);
            }
            return inventario;
        }

        public async Task UpdateWithChildren(Inventario inventario)
        {
            await inventarioUpdater.UpdateWithChildren(inventario);
        }

        public async Task Delete(Inventario inventario)
        {
            await inventarioDeleter.Delete(inventario);
        }

        public async Task DeleteAll()
        {
            await inventarioDeleter.DeleteAll();
        }

        public bool IsDuplicatedDetalleInventario(Inventario inventario, DetalleInventario detalleInventario)
        {
            return inventarioValidator.IsDuplicatedDetalleInventario(inventario, detalleInventario);
        }

        public DetalleInventario GetDetalleInventarioDuplicated(Inventario inventario, DetalleInventario detalleInventario)
        {
            return inventarioSearcher.GetDetalleInventarioDuplicated(inventario, detalleInventario);
        }

        public async Task<IList<Inventario>> GetAllProvisorios()
        {
            return await inventarioSearcher.GetAllByEstado(EstadoInventario.Provisorio);
        }

        public async Task<IList<Inventario>> GetAllRechazados()
        {
            return await inventarioSearcher.GetAllByEstado(EstadoInventario.Rechazado);
        }

        public async Task<IList<Inventario>> GetAllPendienteAprobacion()
        {
            return await inventarioSearcher.GetAllByEstado(EstadoInventario.PendienteAprobacion, EstadoInventario.RechazadoSAP);
        }

        public async Task<IList<Inventario>> GetAllPendienteAprobacionWithChildren()
        {
            return await inventarioSearcher.GetAllByEstadoWithChildren(EstadoInventario.PendienteAprobacion, EstadoInventario.RechazadoSAP);
        }

        public async Task<IList<Inventario>> GetAllPendienteAprobacionSap()
        {
            return await inventarioSearcher.GetAllByEstado(EstadoInventario.PendienteAprobacionSap);
        }

        public async Task<IList<Inventario>> GetAllPendienteAprobacionSapWithChildren()
        {
            return await inventarioSearcher.GetAllByEstadoWithChildren(EstadoInventario.PendienteAprobacion, EstadoInventario.RechazadoSAP);
        }

        public async Task<IList<Inventario>> GetAllSap()
        {
            return await inventarioSearcher.GetAllByEstado(EstadoInventario.Recuento);
        }

        public async Task<IList<Inventario>> GetById(IList<Inventario> inventarios)
        {
            return await inventarioSearcher.GetAllByIds(inventarios.Select(x => x.Id).ToList());
        }       

        public async Task<Inventario> GetInventarioBy(string codigo, EstadoInventario estadoInventario)
        {
            return await inventarioSearcher.GetBy(codigo, estadoInventario);
        }

        public async Task<Inventario> GetInventarioById(int id)
        {
            return await inventarioSearcher.GetById(id);
        }

        public bool IsValidToFinish(Inventario inventario)
        {
            return inventarioValidator.IsValidToFinish(inventario);
        }

        public async Task SetToPendienteAprobacion(Inventario inventario)
        {
            await inventarioUpdater.SetPendienteAprobacion(inventario);
            await settingsService.SetPendingToSync(true);
        }

        public async Task SetToPendienteAprobacionSap(Inventario inventario)
        {
            await inventarioUpdater.SetPendienteAprobacionSap(inventario);
            await settingsService.SetPendingToSync(true);
        }

        public async Task SetToAprobado(Inventario inventario)
        {
            await inventarioUpdater.SetAprobado(inventario);
            await settingsService.SetPendingToSync(true);
        }

        public async Task SetToAprobadoParcial(Inventario inventario)
        {
            await inventarioUpdater.SetAprobadoParcial(inventario);
            await settingsService.SetPendingToSync(true);
        }

        public async Task SetToAprobado(IList<Inventario> listInventario)
        {
            await inventarioUpdater.SetAprobado(listInventario);
            await settingsService.SetPendingToSync(true);
        }

        public async Task SetToRechazado(Inventario inventario)
        {
            await inventarioUpdater.SetRechazado(inventario);
            await settingsService.SetPendingToSync(true);
        }

        public async Task SetToRechazado(IList<Inventario> listInventario)
        {
            await inventarioUpdater.SetRechazado(listInventario);
            await settingsService.SetPendingToSync(true);
        }

        public async Task SetToRechazadoParcial(Inventario inventario)
        {
            var inventrarioRechazdo = inventarioFactory.CreateRechazado(inventario);
            await inventarioGenerator.Generate(inventrarioRechazdo);
            await settingsService.SetPendingToSync(true);
        }

        public async Task SetToAprobadoSap(Inventario inventario)
        {
            await inventarioUpdater.SetAprobadoSap(inventario);
            await settingsService.SetPendingToSync(true);
        }

        public async Task SetToAprobadoSap(IList<Inventario> listInventario)
        {
            await inventarioUpdater.SetAprobadoSap(listInventario);
            await settingsService.SetPendingToSync(true);
        }

        public async Task SetComentario(IList<Inventario> listInventario, string comentario)
        {
            await inventarioUpdater.SetComentario(listInventario, comentario);
            await settingsService.SetPendingToSync(true);
        }
        
        public Task SetComentario(Inventario inventario, string comentario)
        {
            return inventarioUpdater.SetComentario(inventario, comentario);
        }

        public async Task SetToRechazadoSap(Inventario inventario)
        {
            await inventarioUpdater.SetRechazadoSap(inventario);
            await settingsService.SetPendingToSync(true);
        }

        public async Task SetToRechazadoSap(IList<Inventario> listInventario)
        {
            await inventarioUpdater.SetRechazadoSap(listInventario);
            await settingsService.SetPendingToSync(true);
        }

        public async Task<IList<string>> GetAllCodigoAutocomplete()
        {
            return await inventarioSearcher.GetAllCodigoAutocomplete();
        }

        public async Task<IList<InventarioLog>> GetAllInventarioLog()
        {
            return await inventarioLogSearcher.GetAll();
        }

        public async Task<IList<InventarioLog>> GetAllInventarioLogError()
        {
            return await inventarioLogSearcher.GetAllError();
        }

        public async Task<IList<Material>> GetAllMaterialAutocompleteRecuento()
        {
            return await inventarioSearcher.GetAllMaterialRecuento();
        }

        public async Task DeleteLog(IList<int> listIdRemoto)
        {
            await inventarioLogDeleter.Delete(listIdRemoto);
        }

        public async Task DeleteLogOlderThan(int cantidadDias)
        {
            await inventarioLogDeleter.DeleteOlderThan(cantidadDias);
        }
    }
}
