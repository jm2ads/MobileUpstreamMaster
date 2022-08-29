using Frontend.Azure.DTOs;
using Frontend.Business.Movimientos.Ingresos;
using System.Collections.Generic;
using System.Linq;

namespace Frontend.Azure.Mappers.Movimientos.Ingresos
{
    public class NotaDeEntregaMapper
    {

        private readonly DetalleNotaDeEntregaMapper detalleNotaDeEntregaMapper;

        public NotaDeEntregaMapper(DetalleNotaDeEntregaMapper detalleNotaDeEntregaMapper)
        {
            this.detalleNotaDeEntregaMapper = detalleNotaDeEntregaMapper;
        }

        public NotaDeEntregaOutputDto MapToDto(NotaDeEntrega notaDeEntrega)
        {
            var notaDeEntregaOutputDto = new NotaDeEntregaOutputDto();

            notaDeEntregaOutputDto.PedidoId = notaDeEntrega.PedidoId;
            notaDeEntregaOutputDto.NumeroPedido = notaDeEntrega.Pedido.NumeroPedido;
            notaDeEntregaOutputDto.CartaDePorte = notaDeEntrega.CartaDePorte;
            notaDeEntregaOutputDto.FechaContabilizacion = notaDeEntrega.FechaContabilizacion;
            notaDeEntregaOutputDto.FechaDocumento = notaDeEntrega.FechaDocumento;
            notaDeEntregaOutputDto.NumeroNotaDeEntrega = notaDeEntrega.NumeroNotaDeEntrega;
            notaDeEntregaOutputDto.TextoDeCabecera = notaDeEntrega.TextoDeCabecera;
            notaDeEntregaOutputDto.UsuarioCreacion = notaDeEntrega.UsuarioCreacion;
            notaDeEntregaOutputDto.DetallesNotaDeEntrega = detalleNotaDeEntregaMapper.MapToDto(notaDeEntrega.DetalleNotaDeEntrega).ToList();
            
            return notaDeEntregaOutputDto;
        }

        public IList<NotaDeEntregaOutputDto> MapToDto(IList<NotaDeEntrega> notasDeEntrega)
        {
            var notaReservaOutputDtoList = new List<NotaDeEntregaOutputDto>();
            foreach (var notaDeEntrega in notasDeEntrega)
            {
                notaReservaOutputDtoList.Add(MapToDto(notaDeEntrega));
            }
            return notaReservaOutputDtoList;
        }
    }
}
