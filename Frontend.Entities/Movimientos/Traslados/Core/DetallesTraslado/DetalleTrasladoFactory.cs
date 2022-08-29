using Frontend.Business.Inventarios.StockEspeciales.Searchers;
using Frontend.Business.Stocks;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Traslados.Core
{
    public class DetalleTrasladoFactory
    {
        private readonly StockEspecialSearcher stockEspecialSearch;

        public DetalleTrasladoFactory(StockEspecialSearcher stockEspecialSearch)
        {
            this.stockEspecialSearch = stockEspecialSearch;
        }


        public async Task<DetalleTraslado> Create(Traslado traslado, Stock stock)
        {
            var detalleTraslado = new DetalleTraslado()
            {
                CodigoMaterial = stock.Material.Codigo,
                Cantidad = stock.CantidadAlmacen,
                CentroId = stock.IdCentro,
                Centro = stock.Centro,
                ClaseDeValoracionId = stock.IdClaseDeValoracion,
                ClaseDeValoracion = stock.ClaseDeValoracion,
                StockId = stock.Id,
                Stock = stock,
                TrasladoId = traslado.Id,
                Traslado = traslado,
                Posicion = CalcularPosicion(traslado)
            };

            if (traslado.ClaseDeMovimientoCodigo != ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_541])
            {
                detalleTraslado.AlmacenId = stock.IdAlmacen.GetValueOrDefault();
                detalleTraslado.Almacen = stock.Almacen;
                detalleTraslado.StockEspecialId = stock.DetalleStockEspecial.IdStockEspecial;
                detalleTraslado.StockEspecial = stock.DetalleStockEspecial.StockEspecial;
            }
            else
            {
                var stockEspecial = await stockEspecialSearch.GetByCodigo("O");
                detalleTraslado.StockEspecialId = stockEspecial.Id;
                detalleTraslado.StockEspecial = stockEspecial;
            }

            return detalleTraslado;
        }

        private int CalcularPosicion(Traslado traslado)
        {
            var detalle = traslado.DetallesTraslado.LastOrDefault();
            return detalle != null ?  detalle.Posicion + 10 :  10; 
        }
    }
}
