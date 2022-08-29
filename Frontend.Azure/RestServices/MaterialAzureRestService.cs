using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers;
using Frontend.Business.IAzureRestServices;
using Frontend.Business.IData;
using Frontend.Business.Materiales;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    public class MaterialAzureRestService : IMaterialAzureRestService, ISyncRestService<Material>
    {
        #region Private Properties

        private readonly MaterialMapper mapper;
        private readonly ISettingsService settingsService;
        private readonly YpfAzureHttpClient client;

        #endregion

        #region Public Methods

        public MaterialAzureRestService(YpfAzureHttpClient client, MaterialMapper mapper, ISettingsService settingsService)
        {
            this.client = client;
            this.mapper = mapper;
            this.settingsService = settingsService;
        }

        public Task<IList<Material>> DoGet(object parameters)
        {
            return GetAll();
        }

        public Task<Material> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Material>> DoPost(object body)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<Material>> GetAll()
        {
            var setting = await settingsService.Get();
            var materiales = await client.CallWithHeaders<List<MaterialInputDto>>(UrlConstants.MaterialApi, null, HttpMethod.Post, await mapper.MapToDto(setting));
            return await mapper.MapFromDto(materiales.ToList());
        }

        #endregion
    }
}
