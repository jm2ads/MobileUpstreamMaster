using Frontend.Azure.DTOs;
using Frontend.Business.Movimientos.Ingresos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Azure.Mappers.Movimientos.Ingresos
{
    public class DetalleNotaDeEntregaPosicionMapper
    {
        public DetalleNotaDeEntregaPosicionOutputDto MapToDto(DetalleNotaDeEntregaPosicion detalleNotaDeEntregaPosicion)
        {
            var detalleNotaDeEntregaOutputDto = new DetalleNotaDeEntregaPosicionOutputDto();
            detalleNotaDeEntregaOutputDto.CantidadRecibida = detalleNotaDeEntregaPosicion.CantidadRecibida;
            detalleNotaDeEntregaOutputDto.ClaseDeMovimientoCodigo = detalleNotaDeEntregaPosicion.ClaseDeMovimientoCodigo;
            detalleNotaDeEntregaOutputDto.DocumentoReferencia = detalleNotaDeEntregaPosicion.DocumentoReferencia;
            detalleNotaDeEntregaOutputDto.CantidadPendiente = detalleNotaDeEntregaPosicion.DetallePedidoPosicion.CantidadPendiente;
            detalleNotaDeEntregaOutputDto.Ejercicio = detalleNotaDeEntregaPosicion.Ejercicio;
            detalleNotaDeEntregaOutputDto.PosicionDocumento = detalleNotaDeEntregaPosicion.PosicionDocumento;
            return detalleNotaDeEntregaOutputDto;
        }

        public IList<DetalleNotaDeEntregaPosicionOutputDto> MapToDto(IList<DetalleNotaDeEntregaPosicion> detallesNotaDeEntregaPosicion)
        {
            var detalleNotaDeEntregaPosicionOutputDtoList = new List<DetalleNotaDeEntregaPosicionOutputDto>();
            foreach (var detalleNotaDeEntregaPosicion in detallesNotaDeEntregaPosicion)
            {
                detalleNotaDeEntregaPosicionOutputDtoList.Add(MapToDto(detalleNotaDeEntregaPosicion));
            }
            return detalleNotaDeEntregaPosicionOutputDtoList;
        }
    }
}
