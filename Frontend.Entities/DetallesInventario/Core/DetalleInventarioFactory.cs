using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.DetallesStocksEspeciales;
using Frontend.Business.Inventarios;
using Frontend.Business.LecturaQRs;
using Frontend.Business.Materiales.Core;
using Frontend.Business.Stocks;
using System.Collections.Generic;
using System.Linq;

namespace Frontend.Business.DetallesInventario.Core
{
    public class DetalleInventarioFactory
    {
        private readonly MaterialFactory materialFactory;

        public DetalleInventarioFactory(MaterialFactory materialFactory)
        {
            this.materialFactory = materialFactory;
        }

        public DetalleInventario Create(Inventario inventario, List<Stock> stocksDisponibles, LecturaQR lecturaQR, ClaseDeValoracion claseDeValoracion, DetalleStockEspecial detalleStockEspecial)
        {
            var detalleInventario = new DetalleInventario();

            detalleInventario.Posicion = "0000";
            detalleInventario.EsContado = false;
            detalleInventario.InventarioId = inventario.Id;
            detalleInventario.Inventario = inventario;
            detalleInventario.StocksDisponibles = stocksDisponibles;
            detalleInventario.Ubicacion = string.IsNullOrWhiteSpace(lecturaQR.Ubicacion) ? string.Empty : lecturaQR.Ubicacion;
            detalleInventario.ClaseDeValoracionId = stocksDisponibles.Any(stock => stock.IdClaseDeValoracion == lecturaQR.LoteId.GetValueOrDefault()) ? lecturaQR.LoteId.GetValueOrDefault() : 0;
            detalleInventario.Lote = stocksDisponibles.Any(stock => stock.IdClaseDeValoracion == lecturaQR.LoteId.GetValueOrDefault()) ? claseDeValoracion : null;
            detalleInventario.DetalleStockEspecial = detalleStockEspecial;
            detalleInventario.DetalleStockEspecialId = detalleStockEspecial != null ? detalleStockEspecial.Id : 0;

            return detalleInventario;
        }

        public DetalleInventario Create()
        {
            return new DetalleInventario();
        }

        public IEnumerable<DetalleInventario> CreateCopy(IList<DetalleInventario> detalleInventarios)
        {
            foreach (var detalleInventario in detalleInventarios)
            {
                yield return CreateCopy(detalleInventario);
            }
        }

        public DetalleInventario CreateCopy(DetalleInventario detalleInventario)
        {
            var detalleInventarioNuevo = new DetalleInventario()
            {
                Cantidad = detalleInventario.Cantidad,
                CantidadContada = detalleInventario.CantidadContada,
                ClaseDeValoracionId = detalleInventario.ClaseDeValoracionId,
                DetalleStockEspecial = detalleInventario.DetalleStockEspecial,
                DetalleStockEspecialId = detalleInventario.DetalleStockEspecialId,
                EsContado = detalleInventario.EsContado,
                Lote = detalleInventario.Lote,
                Posicion = detalleInventario.Posicion,
                Stock = detalleInventario. Stock,
                StockId = detalleInventario.StockId,
                StocksDisponibles = detalleInventario.StocksDisponibles,
                TipoStockId = detalleInventario.TipoStockId,
                Ubicacion = detalleInventario.Ubicacion,
                UnidadAlmacen = detalleInventario.UnidadAlmacen,
                Uploaded = detalleInventario.Uploaded,
                Inventario = detalleInventario.Inventario,
                InventarioId =detalleInventario.InventarioId
            };

            return detalleInventarioNuevo;
        }
    }
}
