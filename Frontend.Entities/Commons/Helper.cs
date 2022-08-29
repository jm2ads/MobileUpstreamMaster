using Frontend.Business.DetallesInventario;
using Frontend.Business.DetallesInventarioLocal;
using Frontend.Business.Inventarios;
using Frontend.Business.InventariosLocales;
using System.Collections.Generic;

namespace Frontend.Business.Commons
{
    public static class Helper
    {
        public static Inventario MapToInventario(InventarioLocal inventarioLocal)
        {
            var inventario = new Inventario();
            inventario.DetallesInventario = new List<DetalleInventario>();
            inventario.inventarioLocalId = inventarioLocal.Id;
            inventario.NumeroProvisorio = inventarioLocal.NumeroProvisorio;
            inventario.NumeroSAP = inventarioLocal.NumeroSAP;
            inventario.EsProvisorio = inventarioLocal.EsProvisorio;
            inventario.FechaCreacion = inventarioLocal.FechaCreacion;
            inventario.FechaRecuento = inventarioLocal.FechaRecuento;
            inventario.UsuarioCreacion = inventarioLocal.UsuarioCreacion;
            inventario.UsuarioModificacion = inventarioLocal.UsuarioModificacion;
            inventario.IdAlmacen = inventarioLocal.IdAlmacen;
            inventario.Almacen = inventarioLocal.Almacen;
            inventario.IdCentro = inventarioLocal.IdCentro;
            inventario.Centro = inventarioLocal.Centro;
            inventario.Estado = inventarioLocal.Estado;
            inventario.IdStockEspecial = inventarioLocal.IdStockEspecial;
            inventario.StockEspecial = inventarioLocal.StockEspecial;

            foreach (var detalle in inventarioLocal.DetallesInventario)
            {
                var nuevoDetalle = new DetalleInventario();
                nuevoDetalle.Id = detalle.Id;
                nuevoDetalle.Cantidad = detalle.Cantidad;
                nuevoDetalle.CantidadContada = detalle.CantidadContada;
                nuevoDetalle.EsContado = detalle.EsContado;
                nuevoDetalle.Posicion = detalle.Posicion;
                nuevoDetalle.TipoStockId = detalle.TipoStockId;
                nuevoDetalle.Ubicacion = detalle.Ubicacion;
                nuevoDetalle.UnidadAlmacen = detalle.UnidadAlmacen;
                nuevoDetalle.InventarioId = inventario.Id;
                nuevoDetalle.Inventario = inventario;
                nuevoDetalle.StockId = detalle.StockId;
                nuevoDetalle.Stock = detalle.Stock;
                nuevoDetalle.ClaseDeValoracionId = detalle.ClaseDeValoracionId;
                nuevoDetalle.Lote = detalle.Lote;
                nuevoDetalle.DetalleStockEspecialId = detalle.DetalleStockEspecialId;
                nuevoDetalle.DetalleStockEspecial = detalle.DetalleStockEspecial;
                nuevoDetalle.EstadoConteo = detalle.EstadoConteo;
                //nuevoDetalle.StocksDisponibles = detalle.StocksDisponibles;

                inventario.DetallesInventario.Add(nuevoDetalle);
            }

            return inventario;
        }

