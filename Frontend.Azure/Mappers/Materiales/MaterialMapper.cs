using Frontend.Azure.DTOs;
using Frontend.Business.Centros;
using Frontend.Business.GruposDeArticulos.Searchers;
using Frontend.Business.Materiales;
using Frontend.Business.Materiales.Core;
using Frontend.Business.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers
{
    public class MaterialMapper
    {
        private readonly MaterialFactory materialFactory;
        private readonly GrupoDeArticuloMapper grupoDeArticuloMapper;
        private readonly GrupoDeArticuloSearcher grupoDeArticuloSearcher;

        public MaterialMapper(MaterialFactory materialFactory, GrupoDeArticuloMapper grupoDeArticuloMapper, GrupoDeArticuloSearcher grupoDeArticuloSearcher)
        {
            this.materialFactory = materialFactory;
            this.grupoDeArticuloMapper = grupoDeArticuloMapper;
            this.grupoDeArticuloSearcher = grupoDeArticuloSearcher;
        }

        public async Task<Material> MapFromDto(MaterialInputDto materialInputDto)
        {
            var material = materialFactory.Create();

            material.Id = materialInputDto.Id;
            material.Codigo = materialInputDto.Codigo;
            material.Descripcion = materialInputDto.Descripcion;
            material.UnidadDeMedidaBase = materialInputDto.UnidadDeMedidaBase;
            material.UnidadDeMedidaAlternativa1 = materialInputDto.UnidadDeMedidaAlternativa1;
            material.UnidadDeMedidaAlternativa2 = materialInputDto.UnidadDeMedidaAlternativa2;
            material.UnidadDeMedidaAlternativa3 = materialInputDto.UnidadDeMedidaAlternativa3;
            material.UnidadDeMedidaAlternativa4 = materialInputDto.UnidadDeMedidaAlternativa4;
            material.IdGrupoArticulo = materialInputDto.GrupoDeArticuloId;

            return material;
        }


        public async Task<IList<Material>> MapFromDto(IList<MaterialInputDto> listMaterialInputDto)
        {
            IList<Material> listMateriales = new List<Material>();

            foreach (var materialInputDto in listMaterialInputDto)
            {
                listMateriales.Add(await MapFromDto(materialInputDto));
            }

            return listMateriales;
        }

        public async Task<MaterialOutputDto> MapToDto(Setting setting)
        {
            var materialOutputDto = new MaterialOutputDto();

            materialOutputDto.CentroId = setting.CentroActivoId;
            materialOutputDto.delta = setting.LastSync.ToString("O");

            return materialOutputDto;
        }
    }
}
