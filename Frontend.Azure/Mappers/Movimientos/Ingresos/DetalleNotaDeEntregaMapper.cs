using Frontend.Azure.DTOs;
using Frontend.Business.Movimientos.Ingresos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frontend.Azure.Mappers.Movimientos.Ingresos
{
    public class DetalleNotaDeEntregaMapper
    {
        private readonly DetalleNotaDeEntregaPosicionMapper detalleNotaDeEntregaPosicionMapper;
        public DetalleNotaDeEntregaMapper(DetalleNotaDeEntregaPosicionMapper detalleNotaDeEntregaPosicionMapper)
        {
            this.detalleNotaDeEntregaPosicionMapper = detalleNotaDeEntregaPosicionMapper;
        }

        public DetalleNotaDeEntregaOutputDto MapToDto(DetalleNotaDeEntrega detalleNotaDeEntrega)
        {
            var detalleNotaDeEntregaOutputDto = new DetalleNotaDeEntregaOutputDto();
            detalleNotaDeEntregaOutputDto.EntregaFinal = detalleNotaDeEntrega.EntregaFinal;
            detalleNotaDeEntregaOutputDto.TextoPosicion = detalleNotaDeEntrega.TextoPosicion;
            detalleNotaDeEntregaOutputDto.PuestoDeDescarga = detalleNotaDeEntrega.PuestoDeDescarga;
            detalleNotaDeEntregaOutputDto.TipoDeStock = detalleNotaDeEntrega.TipoStockId;
            detalleNotaDeEntregaOutputDto.DestinatarioMercancia = detalleNotaDeEntrega.DestinatarioMercancia;
            detalleNotaDeEntregaOutputDto.ClaseDeValoracionId = detalleNotaDeEntrega.ClaseDeValoracionId.GetValueOrDefault();
            detalleNotaDeEntregaOutputDto.AlmacenId = detalleNotaDeEntrega.AlmacenId.GetValueOrDefault();
            detalleNotaDeEntregaOutputDto.StockEspecialId = detalleNotaDeEntrega.StockEspecialId;
            detalleNotaDeEntregaOutputDto.CentroId = detalleNotaDeEntrega.CentroId;
            detalleNotaDeEntregaOutputDto.Posicion = detalleNotaDeEntrega.DetallePedido.Posicion;
            detalleNotaDeEntregaOutputDto.Unidad = detalleNotaDeEntrega.DetallePedido.Unidad;
            detalleNotaDeEntregaOutputDto.MaterialId = detalleNotaDeEntrega.DetallePedido.MaterialId;
            detalleNotaDeEntregaOutputDto.Tolerancia = detalleNotaDeEntrega.DetallePedido.Tolerancia;
            detalleNotaDeEntregaOutputDto.DetallesNotaDeEntregaPosicion = detalleNotaDeEntregaPosicionMapper.MapToDto(detalleNotaDeEntrega.DetalleNotaDeEntregaPosicion).ToList();
            return detalleNotaDeEntregaOutputDto;
        }

        public IList<DetalleNotaDeEntregaOutputDto> MapToDto(IList<DetalleNotaDeEntrega> detallesNotasDeEntrega)
        {
            var detalleNotaDeEntregaOutputDtoList = new List<DetalleNotaDeEntregaOutputDto>();
            foreach (var detalleNotasDeEntrega in detallesNotasDeEntrega)
            {
                detalleNotaDeEntregaOutputDtoList.Add(MapToDto(detalleNotasDeEntrega));
            }
            return detalleNotaDeEntregaOutputDtoList;
        }
    }
}
