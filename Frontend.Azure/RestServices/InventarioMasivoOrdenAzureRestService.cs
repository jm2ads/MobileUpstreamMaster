using Frontend.Azure.Common;
using Frontend.Azure.DTOs.InventariosMasivos;
using Frontend.Azure.Mappers.InventariosMasivos;
using Frontend.Business.Commons;
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
    public class InventarioMasivoOrdenAzureRestService : ISyncRestService<InventarioMasivoOrden>
    {
        private readonly ISettingsService settingsService;
        private readonly YpfAzureHttpClient client;
        private readonly InventarioMasivoOrdenMapper mapper;
        private readonly IDatabaseManager databaseManager;

        public InventarioMasivoOrdenAzureRestService(ISettingsService settingsService, YpfAzureHttpClient client, InventarioMasivoOrdenMapper mapper, IDatabaseManager databaseManager)
        {
            this.settingsService = settingsService;
            this.client = client;
            this.mapper = mapper;
            this.databaseManager = databaseManager;
        }

        public async Task<IList<InventarioMasivoOrden>> DoGet(object parameters)
        {
            await databaseManager.ResetDB(new List<Type>(){typeof(InventarioMasivoOrden)});
            var setting = await settingsService.Get();
            var inventarioMasivoOrden = await client.CallWithHeaders<IList<InventarioMasivoOrdenInputDto>>(UrlConstants.InventariosMasivosOrdenGetByCentro, null, HttpMethod.Post, mapper.MapToDto(setting));
            return mapper.MapFromDto(inventarioMasivoOrden).ToList();
        }

        public Task<InventarioMasivoOrden> DoGetEntity(object parameters)
        {
            throw new NotImplementedException();
        }

        public Task<IList<InventarioMasivoOrden>> DoPost(object body)
        {
            throw new NotImplementedException();
        }
    }
}
