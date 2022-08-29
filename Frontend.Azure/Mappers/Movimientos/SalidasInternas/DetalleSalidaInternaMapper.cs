using Frontend.Azure.DTOs.Movimientos.SalidasInternas;
using Frontend.Business.Movimientos.SalidasInternas;
using Frontend.Business.Movimientos.SalidasInternas.Core;
using System;

namespace Frontend.Azure.Mappers.Movimientos.SalidasInternas
{
    public class DetalleSalidaInternaMapper
    {
        private readonly DetalleSalidaInternaFactory detalleSalidaInternaFactory;

        public DetalleSalidaInternaMapper(DetalleSalidaInternaFactory detalleSalidaInternaFactory)
        {
            this.detalleSalidaInternaFactory = detalleSalidaInternaFactory;
        }

        public DetalleSalidaInterna MapFromDto(DetalleSalidaInternaInputDto detalleSalidaInternaResponseDto, int salidaInternaId)
        {
            var detalleSalidaInterna = detalleSalidaInternaFactory.Create();
            detalleSalidaInterna.Id = detalleSalidaInternaResponseDto.Id;
            detalleSalidaInterna.SalidaInternaId = salidaInternaId;
            detalleSalidaInterna.MaterialId = detalleSalidaInternaResponseDto.MaterialId;
            detalleSalidaInterna.CantidadPendiente = detalleSalidaInternaResponseDto.CantidadPendiente;
            detalleSalidaInterna.Posicion = detalleSalidaInternaResponseDto.Posicion;
            detalleSalidaInterna.UnidadDeMedida = detalleSalidaInternaResponseDto.UnidadDeMedida;

            return detalleSalidaInterna;
        }

        public DetalleSalidaInternaOutputDto MapToDto(DetalleSalidaInterna detalleSalidaInterna)
        {
            var detalleSalidaInternaDto = new DetalleSalidaInternaOutputDto();

            detalleSalidaInternaDto.Id = detalleSalidaInterna.Id;
            detalleSalidaInternaDto.CantidadEnviada = detalleSalidaInterna.CantidadEnviada;
            detalleSalidaInternaDto.ClaseDeValoracionId = detalleSalidaInterna.ClaseDeValoracionId;
            detalleSalidaInternaDto.TextoPosicion = String.IsNullOrEmpty(detalleSalidaInterna.TextoPosicion) ? "" : detalleSalidaInterna.TextoPosicion;
            detalleSalidaInternaDto.DestinatarioDeMercancia = String.IsNullOrEmpty(detalleSalidaInterna.DestinatarioMercancia) ? "" : detalleSalidaInterna.DestinatarioMercancia;
            detalleSalidaInternaDto.AlmacenId = detalleSalidaInterna.AlmacenId.GetValueOrDefault();
            
            return detalleSalidaInternaDto;
        }
    }
}
