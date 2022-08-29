using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.DTOs.Inventarios;
using Frontend.Azure.Mappers.InventarioLocal;
using Frontend.Azure.Mappers.Inventarios;
using Frontend.Business.IData;
using Frontend.Business.InventariosLocales;
using Frontend.Commons.Enums;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    public class InventarioLocalAzureRestService : ISyncRestService<InventarioLocal>
    {
        #region Private Properties

        private readonly InventarioLocalMapper mapper;
        private readonly ISettingsService settingsService;
        private readonly IInventarioService inventarioService;
        private readonly InventarioLogMapper inventarioLogMapper;
        private readonly YpfAzureHttpClient client;

        #endregion

        #region Public Methods

        public InventarioLocalAzureRestService(YpfAzureHttpClient client, InventarioLocalMapper mapper, ISettingsService settingsService, IInventarioService inventarioService,
            InventarioLogMapper inventarioLogMapper)
        {
            this.client = client;
            this.mapper = mapper;
            this.settingsService = settingsService;
            this.inventarioService = inventarioService;
            this.inventarioLogMapper = inventarioLogMapper;
        }

        public async Task<IList<RespuestaDto>> SendNuevosInventarios(IList<CrearInventarioInputDto> nuevosInventarios)
        {
            var response = await client.CallWithHeaders<IList<RespuestaDto>>(UrlConstants.InventariosCreate, nuevosInventarios, HttpMethod.Post, null);
            return response;
        }

        public Task<IList<InventarioLocal>> DoGet(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<InventarioLocal> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<InventarioLocal>> DoPost(object body)
        {
            var entities = (IList<InventarioLocal>)body;
            if (entities.Count == 0)
            {
                return null;
            }

            await SendInventariosRechazados(entities);
            await SendInventariosCreados(entities);

            return null;
        }
        
        private async Task SendInventariosRechazados(IList<InventarioLocal> inventarios)
        {
            var inventariosRechazados = inventarios.Where(x => x.Estado == EstadoInventario.Rechazado).ToList();
            if (inventariosRechazados.Count == 0)
            {
                return;
            }
            var inventariosRechazadosInput = await mapper.MapToDto(inventariosRechazados);
            var listRespuestaDto = await client.CallWithHeaders<IList<RespuestaDto>>(UrlConstants.InventariosCreate, inventariosRechazadosInput, HttpMethod.Post, null);


            var listRespuestaDtoErrors = listRespuestaDto.Where(repuesta => !repuesta.Success).ToList();
            var listRespuestaDtoSuccess = listRespuestaDto.Where(repuesta => repuesta.Success).ToList();

            await settingsService.SetHasSyncWithError(listRespuestaDtoErrors.Count > 0);

            await inventarioService.Generate(await inventarioLogMapper.MapFromDto(listRespuestaDtoErrors));
            var delete = inventarioService.DeleteLog(listRespuestaDtoSuccess.Select(x => x.Id).ToList());
        }

        private async Task SendInventariosCreados(IList<InventarioLocal> inventarios)
        {
            var inventariosCreados = inventarios.Where(x => x.Estado == EstadoInventario.PendienteAprobacion).ToList();
            if (inventariosCreados.Count == 0)
            {
                return;
            }
            var inventariosCreadosInput = await mapper.MapToDto(inventariosCreados);
            var listRespuestaDto = await client.CallWithHeaders<IList<RespuestaDto>>(UrlConstants.InventariosCreate, inventariosCreadosInput, HttpMethod.Post, null);

            var listRespuestaDtoErrors = listRespuestaDto.Where(repuesta => !repuesta.Success).ToList();
            var listRespuestaDtoSuccess = listRespuestaDto.Where(repuesta => repuesta.Success).ToList();

            await settingsService.SetHasSyncWithError(listRespuestaDtoErrors.Count > 0);

            await inventarioService.Generate(await inventarioLogMapper.MapFromDto(listRespuestaDtoErrors));
            var delete = inventarioService.DeleteLog(listRespuestaDtoSuccess.Select(x => x.Id).ToList());

        }

        #endregion
    }
}
