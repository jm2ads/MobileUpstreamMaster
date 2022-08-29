using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers;
using Frontend.Business.Centros;
using Frontend.Business.IAzureRestServices;
using Frontend.Business.IData;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    public class CentroAzureRestService : ICentroAzureRestService, ISyncRestService<Centro>
    {
        #region Private Properties

        private readonly CentroMapper mapper;
        private readonly YpfAzureHttpClient client;

        #endregion

        #region Public Methods

        public CentroAzureRestService(YpfAzureHttpClient client, CentroMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public Task<IList<Centro>> DoGet(object parameters)
        {
            if (parameters == null)
            {
                return GetAll();
            }
            return GetByIdRed(parameters.ToString());
        }

        public Task<Centro> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Centro>> DoPost(object body)
        {
            throw new System.NotImplementedException();
        }

        //Para el caso particular de centro, como deben ser visibles antes de que el usuario se loguee, no se envían headers.
        public async Task<IList<Centro>> GetAll()
        {
            var centros = await client.Call<List<CentroInputDto>>(UrlConstants.CentroApi, null, HttpMethod.Post, null);
            return await mapper.MapFromDto(centros);
        }

        public async Task<IList<Centro>> GetByIdRed(string idRed)
        {
            var centroOutputDto = mapper.MapToDto(idRed);
            var centros = await client.Call<List<CentroInputDto>>(UrlConstants.CentrosByIdRed, null, HttpMethod.Post, centroOutputDto);
            return await mapper.MapFromDto(centros.GroupBy(x => x.Id).Select(y => y.First()).ToList());
        }

        #endregion
    }
}
