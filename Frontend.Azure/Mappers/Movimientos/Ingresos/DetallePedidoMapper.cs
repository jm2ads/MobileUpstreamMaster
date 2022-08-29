using Frontend.Azure.DTOs;
using Frontend.Business.Almacenes.Searchers;
using Frontend.Business.ClasesDeValoracion.Searchers;
using Frontend.Business.Materiales.Searchers;
using Frontend.Business.Movimientos.Ingresos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers
{
    public class DetallePedidoMapper
    {
        private readonly DetallePedidoPosicionMapper detallePedidoPosicionMapper;
        public DetallePedidoMapper(DetallePedidoPosicionMapper detallePedidoPosicionMapper)
        {
            this.detallePedidoPosicionMapper = detallePedidoPosicionMapper;
        }

        public async Task<DetallePedido> MapFromDto(DetallePedidoInputDto detallePedidoResponseDto, int pedidoId, int centroId)
        {
            var detallePedido = new DetallePedido();
            detallePedido.CentroId = centroId;
            detallePedido.Id = detallePedidoResponseDto.Id;
            detallePedido.PedidoId = pedidoId;
            detallePedido.Unidad = detallePedidoResponseDto.Unidad;
            detallePedido.Posicion = detallePedidoResponseDto.Posicion;
            detallePedido.Tolerancia = detallePedidoResponseDto.Tolerancia;
            detallePedido.MaterialId = detallePedidoResponseDto.MaterialId;
            detallePedido.TipoStockId = detallePedidoResponseDto.TipoDeStock == null ? string.Empty : detallePedidoResponseDto.TipoDeStock;
            detallePedido.StockEspecialId = detallePedidoResponseDto.StockEspecialId;
            detallePedido.ClaseDeValoracionId = detallePedidoResponseDto.ClaseDeValoracionId.GetValueOrDefault();
            detallePedido.AlmacenId = detallePedidoResponseDto.AlmacenId.GetValueOrDefault();
            detallePedido.DetallesPedidoPosicion = new List<DetallePedidoPosicion>();
            foreach (var item in detallePedidoResponseDto.DetallesPedidoPosicion)
            {
                detallePedido.DetallesPedidoPosicion.Add(await MapDetallePedidoPosicion(item, detallePedido.Id, centroId));
            }
            return detallePedido;
        }

        private async Task<DetallePedidoPosicion> MapDetallePedidoPosicion(DetallePedidoPosicionInputDto detallePedidoResponseDto, int detallePedidoId, int centroId)
        {
            return await detallePedidoPosicionMapper.MapFromDto(detallePedidoResponseDto, detallePedidoId, centroId);
        }
    }
}
