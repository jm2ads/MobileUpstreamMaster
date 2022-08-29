using Frontend.Azure.DTOs;
using Frontend.Business.Inventarios;
using Frontend.Business.Inventarios.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers.Inventarios
{
    public class InventarioLogMapper
    {
        private readonly InventarioLogFactory inventarioLogFactory;

        public InventarioLogMapper(InventarioLogFactory inventarioLogFactory)
        {
            this.inventarioLogFactory = inventarioLogFactory;
        }

        public async Task<InventarioLog> MapFromDto(RespuestaDto respuestaDto)
        {
            var inventarioLog = inventarioLogFactory.Create();

            inventarioLog.IdRemoto = respuestaDto.Id;
            inventarioLog.Label = respuestaDto.Label;
            inventarioLog.Success = respuestaDto.Success;
            inventarioLog.Data = respuestaDto.Data;
            if (respuestaDto.Adicional != null)
            {
                inventarioLog.Adicional = await MapFromDto(respuestaDto.Adicional);
            }

            return inventarioLog;
        }

        public async Task<List<InventarioLog>> MapFromDto(IList<RespuestaDto> listRespuestoDto)
        {
            var lstInventarioLog = new List<InventarioLog>();

            foreach (var respuestaDto in listRespuestoDto)
            {
                lstInventarioLog.Add(await MapFromDto(respuestaDto));
            }

            return lstInventarioLog;
        }
    }
}
