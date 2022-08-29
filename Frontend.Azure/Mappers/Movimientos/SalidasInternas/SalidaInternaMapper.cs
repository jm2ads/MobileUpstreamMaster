using Frontend.Azure.DTOs.Movimientos.SalidasInternas;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.SalidasInternas;
using Frontend.Business.Movimientos.SalidasInternas.Core;
using Frontend.Business.Settings;
using Frontend.Commons.Commons;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers.Movimientos.SalidasInternas
{
    public class SalidaInternaMapper
    {
        private readonly SalidaInternaFactory salidaInternaFactory;
        private readonly DetalleSalidaInternaMapper detalleSalidaInternaMapper;

        public SalidaInternaMapper(SalidaInternaFactory salidaInternaFactory, DetalleSalidaInternaMapper detalleSalidaInternaMapper)
        {
            this.salidaInternaFactory = salidaInternaFactory;
            this.detalleSalidaInternaMapper = detalleSalidaInternaMapper;
        }

        public SalidaInterna MapFromDto(SalidaInternaInputDto salidaInternaResponseDto)
        {
            var salidaInterna = salidaInternaFactory.Create();

            salidaInterna.Id = salidaInternaResponseDto.Id;
            salidaInterna.NumeroPedido = salidaInternaResponseDto.NumeroPedido;
            salidaInterna.ClaseDeMovimientoCodigo = salidaInternaResponseDto.ClaseDeMovimientoCodigo;
            salidaInterna.Estado = (EstadoMovimiento) salidaInternaResponseDto.EstadoMovimientoId;
            salidaInterna.CentroReceptorId = salidaInternaResponseDto.CentroReceptorId;
            foreach (var item in salidaInternaResponseDto.DetallesSalidasInternas)
            {
                salidaInterna.DetallesSalidaInterna.Add(detalleSalidaInternaMapper.MapFromDto(item, salidaInternaResponseDto.Id));
            }

            return salidaInterna;
        }

        public IList<SalidaInterna> MapFromDto(IList<SalidaInternaInputDto> listSalidaInternaResponseDto)
        {
            var salidaInterna = new List<SalidaInterna>();
            foreach (var item in listSalidaInternaResponseDto)
            {
                salidaInterna.Add(MapFromDto(item));
            }
            return salidaInterna;
        }

        public async Task<SalidaInternaRequestDto> MapToDto(Setting setting, params EstadoMovimiento[] estadoMoviminetoIds)
        {
            var salidaInternaRequestDto = new SalidaInternaRequestDto();
            salidaInternaRequestDto.CentroId = setting.CentroActivoId;
            salidaInternaRequestDto.delta = ApplicationConstants.DefaultDateSync.ToString("O");
            salidaInternaRequestDto.EstadoMovimientoIds = estadoMoviminetoIds.Select(x => x.GetHashCode()).ToArray();
            return salidaInternaRequestDto;
        }

        public SalidaInternaOutputDto MapToDto(SalidaInterna salidaInterna)
        {
            var salidaInternaDto = new SalidaInternaOutputDto();
            salidaInternaDto.DetallesSalidasInternas = new List<DetalleSalidaInternaOutputDto>();

            salidaInternaDto.Id = salidaInterna.Id;
            salidaInternaDto.Usuario = salidaInterna.Usuario;
            salidaInternaDto.FechaDocumento = salidaInterna.FechaDocumento.ToString("O");
            salidaInternaDto.FechaContabilizacion = salidaInterna.FechaContabilizacion.ToString("O");

            foreach (var detallesSalidaInterna in salidaInterna.DetallesSalidaInterna.Where(x=>x.EsContado))
            {
                salidaInternaDto.DetallesSalidasInternas.Add(detalleSalidaInternaMapper.MapToDto(detallesSalidaInterna));
            }

            return salidaInternaDto;
        }

        public IEnumerable<SalidaInternaOutputDto> MapToDto(IList<SalidaInterna> salidaInternaList)
        {
            foreach (var salidaInterna in salidaInternaList)
            {
                yield return MapToDto(salidaInterna);
            }
        }
    }
}
