using Frontend.Azure.DTOs;
using Frontend.Azure.DTOs.ClasesDeValoraciones;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.ClasesDeValoracion.Core;
using Frontend.Business.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers
{
    public class ClaseDeValoracionMapper
    {
        private readonly ClaseDeValoracionFactory claseDeValoracionFactory;

        public ClaseDeValoracionMapper(ClaseDeValoracionFactory claseDeValoracionFactory)
        {
            this.claseDeValoracionFactory = claseDeValoracionFactory;
        }

        public async Task<ClaseDeValoracion> MapFromDto(ClaseDeValoracionInputDto claseDeValoracionInputDto)
        {
            var claseDeValoracion = claseDeValoracionFactory.Create();

            claseDeValoracion.Id  = claseDeValoracionInputDto.Id;
            claseDeValoracion.Codigo = claseDeValoracionInputDto.Codigo;
            claseDeValoracion.EsUsado = claseDeValoracionInputDto.EsUsado;

            return claseDeValoracion;
        }

        public async Task<IList<ClaseDeValoracion>> MapFromDto(IList<ClaseDeValoracionInputDto> listClaseDeValoracionInputDto)
        {
            IList<ClaseDeValoracion> listClaseDeValoracion = new List<ClaseDeValoracion>();

            foreach (var claseDeValoracionInputDto in listClaseDeValoracionInputDto)
            {
                listClaseDeValoracion.Add(await MapFromDto(claseDeValoracionInputDto));
            }

            return listClaseDeValoracion;
        }

        public async Task<ClaseDeValoracionOutputDto> MapToDto(Setting setting)
        {
            var claseDeValoracionOutputDto = new ClaseDeValoracionOutputDto();

            claseDeValoracionOutputDto.delta = setting.LastSync.ToString("O");

            return claseDeValoracionOutputDto;
        }
    }
}
