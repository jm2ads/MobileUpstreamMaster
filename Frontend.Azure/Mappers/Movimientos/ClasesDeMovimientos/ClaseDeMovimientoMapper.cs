using Frontend.Azure.DTOs.Movimientos.ClasesDeMovimientos;
using Frontend.Business.Movimientos;
using System.Collections.Generic;

namespace Frontend.Azure.Mappers.Movimientos.ClasesDeMovimientos
{
    public class ClaseDeMovimientoMapper
    {
        public ClaseDeMovimiento MapFromDto(ClaseDeMovimientoInputDto claseDeMovimientoResponseDto, int movimientoId)
        {
            var claseDeMovimiento = new ClaseDeMovimiento();
            claseDeMovimiento.Id = claseDeMovimientoResponseDto.Id;
            claseDeMovimiento.Codigo = claseDeMovimientoResponseDto.Codigo;
            claseDeMovimiento.MovimientoId = movimientoId;
            return claseDeMovimiento;
        }

        public IEnumerable<ClaseDeMovimiento> MapFromDto(IList<ClaseDeMovimientoInputDto> list, int movimientoId)
        {
            foreach (var claseDeMovimientoResponseDto in list)
            {
                yield return MapFromDto(claseDeMovimientoResponseDto, movimientoId);
            }
        }
    }
}
