using Frontend.Azure.DTOs;
using Frontend.Business.Funcionalidades;
using Frontend.Business.Funcionalidades.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Azure.Mapper
{
    public class FuncionalidadMapper
    {
        private readonly FuncionalidadFactory funcionalidadFactory;

        public FuncionalidadMapper(FuncionalidadFactory funcionalidadFactory)
        {
            this.funcionalidadFactory = funcionalidadFactory;
        }

        public async Task<Funcionalidad> MapFromDto(FuncionalidadInputDto funcionalidadInputDto)
        {
            var funcionalidad = funcionalidadFactory.Create();

            funcionalidad.Id = funcionalidadInputDto.Id;
            funcionalidad.Nombre = funcionalidadInputDto.Nombre;
            funcionalidad.Orden = funcionalidadInputDto.Orden;

            return funcionalidad;
        }

        public async Task<IList<Funcionalidad>> MapFromDto(IList<FuncionalidadInputDto> listFuncionalidadInputDto)
        {
            IList<Funcionalidad> listFuncionalidad = new List<Funcionalidad>();

            foreach (var funcionalidadInputDto in listFuncionalidadInputDto)
            {
                listFuncionalidad.Add(await MapFromDto(funcionalidadInputDto));
            }

            return listFuncionalidad;
        }

        public async Task<Funcionalidad> MapFromDto(int funcionalidadId)
        {
            var funcionalidad = funcionalidadFactory.Create();

            funcionalidad.Id = funcionalidadId;

            return funcionalidad;
        }

        public async Task<IList<Funcionalidad>> MapFromDto(IList<int> listfuncionalidadId)
        {
            IList<Funcionalidad> listFuncionalidad = new List<Funcionalidad>();

            foreach (var funcionalidadInputDto in listfuncionalidadId)
            {
                listFuncionalidad.Add(await MapFromDto(funcionalidadInputDto));
            }

            return listFuncionalidad;
        }

        public async Task<FuncionalidadOutputDto> MapToDto(string idRed, int centroId)
        {
            var funcionalidadOutputDto = new FuncionalidadOutputDto();

            funcionalidadOutputDto.IdRed = idRed;
            funcionalidadOutputDto.CentroId = centroId;

            return funcionalidadOutputDto;
        }
    }
}
