using Frontend.Azure.DTOs.InventariosMasivos;
using Frontend.Business.InventariosMasivos;
using Frontend.Business.InventariosMasivos.Core;
using Frontend.Business.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers.InventariosMasivos
{
    public class InventarioMasivoOrdenMapper
    {
        private readonly InvenatrioMasivoOrdenFactory invenatrioMasivoOrdenFactory;

        public InventarioMasivoOrdenMapper(InvenatrioMasivoOrdenFactory invenatrioMasivoOrdenFactory)
        {
            this.invenatrioMasivoOrdenFactory = invenatrioMasivoOrdenFactory;
        }

        public InventarioMasivoOrden MapFromDto(InventarioMasivoOrdenInputDto inventarioMasivoOrdenInputDto)
        {
            var inventarioMasivoOrden = invenatrioMasivoOrdenFactory.Create();

            inventarioMasivoOrden.Id = inventarioMasivoOrdenInputDto.Id;
            inventarioMasivoOrden.Orden = inventarioMasivoOrdenInputDto.Orden;
            inventarioMasivoOrden.CentroId = inventarioMasivoOrdenInputDto.CentroId;
            inventarioMasivoOrden.ClaseDeValoracionId = inventarioMasivoOrdenInputDto.LoteId;
            inventarioMasivoOrden.AlmacenId = inventarioMasivoOrdenInputDto.AlmacenId;
            inventarioMasivoOrden.StockEspecialId = inventarioMasivoOrdenInputDto.StockEspecialId;

            return inventarioMasivoOrden;
        }

        public IEnumerable<InventarioMasivoOrden> MapFromDto(IList<InventarioMasivoOrdenInputDto> list)
        {
            foreach (var item in list)
            {
                yield return MapFromDto(item);
            }
        }

        public InventarioMasivoOrdenOutputDto MapToDto(Setting setting)
        {
            var inventarioMasivoOrdenOutputDto = new InventarioMasivoOrdenOutputDto();
            inventarioMasivoOrdenOutputDto.CentroId = setting.CentroActivoId;

            return inventarioMasivoOrdenOutputDto;
        }
    }
}
