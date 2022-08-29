using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers;
using Frontend.Business.IAzureRestServices;
using Frontend.Business.IData;
using Frontend.Business.Stocks;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    public class StockAzureRestService : IStockAzureRestService, ISyncRestService<Stock>
    {
        private readonly StockMapper mapper;
        private readonly ISettingsService settingsService;
        private readonly YpfAzureHttpClient client;

        public StockAzureRestService(YpfAzureHttpClient client, StockMapper mapper, ISettingsService settingsService)
        {
            this.client = client;
            this.mapper = mapper;
            this.settingsService = settingsService;
        }

        public Task<IList<Stock>> DoGet(object parameters)
        {
            return GetAll();
        }

        public Task<Stock> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Stock>> DoPost(object body)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<Stock>> GetAll()
        {
            var setting = await settingsService.GetWithChildren();
            var stocks = await client.CallWithHeaders<List<StockInputDto>>(UrlConstants.StockApi, null, HttpMethod.Post, await mapper.MapToDto(setting));
            return await mapper.MapFromDto(stocks.ToList(), setting.CentroActivoId);
        }

        public async Task<IList<Stock>> GetByCentro(int centroId)
        {
            var setting = await settingsService.GetWithChildren();
            var x = await mapper.MapToDto(setting);
            var stocks = await client.CallWithHeaders<List<StockInputDto>>(UrlConstants.StockApi, null, HttpMethod.Post, x);
            return await mapper.MapFromDto(stocks.ToList(), setting.CentroActivoId);
        }

        public async Task<IList<Stock>> GetEntitiesToSync()
        {
            var setting = await settingsService.GetWithChildren();
            return await GetByCentro(setting.CentroActivoId);
        }
    }
}
