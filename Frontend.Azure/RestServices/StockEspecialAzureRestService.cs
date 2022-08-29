using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers;
using Frontend.Business.IAzureRestServices;
using Frontend.Business.IData;
using Frontend.Business.StocksEspeciales;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    public class StockEspecialAzureRestService : IStockEspecialAzureRestService, ISyncRestService<StockEspecial>
    {
        #region Private Properties

        private readonly StockEspecialMapper mapper;
        private readonly YpfAzureHttpClient client;

        #endregion

        #region Public Methods

        public StockEspecialAzureRestService(YpfAzureHttpClient client, StockEspecialMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public Task<IList<StockEspecial>> DoGet(object parameters)
        {
            return GetAll();
        }

        public Task<StockEspecial> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<StockEspecial>> DoPost(object body)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<StockEspecial>> GetAll()
        {
            var stockEspecial = await client.CallWithHeaders<List<StockEspecialInputDto>>(UrlConstants.StockEspecialApi, null, HttpMethod.Post, null);
            return await mapper.MapFromDto(stockEspecial);
        }

        #endregion
    }
}
