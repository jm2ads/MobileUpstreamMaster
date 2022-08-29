using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers;
using Frontend.Business.GruposDeArticulos;
using Frontend.Business.IAzureRestServices;
using Frontend.Business.IData;
using Frontend.IServices.IServices;

namespace Frontend.Azure.RestServices
{
    public class GrupoDeArticuloAzureRestService : IGrupoDeArticuloAzureRestService, ISyncRestService<GrupoDeArticulo>
    {
        #region Private Properties

        private readonly GrupoDeArticuloMapper mapper;
        private readonly ISettingsService settingsService;
        private readonly YpfAzureHttpClient client;

        #endregion

        #region Public Methods

        public GrupoDeArticuloAzureRestService(YpfAzureHttpClient client, GrupoDeArticuloMapper mapper, ISettingsService settingsService)
        {
            this.client = client;
            this.mapper = mapper;
            this.settingsService = settingsService;
        }

        public Task<IList<GrupoDeArticulo>> DoGet(object parameters)
        {
            return GetAll();
        }

        public Task<GrupoDeArticulo> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<GrupoDeArticulo>> DoPost(object body)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<GrupoDeArticulo>> GetAll()
        {
            var setting = await settingsService.Get();
            var grupos = await client.CallWithHeaders<List<GrupoDeArticuloInputDto>>(UrlConstants.GruposDeArticulosApi, null, HttpMethod.Post, await mapper.MapToDto(setting));
            return await mapper.MapFromDto(grupos);
        }

        #endregion
    }
}
