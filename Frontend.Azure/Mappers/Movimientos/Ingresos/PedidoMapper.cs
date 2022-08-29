using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers.Movimientos.Ingresos;
using Frontend.Business.Centros.Searchers;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Ingresos;
using Frontend.Business.Settings;
using Frontend.Commons.Commons;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers
{
    public class PedidoMapper
    {
        private readonly NotaDeEntregaMapper notaDeEntregaMapper;
        private readonly DetallePedidoMapper detallePedidoMapper;

        public PedidoMapper(DetallePedidoMapper detallePedidoMapper, CentroSearcher centroSearcher, NotaDeEntregaMapper notaDeEntregaMapper)
        {
            this.notaDeEntregaMapper = notaDeEntregaMapper;
            this.detallePedidoMapper = detallePedidoMapper;
        }

        public async Task<Pedido> MapFromDto(PedidoInputDto pedidoResponseDto, int centroId)
        {
            var pedido = new Pedido();
            pedido.DetallesPedido = new List<DetallePedido>();

            pedido.Id = pedidoResponseDto.Id;
            pedido.NumeroPedido = pedidoResponseDto.NumeroPedido;
            pedido.Estado = (EstadoMovimiento) pedidoResponseDto.EstadoMovimientoId;
            foreach (var item in pedidoResponseDto.DetallesPedido)
            {
                pedido.DetallesPedido.Add(await MapDetallePedido(item, pedidoResponseDto.Id, centroId));
            }

            return pedido;
        }

        public async Task<IList<Pedido>> MapFromDto(IList<PedidoInputDto> pedidoResponseDto, int centroId)
        {
            IList<Pedido> listPedidos = new List<Pedido>();

            foreach (var pedidoInputDto in pedidoResponseDto)
            {
                listPedidos.Add(await MapFromDto(pedidoInputDto, centroId));
            }

            return listPedidos;
        }

        private async Task<DetallePedido> MapDetallePedido(DetallePedidoInputDto detallePedidoResponseDto, int pedidoId, int centroId)
        {
            return await detallePedidoMapper.MapFromDto(detallePedidoResponseDto, pedidoId, centroId);
        }

        public async Task<PedidoRequestDto> MapToDto(Setting setting, params EstadoMovimiento[] estadoMoviminetoIds)
        {
            var pedidoRequestDto = new PedidoRequestDto();

            pedidoRequestDto.CentroId = setting.CentroActivoId;
            pedidoRequestDto.delta = ApplicationConstants.DefaultDateSync.ToString("O");
            pedidoRequestDto.EstadoMovimientoIds = estadoMoviminetoIds.Select(x => x.GetHashCode()).ToArray();

            return pedidoRequestDto;
        }

        public async Task<IList<PedidoOutputDto>> MapToPedidoOutputDto(IList<NotaDeEntrega> notasDeEntregas)
        {
            var pedidoOutputDto = new List<PedidoOutputDto>();
            foreach (var nota in notasDeEntregas)
            {
                var pedidoOutput = new PedidoOutputDto();
                var notaEntregaOutput = new NotaDeEntregaOutputDto();
                notaEntregaOutput = notaDeEntregaMapper.MapToDto(nota);
                pedidoOutput.Id = nota.PedidoId;
                pedidoOutput.NotasDeEntrega = new List<NotaDeEntregaOutputDto>();
                pedidoOutput.NotasDeEntrega.Add(notaEntregaOutput);
                pedidoOutputDto.Add(pedidoOutput);
            }

            return pedidoOutputDto;
        }
    }
}
