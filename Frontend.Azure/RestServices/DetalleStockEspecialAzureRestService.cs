using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers;
using Frontend.Business.DetallesStocksEspeciales;
using Frontend.Business.IAzureRestServices;
using Frontend.Business.IData;
using Frontend.IServices.IServices;

namespace Frontend.Azure.RestServices
{
    public class DetalleStockEspecialAzureRestService : IDetalleStockEspecialAzureRestService, ISyncRestService<DetalleStockEspecial>
    {
        #region Private Properties

        private readonly DetalleStockEspecialMapper mapper;
        private readonly ISettingsService settingsService;
        private readonly YpfAzureHttpClient client;

        #endregion

        #region Public Methods

        public DetalleStockEspecialAzureRestService(YpfAzureHttpClient client, DetalleStockEspecialMapper mapper, ISettingsService settingsService)
        {
            this.client = client;
            this.mapper = mapper;
            this.settingsService = settingsService;
        }

        public Task<IList<DetalleStockEspecial>> DoGet(object parameters)
        {
            return GetAll();
        }

        public Task<DetalleStockEspecial> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<DetalleStockEspecial>> DoPost(object body)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<DetalleStockEspecial>> GetAll()
        {
            var setting = await settingsService.Get();
            var detalles = await client.CallWithHeaders<List<DetalleStockEspecialInputDto>>(UrlConstants.DetalleStockEspecialApi, null, HttpMethod.Post, await mapper.MapToDto(setting));
            return await mapper.MapFromDto(detalles);
        }

        #endregion
    }
}
