using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers.Inventarios;
using Frontend.Azure.Mappers.InventariosMasivos;
using Frontend.Business.IData;
using Frontend.Business.InventariosMasivos;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    public class InventarioMasivoAzureRestService : ISyncRestService<InventarioMasivo>
    {
        private readonly ISettingsService settingsService;
        private readonly YpfAzureHttpClient client;
        private readonly InventarioMasivoMapper mapper;
        private readonly InventarioLogMapper inventarioLogMapper;
        private readonly IInventarioService inventarioService;

        public InventarioMasivoAzureRestService(ISettingsService settingsService, YpfAzureHttpClient client, InventarioMasivoMapper mapper, InventarioLogMapper inventarioLogMapper,
            IInventarioService inventarioService)
        {
            this.settingsService = settingsService;
            this.client = client;
            this.mapper = mapper;
            this.inventarioLogMapper = inventarioLogMapper;
            this.inventarioService = inventarioService;
        }

        public async Task<IList<InventarioMasivo>> DoGet(object parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<InventarioMasivo> DoGetEntity(object parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<InventarioMasivo>> DoPost(object body)
        {
            var inventariosMasivos = (IList<InventarioMasivo>)body;
            if (inventariosMasivos.Count == 0)
            {
                return null;
            }
            
            var listInventarioMasivoOutputDto = mapper.MapToDto(inventariosMasivos).ToList();
            var listRespuestaDto = await client.CallWithHeaders<IList<RespuestaDto>>(UrlConstants.InventariosMasivosCreate, listInventarioMasivoOutputDto, HttpMethod.Post, null);

            var listRespuestaDtoErrors = listRespuestaDto.Where(repuesta => !repuesta.Success).ToList();
            var listRespuestaDtoSuccess = listRespuestaDto.Where(repuesta => repuesta.Success).ToList();

            await settingsService.SetHasSyncWithError(listRespuestaDtoErrors.Count > 0);

            await inventarioService.Generate(await inventarioLogMapper.MapFromDto(listRespuestaDtoErrors));
            var delete = inventarioService.DeleteLog(listRespuestaDtoSuccess.Select(x => x.Id).ToList());

            return inventariosMasivos.Where(inventarioMasivo => listRespuestaDtoSuccess.Select(x => x.Id).Contains(inventarioMasivo.Id)).ToList();
        }
    }
}
