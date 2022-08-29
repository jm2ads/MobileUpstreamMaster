using Frontend.Azure.DTOs;
using Frontend.Business.ClasesDeValoracion.Searchers;
using Frontend.Business.DetallesInventario;
using Frontend.Business.DetallesInventario.Core;
using Frontend.Business.DetallesStocksEspeciales.Searchers;
using Frontend.Business.Stocks.Searchers;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers
{
    public class DetalleInventarioMapper
    {
        private readonly DetalleInventarioFactory detalleInventarioFactory;
        private readonly ClaseDeValoracionSearcher claseDeValoracionSearcher;
        private readonly StockSearcher stockSearcher;
        private readonly DetalleStockEspecialSearcher detalleStockEspecialSearcher;

        public DetalleInventarioMapper(DetalleInventarioFactory detalleInventarioFactory, ClaseDeValoracionSearcher claseDeValoracionSearcher,
            StockSearcher stockSearcher, DetalleStockEspecialSearcher detalleStockEspecialSearcher)
        {
            this.detalleInventarioFactory = detalleInventarioFactory;
            this.claseDeValoracionSearcher = claseDeValoracionSearcher;
            this.stockSearcher = stockSearcher;
            this.detalleStockEspecialSearcher = detalleStockEspecialSearcher;
        }

        public async Task<DetalleInventario> MapFromDto(DetalleInventarioInputDto detalleInventarioResponseDto, int inventarioId)
        {
            var detalleInventario = detalleInventarioFactory.Create();
            detalleInventario.InventarioId = inventarioId;
            detalleInventario.Id = detalleInventarioResponseDto.Id;
            detalleInventario.Cantidad = detalleInventarioResponseDto.Cantidad;
            detalleInventario.CantidadContada = detalleInventarioResponseDto.CantidadContada;
            detalleInventario.EsContado = detalleInventarioResponseDto.EsContado;
            detalleInventario.Posicion = detalleInventarioResponseDto.Posicion;
            detalleInventario.Ubicacion = detalleInventarioResponseDto.Ubicacion;
            detalleInventario.UnidadAlmacen = detalleInventarioResponseDto.UnidadAlmacenamiento;
            detalleInventario.Comentario = detalleInventarioResponseDto.Comentario;
            detalleInventario.TipoStockId = detalleInventarioResponseDto.TipoStockId;
            detalleInventario.StockId = detalleInventarioResponseDto.StockId;
            detalleInventario.ClaseDeValoracionId = detalleInventarioResponseDto.ClaseDeValoracionId;
            detalleInventario.DetalleStockEspecialId = detalleInventarioResponseDto.DetalleStockEspecialId;
            detalleInventario.HayConteoErroneo = detalleInventarioResponseDto.HayConteoErroneo;
            detalleInventario.EstadoConteo = detalleInventarioResponseDto.EstadoConteo;

            return detalleInventario;
        }

        public async Task<DetalleInventarioInputDto> MapToDto(DetalleInventario detalleInventario)
        {
            var detalleInventarioDto = new DetalleInventarioInputDto();

            detalleInventarioDto.Id = detalleInventario.Id;
            detalleInventarioDto.Cantidad = detalleInventario.CantidadContada;
            detalleInventarioDto.CantidadContada = detalleInventario.CantidadContada;
            detalleInventarioDto.EsContado = detalleInventario.EsContado;
            detalleInventarioDto.Posicion = detalleInventario.Posicion;
            detalleInventarioDto.TipoStockId = detalleInventario.TipoStockId;
            detalleInventarioDto.Ubicacion = detalleInventario.Ubicacion;
            detalleInventarioDto.Comentario = detalleInventario.Comentario;
            detalleInventarioDto.UnidadAlmacenamiento = detalleInventario.UnidadAlmacen;
            detalleInventarioDto.DetalleStockEspecialId = detalleInventario.DetalleStockEspecialId;
            detalleInventarioDto.StockId = detalleInventario.StockId;
            detalleInventarioDto.ClaseDeValoracionId = detalleInventario.ClaseDeValoracionId;
            detalleInventarioDto.HayConteoErroneo = detalleInventario.HayConteoErroneo;
            detalleInventarioDto.EstadoConteo = detalleInventario.EstadoConteo;

            return detalleInventarioDto;
        }
    }
}
