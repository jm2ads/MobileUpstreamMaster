using Frontend.Business.Centros;
using Frontend.Business.DetallesInventario;
using Frontend.Business.DetallesInventario.Core;
using Frontend.Business.Settings.Searchers;
using Frontend.Business.Synchronizer;
using Frontend.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frontend.Business.Inventarios.Core
{
    public class InventarioFactory
    {
        private readonly DetalleInventarioFactory detalleInventarioFactory;
        private readonly SettingSearcher settingSearcher;

        public InventarioFactory(DetalleInventarioFactory detalleInventarioFactory)
        {
            this.detalleInventarioFactory = detalleInventarioFactory;
        }
        public Inventario Create(Centro centro, string usuarioActivo)
        {
            var inventario = new Inventario()
            {
                NumeroProvisorio = "-" + DateTime.Now.ToString("ddMMyyhhmmssff"),
                EsProvisorio = true,
                Centro = centro,
                IdCentro = centro.Id,
                FechaCreacion = DateTime.Now,
                FechaRecuento = DateTime.Now,
                Estado = EstadoInventario.Provisorio,
                SyncState = SyncState.New,
                UsuarioCreacion = usuarioActivo,
                UsuarioModificacion = usuarioActivo,
                DetallesInventario = new List<DetalleInventario>()
            };
            
            return inventario;
        }


        public Inventario CreateRechazado(Inventario inventario)
        {
            var inventarioNuevo = new Inventario()
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
                Ejercicio = inventario.Ejercicio,
                FechaCreacion = DateTime.Now,
                FechaRecuento = DateTime.Now,
                Estado = EstadoInventario.Rechazado,
                SyncState = SyncState.Updated,
                ComentarioRechazo = inventario.ComentarioRechazo,
                UsuarioCreacion = inventario.UsuarioCreacion,
                UsuarioModificacion = inventario.UsuarioModificacion,
                DetallesInventario = detalleInventarioFactory.CreateCopy(inventario.DetallesInventario).ToList()
            };

            return inventarioNuevo;
        }
    }
}
