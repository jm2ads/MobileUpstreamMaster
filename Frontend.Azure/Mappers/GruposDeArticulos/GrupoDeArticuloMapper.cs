using Frontend.Azure.DTOs;
using Frontend.Azure.DTOs.GruposDeArticulos;
using Frontend.Business.GruposDeArticulos;
using Frontend.Business.GruposDeArticulos.Core;
using Frontend.Business.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers
{
    public class GrupoDeArticuloMapper
    {
        private readonly GrupoDeArticuloFactory grupoDeArticuloFactory;

        public GrupoDeArticuloMapper(GrupoDeArticuloFactory grupoDeArticuloFactory)
        {
            this.grupoDeArticuloFactory = grupoDeArticuloFactory;
        }

        public async Task<GrupoDeArticulo> MapFromDto(GrupoDeArticuloInputDto grupoDeArticuloInputDto)
        {
            var grupoDeArticulo = grupoDeArticuloFactory.Create();

            grupoDeArticulo.Id = grupoDeArticuloInputDto.Id;
            grupoDeArticulo.Codigo = grupoDeArticuloInputDto.Codigo;

            return grupoDeArticulo;
        }

        public async Task<IList<GrupoDeArticulo>> MapFromDto(IList<GrupoDeArticuloInputDto> gruposInputDto)
        {
            IList<GrupoDeArticulo> listGrupos = new List<GrupoDeArticulo>();

            foreach (var grupoInputDto in gruposInputDto)
            {
                listGrupos.Add(await MapFromDto(grupoInputDto));
            }

            return listGrupos;
        }

        public async Task<GrupoDeArticuloOutputDto> MapToDto(Setting setting)
        {
            var grupoDeArticuloOutputDto = new GrupoDeArticuloOutputDto();

            grupoDeArticuloOutputDto.delta = setting.LastSync.ToString("O");

            return grupoDeArticuloOutputDto;
        }
    }
}
