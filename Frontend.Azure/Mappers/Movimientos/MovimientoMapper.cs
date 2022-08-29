using Frontend.Azure.DTOs.Movimientos;
using Frontend.Azure.Mappers.Movimientos.ClasesDeMovimientos;
using Frontend.Business.Movimientos;
using Frontend.Business.Settings;
using System.Collections.Generic;
using System.Linq;

namespace Frontend.Azure.Mappers.Movimientos
{
    public class MovimientoMapper
    {
        private readonly ClaseDeMovimientoMapper claseDeMovimientoMapper;

        public MovimientoMapper(ClaseDeMovimientoMapper claseDeMovimientoMapper)
        {
            this.claseDeMovimientoMapper = claseDeMovimientoMapper;
        }


        public MovimientoOutputDto MapToDto(Setting setting)
        {
            var movimientoOutputDto = new MovimientoOutputDto();

            movimientoOutputDto.CentroId = setting.CentroActivoId;

            return movimientoOutputDto;
        }
        public Movimiento MapFromDto(MovimientoInputDto movimientoInputDto)
        {
            var movimiento = new Movimiento();
            movimiento.Id = movimientoInputDto.Id;
            movimiento.Nombre = movimientoInputDto.Nombre;
            movimiento.ClasesDeMovimientos = claseDeMovimientoMapper.MapFromDto(movimientoInputDto.ClasesDeMovimiento, movimientoInputDto.Id).ToList();
            return movimiento;
        }

        public IEnumerable<Movimiento> MapFromDto(IList<MovimientoInputDto> list)
        {
            foreach (var claseDeMovimientoResponseDto in list)
            {
                yield return MapFromDto(claseDeMovimientoResponseDto);
            }
        }
    }
}
