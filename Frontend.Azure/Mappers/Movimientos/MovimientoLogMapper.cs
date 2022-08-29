using Frontend.Azure.DTOs;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers.Movimientos
{
    public class MovimientoLogMapper
    {
        private readonly MovimientoLogFactory movimientoLogFactory;

        public MovimientoLogMapper(MovimientoLogFactory movimientoLogFactory)
        {
            this.movimientoLogFactory = movimientoLogFactory;
        }

        public async Task<MovimientoLog> MapFromDto(RespuestaDto respuestaDto, TipoMovimiento tipoMovimiento)
        {
            var inventarioLog = movimientoLogFactory.Create();

            inventarioLog.IdRemoto = respuestaDto.Id;
            inventarioLog.Label = respuestaDto.Label;
            inventarioLog.Success = respuestaDto.Success;
            inventarioLog.Data = respuestaDto.Data;
            inventarioLog.TipoMovimiento = tipoMovimiento;

            if (respuestaDto.Adicional != null)
            {
                inventarioLog.Adicional = await MapFromDto(respuestaDto.Adicional, tipoMovimiento);
            }

            return inventarioLog;
        }

        public async Task<List<MovimientoLog>> MapFromDto(IList<RespuestaDto> listRespuestoDto, TipoMovimiento tipoMovimiento)
        {
            var lstInventarioLog = new List<MovimientoLog>();

            foreach (var respuestaDto in listRespuestoDto.Where(repuestaDto => !repuestaDto.Success))
            {
                lstInventarioLog.Add(await MapFromDto(respuestaDto, tipoMovimiento));
            }

            return lstInventarioLog;
        }
    }
}
