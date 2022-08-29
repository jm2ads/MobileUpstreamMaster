using Frontend.Business.DetallesInventario;
using Frontend.Business.InventariosLocales;
using Frontend.Business.Stocks;
using System.Collections.Generic;

namespace Frontend.Business.DetallesInventarioLocal.Core
{
    public class DetalleInventarioLocalFactory
    {
        public DetalleInventarioLocalFactory()
        {
        }

        public DetalleInventarioLocal Create(InventarioLocal inventario, Stock stock)
        {
            return new DetalleInventarioLocal(inventario, stock);
        }

        public DetalleInventarioLocal Create()
        {
            return new DetalleInventarioLocal();
        }

        public IEnumerable<DetalleInventarioLocal> CreateCopy(IList<DetalleInventario> detalleInventarios)
        {
            foreach (var detalleInventario in detalleInventarios)
            {
                yield return CreateCopy(detalleInventario);
            }
        }

        public DetalleInventarioLocal CreateCopy(DetalleInventario detalleInventario)
        {
            var detalleInventarioNuevo = new DetalleInventarioLocal()
            {
                Cantidad = detalleInventario.Cantidad,
                CantidadContada = detalleInventario.CantidadContada,
                ClaseDeValoracionId = detalleInventario.ClaseDeValoracionId,
                DetalleStockEspecial = detalleInventario.DetalleStockEspecial,
                DetalleStockEspecialId = detalleInventario.DetalleStockEspecialId,
                EsContado = detalleInventario.EsContado,
                Lote = detalleInventario.Lote,
                Posicion = detalleInventario.Posicion,
                Stock = detalleInventario.Stock,
                StockId = detalleInventario.StockId,
                TipoStockId = detalleInventario.TipoStockId,
                Ubicacion = detalleInventario.Ubicacion,
                UnidadAlmacen = detalleInventario.UnidadAlmacen,
                Comentario = detalleInventario.Comentario,
                Uploaded = detalleInventario.Uploaded
            };

            return detalleInventarioNuevo;
        }
    }
}
