using Frontend.Azure.DTOs.InventariosMasivos;
using Frontend.Business.InventariosMasivos;
using System.Collections.Generic;

namespace Frontend.Azure.Mappers.InventariosMasivos
{
    public class DetalleInventarioMasivoMapper
    {
        public DetalleInventarioMasivoOutputDto MapToDto(DetalleInventarioMasivo detalleInventarioMasivo)
        {
            var detalleInventarioMasivoOutputDto = new DetalleInventarioMasivoOutputDto();

            detalleInventarioMasivoOutputDto.CantidadContada = detalleInventarioMasivo.Cantidad;
            detalleInventarioMasivoOutputDto.StockId = detalleInventarioMasivo.IdStock.GetValueOrDefault();
            detalleInventarioMasivoOutputDto.TipoStockId = detalleInventarioMasivo.TipoStockId;
            detalleInventarioMasivoOutputDto.Unidad = detalleInventarioMasivo.Unidad;
            detalleInventarioMasivoOutputDto.HayConteoErroneo = detalleInventarioMasivo.HayConteoErroneo;
            detalleInventarioMasivoOutputDto.EstadoConteo = detalleInventarioMasivo.EstadoConteo;
            detalleInventarioMasivoOutputDto.Ubicacion = detalleInventarioMasivo.Ubicacion;

            return detalleInventarioMasivoOutputDto;
        }

        public IEnumerable<DetalleInventarioMasivoOutputDto> MapToDto(IList<DetalleInventarioMasivo> listDetalleInventarioMasivo)
        {
            foreach (var detalleInventarioMasivo in listDetalleInventarioMasivo)
            {
                yield return MapToDto(detalleInventarioMasivo);
            }
        }
    }
}
