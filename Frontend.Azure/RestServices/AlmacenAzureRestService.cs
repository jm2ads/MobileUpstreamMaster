using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers;
using Frontend.Business.Almacenes;
using Frontend.Business.IAzureRestServices;
using Frontend.Business.IData;
using Frontend.IServices.IServices;

namespace Frontend.Azure.RestServices
{
    public class AlmacenAzureRestService : IAlmacenAzureRestService, ISyncRestService<Almacen>
    {
        #region Private Properties

        private readonly AlmacenMapper mapper;
        private readonly ISettingsService settingsService;
        private readonly YpfAzureHttpClient client;

        #endregion

        #region Public Methods

        public AlmacenAzureRestService(YpfAzureHttpClient client, AlmacenMapper mapper, ISettingsService settingsService)
        {
            this.client = client;
            this.mapper = mapper;
            this.settingsService = settingsService;
        }

        public Task<IList<Almacen>> DoGet(object parameters)
        {
            return GetAll();
        }

        public Task<Almacen> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Almacen>> DoPost(object body)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<Almacen>> GetAll()
        {
            var setting = await settingsService.Get();
            var almacenes = await client.CallWithHeaders<List<AlmacenInputDto>>(UrlConstants.AlmacenApi, null, HttpMethod.Post, await mapper.MapToDto(setting));
            return await mapper.MapFromDto(almacenes);
        }

        #endregion
    }
}
