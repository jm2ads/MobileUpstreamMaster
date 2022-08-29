using Frontend.Azure.Common;
using Frontend.Azure.DTOs.Movimientos;
using Frontend.Azure.Mappers.Movimientos;
using Frontend.Business.IData;
using Frontend.Business.Movimientos;
using Frontend.IServices.IServices;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    public class MovimientoAzureRestService : ISyncRestService<Movimiento>
    {
        private readonly YpfAzureHttpClient client;
        private readonly MovimientoMapper mapper;
        private readonly ISettingsService settingsService;

        public MovimientoAzureRestService(YpfAzureHttpClient client, MovimientoMapper mapper, ISettingsService settingsService)
        {
            this.client = client;
            this.mapper = mapper;
            this.settingsService = settingsService;
        }

        public async Task<IList<Movimiento>> DoGet(object parameters)
        {
            return await GetAll();
        }

        public Task<Movimiento> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Movimiento>> DoPost(object body)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<Movimiento>> GetAll()
        {
            var setting = await settingsService.Get();
            var movimientos = await client.CallWithHeaders<List<MovimientoInputDto>>(UrlConstants.MovimientoApi, null, HttpMethod.Post, mapper.MapToDto(setting));

            return mapper.MapFromDto(movimientos).ToList();
        }
    }
}
