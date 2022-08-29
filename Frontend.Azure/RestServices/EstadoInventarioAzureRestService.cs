using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers;
using Frontend.Business.EstadosInventarios;
using Frontend.Business.IAzureRestServices;
using Frontend.Business.IData;

namespace Frontend.Azure.RestServices
{
    public class EstadoInventarioAzureRestService : IEstadoInventarioAzureRestService, ISyncRestService<EstadoInventario>
    {
        #region Private Properties

        private readonly EstadoInventarioMapper mapper;
        private readonly YpfAzureHttpClient client;

        #endregion

        #region Public Methods

        public EstadoInventarioAzureRestService(YpfAzureHttpClient client, EstadoInventarioMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public Task<IList<EstadoInventario>> DoGet(object parameters)
        {
            return GetAll();
        }

        public Task<EstadoInventario> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<EstadoInventario>> DoPost(object body)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<EstadoInventario>> GetAll()
        {
            var estados = await client.CallWithHeaders<List<EstadoInventarioInputDto>>(UrlConstants.EstadosInventarioApi, null, HttpMethod.Post, null);
            return await mapper.MapFromDto(estados);
        }

        #endregion
    }
}
