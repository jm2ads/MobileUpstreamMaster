using Frontend.Azure.DTOs.Movimientos.Reservas;
using Frontend.Business.Movimientos.Reservas;
using Frontend.Business.Movimientos.Reservas.Core.DetallesReservas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers.Movimientos.Reservas
{
    public class DetalleReservaMapper
    {
        private readonly DetalleReservaFactory detalleReservaFactory;

        public DetalleReservaMapper(DetalleReservaFactory detalleReservaFactory)
        {
            this.detalleReservaFactory = detalleReservaFactory;
        }

        public async Task<DetalleReserva> MapFromDto(DetalleReservaInputDto detalleReservaResponseDto, int reservaId, int centroId)
        {
            var detalleReserva = detalleReservaFactory.Create();
            detalleReserva.ReservaId = reservaId;
            detalleReserva.CentroId = centroId;
            detalleReserva.Id = detalleReservaResponseDto.Id;
            detalleReserva.MaterialId = detalleReservaResponseDto.MaterialId;
            detalleReserva.AlmacenId = detalleReservaResponseDto.AlmacenId;
            detalleReserva.CantidadReserva = detalleReservaResponseDto.CantidadReserva;
            detalleReserva.Posicion = detalleReservaResponseDto.Posicion;
            detalleReserva.Unidad = detalleReservaResponseDto.Unidad;
            detalleReserva.Destinatario = detalleReservaResponseDto.Destinatario;
            detalleReserva.PuestoDeDescarga = detalleReservaResponseDto.PuestoDeDescarga;
            detalleReserva.TextoPosicion = detalleReservaResponseDto.TextoPosicion;
            detalleReserva.ClaseDeValoracionId = detalleReservaResponseDto.ClaseDeValoracionId;
            detalleReserva.ClaseDeMovimientoCodigo = detalleReservaResponseDto.ClaseDeMovimientoCodigo;

            return detalleReserva;
        }

        public async Task<List<DetalleReserva>> MapFromDto(IList<DetalleReservaInputDto> listDetalleReservaResponseDto, int reservaId, int centroId)
        {
            var detallesReservas = new List<DetalleReserva>();
            foreach (var item in listDetalleReservaResponseDto)
            {
                detallesReservas.Add(await MapFromDto(item, reservaId, centroId));
            }
            return detallesReservas;
        }
    }
}
