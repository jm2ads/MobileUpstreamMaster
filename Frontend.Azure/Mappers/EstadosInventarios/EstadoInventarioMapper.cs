using Frontend.Azure.DTOs;
using Frontend.Business.EstadosInventarios;
using Frontend.Business.EstadosInventarios.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers
{
    public class EstadoInventarioMapper
    {
        private readonly EstadoInventarioFactory estadoInventarioFactory;

        public EstadoInventarioMapper(EstadoInventarioFactory estadoInventarioFactory)
        {
            this.estadoInventarioFactory = estadoInventarioFactory;
        }

        public async Task<EstadoInventario> MapFromDto(EstadoInventarioInputDto estadoInventarioInputDto)
        {
            var estadoInventario = estadoInventarioFactory.Create();

            estadoInventario.Id = estadoInventarioInputDto.Id;
            estadoInventario.Nombre = estadoInventarioInputDto.Descripcion;

            return estadoInventario;
        }

        public async Task<IList<EstadoInventario>> MapFromDto(IList<EstadoInventarioInputDto> estadosInventarioInputDto)
        {
            IList<EstadoInventario> listEstados = new List<EstadoInventario>();

            foreach (var estadoInputDto in estadosInventarioInputDto)
            {
                listEstados.Add(await MapFromDto(estadoInputDto));
            }

            return listEstados;
        }
    }
}
