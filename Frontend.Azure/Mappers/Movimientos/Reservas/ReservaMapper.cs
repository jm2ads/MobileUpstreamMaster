using Frontend.Azure.DTOs.Movimientos.Reservas;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Reservas;
using Frontend.Business.Movimientos.Reservas.Core.Reservas;
using Frontend.Business.Settings;
using Frontend.Commons.Commons;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers.Movimientos.Reservas
{
    public class ReservaMapper
    {
        private readonly ReservaFactory reservaFactory;
        private readonly DetalleReservaMapper detalleReservaMapper;

        public ReservaMapper(ReservaFactory reservaFactory, DetalleReservaMapper detalleReservaMapper)
        {
            this.reservaFactory = reservaFactory;
            this.detalleReservaMapper = detalleReservaMapper;
        }

        public async Task<Reserva> MapFromDto(ReservaInputDto reservaResponseDto, int centroId)
        {
            var reserva = reservaFactory.Create();

            reserva.Id = reservaResponseDto.Id;
            reserva.Numero = reservaResponseDto.NumeroReserva;
            reserva.Imputacion = reservaResponseDto.Imputacion;
            reserva.ClaseDeMovimiento = reservaResponseDto.ClaseDeMovimientoCodigo;
            reserva.Estado = (EstadoMovimiento)reservaResponseDto.EstadoMovimientoId;
            reserva.TipoReserva = (TipoReserva)reservaResponseDto.TipoDeReserva;
            reserva.DetallesReserva = await detalleReservaMapper.MapFromDto(reservaResponseDto.DetallesReservas, reserva.Id, centroId);

            return reserva;
        }

        public async Task<IList<Reserva>> MapFromDto(IList<ReservaInputDto> listReservaResponseDto, int centroId)
        {
            var reservas = new List<Reserva>();
            foreach (var item in listReservaResponseDto)
            {
                reservas.Add(await MapFromDto(item, centroId));
            }
            return reservas;
        }

        public async Task<ReservaOutputDto> MapToDto(Setting setting, params EstadoMovimiento[] estadoMoviminetoIds)
        {
            var reservaOutputDto = new ReservaOutputDto();
            reservaOutputDto.CentroId = setting.CentroActivoId;
            reservaOutputDto.delta = ApplicationConstants.DefaultDateSync.ToString("O");
            reservaOutputDto.EstadoMovimientoIds = estadoMoviminetoIds.Select(x => x.GetHashCode()).ToArray();
            return reservaOutputDto;
        }
    }
}
