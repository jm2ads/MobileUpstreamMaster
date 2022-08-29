using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.IAzureRestServices;
using Frontend.Business.IData;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    public class ClaseDeValoracionAzureRestService : IClaseDeValoracionAzureRestService, ISyncRestService<ClaseDeValoracion>
    {
        #region Private Properties

        private readonly ClaseDeValoracionMapper mapper;
        private readonly ISettingsService settingsService;
        private readonly YpfAzureHttpClient client;

        #endregion

        #region Public Methods

        public ClaseDeValoracionAzureRestService(YpfAzureHttpClient client, ClaseDeValoracionMapper mapper, ISettingsService settingsService)
        {
            this.client = client;
            this.mapper = mapper;
            this.settingsService = settingsService;
        }

        public Task<IList<ClaseDeValoracion>> DoGet(object parameters)
        {
            return GetAll();
        }

        public Task<ClaseDeValoracion> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<ClaseDeValoracion>> DoPost(object body)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<ClaseDeValoracion>> GetAll()
        {
            var setting = await settingsService.Get();
            var clases = await client.CallWithHeaders<List<ClaseDeValoracionInputDto>>(UrlConstants.ClaseDeValoracionApi, null, HttpMethod.Post, await mapper.MapToDto(setting));
            return await mapper.MapFromDto(clases);
        }

        #endregion
    }
}
