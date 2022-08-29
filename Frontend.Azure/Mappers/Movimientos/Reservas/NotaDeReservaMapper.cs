using Frontend.Azure.DTOs.Movimientos.Reservas;
using Frontend.Business.Movimientos.NotasDeReservas;
using System;
using System.Collections.Generic;

namespace Frontend.Azure.Mappers.Movimientos.Reservas
{
    public class NotaDeReservaMapper
    {
        private readonly DetalleNotaDeReservaMapper detalleNotaDeReservaMapper;

        public NotaDeReservaMapper(DetalleNotaDeReservaMapper detalleNotaDeReservaMapper)
        {
            this.detalleNotaDeReservaMapper = detalleNotaDeReservaMapper;
        }

        public NotaDeReservaOutputDto MapToDto(NotaDeReserva notaDeReserva)
        {
            var notaDeReservaOutputDto = new NotaDeReservaOutputDto();
            notaDeReservaOutputDto.ReservaId = notaDeReserva.ReservaId;
            notaDeReservaOutputDto.TipoDeReserva = Enum.GetName(notaDeReserva.Reserva.TipoReserva.GetType(), notaDeReserva.Reserva.TipoReserva);
            notaDeReservaOutputDto.TextoDeCabecera = notaDeReserva.TextoCabecera;
            notaDeReservaOutputDto.UsuarioReserva = notaDeReserva.UsuarioReserva;
            notaDeReservaOutputDto.FechaDocumento = notaDeReserva.FechaDocumentacion;
            notaDeReservaOutputDto.FechaContabilizacion = notaDeReserva.FechaContabilizacion;
            notaDeReservaOutputDto.NumeroReserva = notaDeReserva.Reserva.Numero;
            notaDeReservaOutputDto.ClaseDeMovimientoCodigo = notaDeReserva.Reserva.ClaseDeMovimiento;
            notaDeReservaOutputDto.Imputacion = notaDeReserva.Reserva.Imputacion;
            notaDeReservaOutputDto.NumeroReserva = notaDeReserva.Reserva.Numero;
            notaDeReservaOutputDto.DetallesNotasDeReservas = detalleNotaDeReservaMapper.MapToDto(notaDeReserva.DetallesNotasDeReservas);

            return notaDeReservaOutputDto;
        }

        public IList<NotaDeReservaOutputDto> MapToDto(IList<NotaDeReserva> notasDeReservas)
        {
            var notaReservaOutputDtoList = new List<NotaDeReservaOutputDto>();
            foreach (var notaDeReserva in notasDeReservas)
            {
                notaReservaOutputDtoList.Add(MapToDto(notaDeReserva));
            }
            return notaReservaOutputDtoList;
        }
    }
}
