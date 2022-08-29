using Frontend.Azure.DTOs.Movimientos.Reservas;
using Frontend.Business.Movimientos.NotasDeReservas;
using System.Collections.Generic;

namespace Frontend.Azure.Mappers.Movimientos.Reservas
{
    public class DetalleNotaDeReservaMapper
    {
        public DetalleNotaDeReservaOutputDto MapToDto(DetalleNotaDeReserva detalleNotaDeReserva)
        {
            var detalleNotaDeReservaOutputDto = new DetalleNotaDeReservaOutputDto();
            detalleNotaDeReservaOutputDto.CantidadIngresada = detalleNotaDeReserva.CantidadIngresada;
            detalleNotaDeReservaOutputDto.TextoPosicion = detalleNotaDeReserva.TextoPosicion;
            detalleNotaDeReservaOutputDto.PuestoDeDescarga = detalleNotaDeReserva.PuestoDeDescarga;
            detalleNotaDeReservaOutputDto.DestinatarioDeMercancia = detalleNotaDeReserva.Destinatario;
            detalleNotaDeReservaOutputDto.EsEntregaFinal = detalleNotaDeReserva.EsEntregaFinal;
            detalleNotaDeReservaOutputDto.DetalleReservaId = detalleNotaDeReserva.DetalleReservaId;
            detalleNotaDeReservaOutputDto.TipoStock = detalleNotaDeReserva.TipoStockCodigo;
            detalleNotaDeReservaOutputDto.StockEspecialId = detalleNotaDeReserva.StockEspecialId;
            detalleNotaDeReservaOutputDto.AlmacenId = detalleNotaDeReserva.AlmacenId;
            detalleNotaDeReservaOutputDto.ClaseDeValoracionId = detalleNotaDeReserva.ClaseDeValoracionId;
            detalleNotaDeReservaOutputDto.Posicion = detalleNotaDeReserva.DetalleReserva.Posicion;
            detalleNotaDeReservaOutputDto.Unidad = detalleNotaDeReserva.DetalleReserva.Unidad;
            detalleNotaDeReservaOutputDto.CantidadReserva = detalleNotaDeReserva.DetalleReserva.CantidadReserva;
            detalleNotaDeReservaOutputDto.CentroId = detalleNotaDeReserva.DetalleReserva.CentroId;
            detalleNotaDeReservaOutputDto.MaterialId = detalleNotaDeReserva.DetalleReserva.MaterialId;
            return detalleNotaDeReservaOutputDto;
        }

        public IList<DetalleNotaDeReservaOutputDto> MapToDto(IList<DetalleNotaDeReserva> detallesNotasDeReservas)
        {
            var detalleNotaReservaOutputDtoList = new List<DetalleNotaDeReservaOutputDto>();
            foreach (var detalleNotaDeReserva in detallesNotasDeReservas)
            {
                detalleNotaReservaOutputDtoList.Add(MapToDto(detalleNotaDeReserva));
            }
            return detalleNotaReservaOutputDtoList;
        }
    }
}
