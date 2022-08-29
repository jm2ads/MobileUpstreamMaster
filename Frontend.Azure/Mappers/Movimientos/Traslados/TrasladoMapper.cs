using Frontend.Azure.DTOs.Movimientos.Traslados;
using Frontend.Business.Movimientos.Traslados;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers.Movimientos.Traslados
{
    public class TrasladoMapper
    {
        private readonly DetalleTrasladoMapper detalleTrasladoMapper;
        public TrasladoMapper(DetalleTrasladoMapper detalleTrasladoMapper)
        {
            this.detalleTrasladoMapper = detalleTrasladoMapper;
        }

        public TrasladoOutputDto MapToDto(Traslado traslado)
        {
            var trasladoOutputDto = new TrasladoOutputDto();
            trasladoOutputDto.ClaseDeMovimientoCodigo = traslado.ClaseDeMovimientoCodigo;
            trasladoOutputDto.FechaDocumento = traslado.FechaDocumento;
            trasladoOutputDto.FechaContabilizacion = traslado.FechaContabilizacion;
            trasladoOutputDto.NumeroProvisorio = traslado.NumeroProvisorio;
            trasladoOutputDto.Usuario = traslado.Usuario;
            trasladoOutputDto.DetallesTraslado = detalleTrasladoMapper.MapToDto(traslado.DetallesTraslado);

            return trasladoOutputDto;
        }

        public async Task<IList<TrasladoOutputDto>> MapToDto(IList<Traslado> traslados)
        {
            var trasladoOutputDtoList = new List<TrasladoOutputDto>();
            foreach (var traslado in traslados)
            {
                trasladoOutputDtoList.Add(MapToDto(traslado));
            }
            return trasladoOutputDtoList;
        }
    }
}
