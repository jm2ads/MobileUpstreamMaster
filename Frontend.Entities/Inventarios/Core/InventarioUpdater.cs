using Frontend.Business.Commons;
using Frontend.Business.DetallesInventario.Core;
using Frontend.Business.Inventarios.Searchers;
using Frontend.Business.Inventarios.Validations;
using Frontend.Business.Settings.Searchers;
using Frontend.Business.Synchronizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Business.Inventarios.Core
{
    public class InventarioUpdater
    {
        private readonly IRepository<Inventario> repository;
        private readonly InventarioValidator inventarioValidator;
        private readonly DetalleInventarioSaver detalleInventarioSaver;
        private readonly InventarioSearcher inventarioSearcher;
        private readonly SettingSearcher settingsSearcher;

        public InventarioUpdater(IRepository<Inventario> repository, InventarioValidator inventarioValidator,
            DetalleInventarioSaver detalleInventarioSaver, InventarioSearcher inventarioSearcher, SettingSearcher settingsSearcher)
        {
            this.repository = repository;
            this.inventarioValidator = inventarioValidator;
            this.detalleInventarioSaver = detalleInventarioSaver;
            this.inventarioSearcher = inventarioSearcher;
            this.settingsSearcher = settingsSearcher;
        }

        public async Task UpdateWithChildren(Inventario inventario, SyncState syncState = SyncState.Updated)
        {
            var setting = await settingsSearcher.GetWithChildren();
            inventario.UsuarioModificacion = setting.UsuarioActivo.IdRed;
            inventario.FechaModificacion = DateTime.Now;
            inventario.SyncState = syncState;
            await repository.UpdateWithChildren(inventario);
            await detalleInventarioSaver.Save(inventario.DetallesInventario);
        }

        public async Task Update(Inventario inventario, SyncState syncState = SyncState.Updated)
        {
            var setting = await settingsSearcher.GetWithChildren();
            inventario.UsuarioModificacion = setting.UsuarioActivo.IdRed;
            inventario.FechaModificacion = DateTime.Now;
            inventario.SyncState = syncState;
            await repository.Update(inventario);
        }

        public async Task SetPendienteAprobacion(Inventario inventario)
        {
            inventarioValidator.ValidPendienteAprobacion(inventario);
            inventario.Estado = Frontend.Commons.Enums.EstadoInventario.PendienteAprobacion;
            await Update(inventario, SyncState.PendingToSync);
        }

        public async Task SetPendienteAprobacionSap(Inventario inventario)
        {
            inventarioValidator.ValidPendienteAprobacionSap(inventario);
            inventario.FechaRecuento = DateTime.Now;
            inventario.Estado = Frontend.Commons.Enums.EstadoInventario.PendienteAprobacionSap;
            await Update(inventario, SyncState.PendingToSync);
        }

        public async Task SetAprobado(Inventario inventario)
        {
            inventarioValidator.ValidAprobado(inventario);
            inventario.Estado = Frontend.Commons.Enums.EstadoInventario.Aprobado;
            await Update(inventario, SyncState.PendingToSync);
        }

        public async Task SetAprobadoParcial(Inventario inventario)
        {
            inventarioValidator.ValidAprobado(inventario);
            inventario.Estado = Frontend.Commons.Enums.EstadoInventario.Aprobado;
            await UpdateWithChildren(inventario, SyncState.PendingToSync);
        }

        public async Task SetAprobadoSap(Inventario inventario)
        {
            inventarioValidator.ValidAprobadoSap(inventario);
            inventario.Estado = Frontend.Commons.Enums.EstadoInventario.AprobadoSap;
            await Update(inventario, SyncState.PendingToSync);
        }

        public async Task SetAprobado(IList<Inventario> listInventario)
        {
            foreach (var inventario in listInventario)
            {
                await SetAprobado(inventario);
            }
        }

        public async Task SetAprobadoSap(IList<Inventario> listInventario)
        {
            foreach (var inventario in listInventario)
            {
                await SetAprobadoSap(inventario);
            }
        }

        public async Task SetComentario(IList<Inventario> listInventario, string comentario)
        {
            var date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            foreach (var inventario in listInventario)
            {
                inventario.ComentarioRechazo = String.Format("{1}[{0}]{1}{2}{1}{3}{1}", date, Environment.NewLine, comentario, inventario.ComentarioRechazo);
                await Update(inventario);
            }
        }
        public async Task SetComentario(Inventario inventario, string comentario)
        {
            var date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            inventario.ComentarioRechazo = String.Format("{1}[{0}]{1}{2}{1}{3}{1}", date, Environment.NewLine, comentario, inventario.ComentarioRechazo);
            await Update(inventario);
        }

        public async Task SetRechazado(Inventario inventario)
        {
            inventarioValidator.ValidRechazado(inventario);
            inventario.Estado = Frontend.Commons.Enums.EstadoInventario.Rechazado;
            await Update(inventario, SyncState.PendingToSync);
        }

        public async Task SetRechazado(IList<Inventario> listInventario)
        {
            foreach (var inventario in listInventario)
            {
                await SetRechazado(inventario);
            }
        }

        public async Task SetRechazadoSap(Inventario inventario)
        {
            inventarioValidator.ValidRechazadoSap(inventario);
            inventario.Estado = Frontend.Commons.Enums.EstadoInventario.RechazadoSAP;
            await UpdateWithChildren(inventario, SyncState.PendingToSync);
        }

        public async Task SetRechazadoSap(IList<Inventario> listInventario)
        {
            foreach (var inventario in listInventario)
            {
                await SetRechazadoSap(inventario);
            }
        }
    }
}
