using Frontend.Azure.DTOs.DetallesInventarios;
using Frontend.Business.DetallesInventarioLocal;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers.DetallesInventarioLocal
{
    public class DetalleInventarioLocalMapper
    {
        public async Task<DetalleCrearInventarioInputDto> MapToDto(DetalleInventarioLocal detalleInventario, int stockEspecialId)
        {
            var detalleInventarioDto = new DetalleCrearInventarioInputDto();

            detalleInventarioDto.Cantidad = detalleInventario.CantidadContada;
            detalleInventarioDto.CantidadContada = detalleInventario.CantidadContada;
            detalleInventarioDto.EsContado = detalleInventario.EsContado;
            detalleInventarioDto.Posicion = detalleInventario.Posicion;
            detalleInventarioDto.TipoStockId = detalleInventario.TipoStockId; 
            detalleInventarioDto.Ubicacion = detalleInventario.Ubicacion;
            detalleInventarioDto.Comentario = detalleInventario.Comentario;
            detalleInventarioDto.UnidadAlmacenamiento = detalleInventario.UnidadAlmacen;
            detalleInventarioDto.StockId = detalleInventario.StockId;
            detalleInventarioDto.ClaseDeValoracionId = detalleInventario.ClaseDeValoracionId;
            detalleInventarioDto.StockEspecialId = stockEspecialId;
            detalleInventarioDto.DetalleStockEspecialId = detalleInventario.DetalleStockEspecialId;
            detalleInventarioDto.EstadoConteo = detalleInventario.EstadoConteo;

            return detalleInventarioDto;
        }
    }
}
