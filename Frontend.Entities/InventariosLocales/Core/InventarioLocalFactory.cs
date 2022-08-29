using Frontend.Business.Centros;
using Frontend.Business.DetallesInventarioLocal;
using Frontend.Business.DetallesInventarioLocal.Core;
using Frontend.Business.Inventarios;
using Frontend.Business.Synchronizer;
using Frontend.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frontend.Business.InventariosLocales.Core
{
    public class InventarioLocalFactory
    {
        private readonly DetalleInventarioLocalFactory detalleInventarioFactory;

        public InventarioLocalFactory(DetalleInventarioLocalFactory detalleInventarioFactory)
        {
            this.detalleInventarioFactory = detalleInventarioFactory;
        }

        public InventarioLocal Create(Centro centro, string usuarioActivo)
        {
            var inventario = new InventarioLocal()
            {
                DetallesInventario = new List<DetalleInventarioLocal>()
            };

            return inventario;
        }

        public InventarioLocal CreateRechazado(Inventario inventario, string comentario)
        {
            var inventarioNuevo = new InventarioLocal()
            {
                NumeroProvisorio = "-" + DateTime.Now.ToString("ddMMyyhhmmssff"),
                ProvisorioAnterior = inventario.NumeroProvisorio,
                EsProvisorio = true,
                Centro = inventario.Centro,
                IdCentro = inventario.Centro.Id,
                IdAlmacen = inventario.IdAlmacen,
                Almacen = inventario.Almacen,
                IdStockEspecial = inventario.IdStockEspecial,
                StockEspecial = inventario.StockEspecial,
                FechaCreacion = DateTime.Now,
                FechaRecuento = DateTime.Now,
                Estado = EstadoInventario.Rechazado,
                SyncState = SyncState.PendingToSync,
                ComentarioRechazo = comentario,
                UsuarioCreacion = inventario.UsuarioCreacion,
                UsuarioModificacion = inventario.UsuarioModificacion,
                DetallesInventario = detalleInventarioFactory.CreateCopy(inventario.DetallesInventario).ToList()
            };

            return inventarioNuevo;
        }
    }
}
