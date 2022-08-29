using Frontend.Azure.DTOs.Movimientos.Traslados;
using Frontend.Business.Movimientos.Traslados;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Azure.Mappers.Movimientos.Traslados
{
    public class DetalleTrasladoMapper
    {
        public DetalleTrasladoOutputDto MapToDto(DetalleTraslado detalleTraslado)
        {
            var detalleTrasladoOutputDto = new DetalleTrasladoOutputDto();
            detalleTrasladoOutputDto.AlmacenId = detalleTraslado.AlmacenId;
            detalleTrasladoOutputDto.Cantidad = detalleTraslado.Cantidad;
            detalleTrasladoOutputDto.CentroId = detalleTraslado.CentroId;
            detalleTrasladoOutputDto.ClaseDeValoracionId = detalleTraslado.ClaseDeValoracionId;
            detalleTrasladoOutputDto.CodigoMaterial = detalleTraslado.CodigoMaterial;
            detalleTrasladoOutputDto.ElementoPEP = detalleTraslado.ElementoPEP;
            detalleTrasladoOutputDto.Posicion = detalleTraslado.DisplayPosicion;
            detalleTrasladoOutputDto.Proveedor = detalleTraslado.Proveedor;
            detalleTrasladoOutputDto.StockEspecialId = detalleTraslado.StockEspecialId;
            detalleTrasladoOutputDto.StockId = detalleTraslado.StockId;
            detalleTrasladoOutputDto.Textobreve = detalleTraslado.TextoBreve;
            return detalleTrasladoOutputDto;
        }

        public List<DetalleTrasladoOutputDto> MapToDto(List<DetalleTraslado> detalles)
        {
            var detallesList = new List<DetalleTrasladoOutputDto>();
            foreach (var detalle in detalles)
            {
                detallesList.Add(MapToDto(detalle));
            }
            return detallesList;
        }
    }
}
