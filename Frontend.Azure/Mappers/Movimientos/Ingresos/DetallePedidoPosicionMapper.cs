using Frontend.Azure.DTOs;
using Frontend.Business.Movimientos.Ingresos;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers
{
    public class DetallePedidoPosicionMapper
    {
        public async Task<DetallePedidoPosicion> MapFromDto(DetallePedidoPosicionInputDto detallePedidoPosicionResponseDto, int detallePedidoId, int centroId)
        {
            var detallePedidoPosicion = new DetallePedidoPosicion();
            detallePedidoPosicion.Id = detallePedidoPosicionResponseDto.Id;
            detallePedidoPosicion.CantidadPendiente = detallePedidoPosicionResponseDto.CantidadPendiente;
            detallePedidoPosicion.ClaseMovimientoCodigo = detallePedidoPosicionResponseDto.ClaseDeMovimientoCodigo;
            detallePedidoPosicion.DetallePedidoId = detallePedidoId;
            detallePedidoPosicion.Ejercicio = detallePedidoPosicionResponseDto.Ejercicio;
            detallePedidoPosicion.DocumentoReferencia = detallePedidoPosicionResponseDto.DocumentoReferencia;
            detallePedidoPosicion.PosicionDocumento = detallePedidoPosicionResponseDto.PosicionDocumento;

            return detallePedidoPosicion;
        }
    }
}
