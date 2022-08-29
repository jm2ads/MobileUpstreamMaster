using Frontend.Azure.DTOs;
using Frontend.Business.Centros;
using Frontend.Business.Centros.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers
{
    public class CentroMapper
    {
        private readonly CentroFactory centroFactory;

        public CentroMapper(CentroFactory centroFactory )
        {
            this.centroFactory = centroFactory;
        }

        public async Task<Centro> MapFromDto(CentroInputDto centroInputDto)
        {
            var centro = centroFactory.Create();

            centro.Id = centroInputDto.Id;
            centro.Codigo = centroInputDto.Codigo;
            centro.Nombre = centroInputDto.Nombre;

            return centro;
        }

        public async Task<IList<Centro>> MapFromDto(IList<CentroInputDto> listCentroInputDto)
        {
            IList<Centro> listCentro = new List<Centro>();

            foreach (var centroInputDto in listCentroInputDto)
            {
                listCentro.Add(await MapFromDto(centroInputDto));
            }

            return listCentro;
        }

        public CentroOutputDto MapToDto(string idRed)
        {
            var centroOutputDto = new CentroOutputDto();
            centroOutputDto.IdRed = idRed;
            return centroOutputDto;
        }
    }
}
