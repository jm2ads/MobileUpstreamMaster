using Frontend.Azure.DTOs;
using Frontend.Azure.DTOs.Almacenes;
using Frontend.Business.Almacenes;
using Frontend.Business.Almacenes.Core;
using Frontend.Business.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers
{
    public class AlmacenMapper
    {
        private readonly AlmacenFactory almacenFactory;

        public AlmacenMapper(AlmacenFactory almacenFactory)
        {
            this.almacenFactory = almacenFactory;
        }

        public async Task<Almacen> MapFromDto(AlmacenInputDto almacenInputDto)
        {
            var almacen = almacenFactory.Create(almacenInputDto.CentroId);

            almacen.Id = almacenInputDto.Id;
            almacen.Codigo = almacenInputDto.Codigo;
            almacen.Nombre = almacenInputDto.Nombre;

            return almacen;
        }

        public async Task<IList<Almacen>> MapFromDto(IList<AlmacenInputDto> listAlmacenInputDto)
        {
            IList<Almacen> listAlmacen = new List<Almacen>();

            foreach (var almacenInputDto in listAlmacenInputDto)
            {
                listAlmacen.Add(await MapFromDto(almacenInputDto));
            }

            return listAlmacen;
        }

        public async Task<AlmacenOutputDto> MapToDto(Setting setting)
        {
            var almacenOutputDto = new AlmacenOutputDto();
            almacenOutputDto.delta = setting.LastSync.ToString("O");

            return almacenOutputDto;
        }
    }
}