        public static InventarioLocal MapToInventarioLocal(Inventario inventario)
        {
            var inventarioLocal = new InventarioLocal();
            inventarioLocal.DetallesInventario = new List<DetalleInventarioLocal>();

            inventarioLocal.NumeroProvisorio = inventario.NumeroProvisorio;
            inventarioLocal.NumeroSAP = inventario.NumeroSAP;
            inventarioLocal.EsProvisorio = inventario.EsProvisorio;
            inventarioLocal.FechaCreacion = inventario.FechaCreacion;
            inventarioLocal.FechaRecuento = inventario.FechaRecuento;
            inventarioLocal.UsuarioCreacion = inventario.UsuarioCreacion;
            inventarioLocal.UsuarioModificacion = inventario.UsuarioModificacion;
            inventarioLocal.IdAlmacen = inventario.IdAlmacen;
            inventarioLocal.Almacen = inventario.Almacen;
            inventarioLocal.IdCentro = inventario.IdCentro;
            inventarioLocal.Centro = inventario.Centro;
            inventarioLocal.Estado = inventario.Estado;
            inventarioLocal.IdStockEspecial = inventario.IdStockEspecial;
            inventarioLocal.StockEspecial = inventario.StockEspecial;

            foreach (var detalle in inventario.DetallesInventario)
            {
                var nuevoDetalle = new DetalleInventarioLocal();
                nuevoDetalle.Id = detalle.Id;
                nuevoDetalle.Cantidad = detalle.Cantidad;
                nuevoDetalle.CantidadContada = detalle.CantidadContada;
                nuevoDetalle.EsContado = detalle.EsContado;
                nuevoDetalle.Posicion = detalle.Posicion;
                nuevoDetalle.TipoStockId = detalle.TipoStockId;
                nuevoDetalle.Ubicacion = detalle.Ubicacion;
                nuevoDetalle.UnidadAlmacen = detalle.UnidadAlmacen;
                nuevoDetalle.InventarioId = inventarioLocal.Id;
                nuevoDetalle.Inventario = inventarioLocal;
                nuevoDetalle.StockId = detalle.StockId;
                nuevoDetalle.Stock = detalle.Stock;
                nuevoDetalle.ClaseDeValoracionId = detalle.ClaseDeValoracionId;
                nuevoDetalle.Lote = detalle.Lote;
                nuevoDetalle.DetalleStockEspecialId = detalle.DetalleStockEspecialId;
                nuevoDetalle.DetalleStockEspecial = detalle.DetalleStockEspecial;
                nuevoDetalle.EstadoConteo = detalle.EstadoConteo;
                //nuevoDetalle.StocksDisponibles = detalle.StocksDisponibles;

                inventarioLocal.DetallesInventario.Add(nuevoDetalle);
            }

            return inventarioLocal;
        }

        public static DetalleInventario MapToDetalleInventario(DetalleInventarioLocal detalleInventarioLocal)
        {
            var nuevoDetalle = new DetalleInventario();
            nuevoDetalle.Id = detalleInventarioLocal.Id;
            nuevoDetalle.Cantidad = detalleInventarioLocal.Cantidad;
            nuevoDetalle.CantidadContada = detalleInventarioLocal.CantidadContada;
            nuevoDetalle.EsContado = detalleInventarioLocal.EsContado;
            nuevoDetalle.Posicion = detalleInventarioLocal.Posicion;
            nuevoDetalle.TipoStockId = detalleInventarioLocal.TipoStockId;
            nuevoDetalle.Ubicacion = detalleInventarioLocal.Ubicacion;
            nuevoDetalle.UnidadAlmacen = detalleInventarioLocal.UnidadAlmacen;
            nuevoDetalle.StockId = detalleInventarioLocal.StockId;
            nuevoDetalle.Stock = detalleInventarioLocal.Stock;
            nuevoDetalle.ClaseDeValoracionId = detalleInventarioLocal.ClaseDeValoracionId;
            nuevoDetalle.Lote = detalleInventarioLocal.Lote;
            nuevoDetalle.DetalleStockEspecialId = detalleInventarioLocal.DetalleStockEspecialId;
            nuevoDetalle.DetalleStockEspecial = detalleInventarioLocal.DetalleStockEspecial;
            //nuevoDetalle.StocksDisponibles = detalleInventarioLocal.StocksDisponibles;
            
            return nuevoDetalle;
        }

        public static DetalleInventarioLocal MapToDetalleInventarioLocal(DetalleInventario detalleInventario)
        {
            var nuevoDetalle = new DetalleInventarioLocal();
            nuevoDetalle.Id = detalleInventario.Id;
            nuevoDetalle.Cantidad = detalleInventario.Cantidad;
            nuevoDetalle.CantidadContada = detalleInventario.CantidadContada;
            nuevoDetalle.EsContado = detalleInventario.EsContado;
            nuevoDetalle.Posicion = detalleInventario.Posicion;
            nuevoDetalle.TipoStockId = detalleInventario.TipoStockId;
            nuevoDetalle.Ubicacion = detalleInventario.Ubicacion;
            nuevoDetalle.UnidadAlmacen = detalleInventario.UnidadAlmacen;
            nuevoDetalle.StockId = detalleInventario.StockId;
            nuevoDetalle.Stock = detalleInventario.Stock;
            nuevoDetalle.ClaseDeValoracionId = detalleInventario.ClaseDeValoracionId;
            nuevoDetalle.Lote = detalleInventario.Lote;
            nuevoDetalle.DetalleStockEspecialId = detalleInventario.DetalleStockEspecialId;
            nuevoDetalle.DetalleStockEspecial = detalleInventario.DetalleStockEspecial;
            //nuevoDetalle.StocksDisponibles = detalleInventario.StocksDisponibles;

            return nuevoDetalle;
        }
    }
}
