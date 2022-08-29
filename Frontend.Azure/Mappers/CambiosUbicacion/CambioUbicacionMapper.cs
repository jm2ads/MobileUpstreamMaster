using Frontend.Azure.DTOs.CambiosUbicacion;
using Frontend.Business.CambiosUbicacion;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers.CambiosUbicacion
{
    public class CambioUbicacionMapper
    {
        public CambioUbicacionOutputDto MapToDto(CambioUbicacion cambioUbicacion)
        {
            var cambioUbicacionOutputDto = new CambioUbicacionOutputDto();
            cambioUbicacionOutputDto.Ubicacion = cambioUbicacion.Ubicacion;
            cambioUbicacionOutputDto.UsuarioAprobador = cambioUbicacion.Usuario;
            cambioUbicacionOutputDto.FechaEnvio = cambioUbicacion.FechaCreacion;
            cambioUbicacionOutputDto.CentroId = cambioUbicacion.IdCentro;
            cambioUbicacionOutputDto.MaterialId = cambioUbicacion.IdMaterial;
            cambioUbicacionOutputDto.AlmacenesIncluidosIds = cambioUbicacion.AlmacenesIncluidos.Select(x => x.Id).ToArray();

            return cambioUbicacionOutputDto;
        }

        public async Task<IList<CambioUbicacionOutputDto>> MapToDto(IList<CambioUbicacion> cambiosUbicacion)
        {
            var cambiosUbicacionOutputDtoList = new List<CambioUbicacionOutputDto>();
            foreach (var cambioUbicacion in cambiosUbicacion)
            {
                cambiosUbicacionOutputDtoList.Add(MapToDto(cambioUbicacion));
            }
            return cambiosUbicacionOutputDtoList;
        }
    }
}
