using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mapper;
using Frontend.Business.Funcionalidades;
using Frontend.Business.IAzureRestServices;
using Frontend.Business.IData;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    public class FuncionalidadAzureRestService : IFuncionalidadAzureRestService, ISyncRestService<Funcionalidad>
    {
        #region Private Properties

        private readonly FuncionalidadMapper mapper;
        private readonly YpfAzureHttpClient client;

        #endregion

        #region Public Methods

        public FuncionalidadAzureRestService(YpfAzureHttpClient client, FuncionalidadMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        //Para el caso particular de funcionalidad, como deben ser visibles antes de que el usuario se loguee, no se envían headers.
        public async Task<IList<Funcionalidad>> GetAll()
        {
            var funcionalidades = await client.Call<List<FuncionalidadInputDto>>(UrlConstants.FuncionalidadApi, null, HttpMethod.Post, null);
            return await mapper.MapFromDto(funcionalidades);
        }

        public async Task<IList<Funcionalidad>> GetByIdRed(string idRed, int centroId)
        {
            var funcionalidadOutputDto = await mapper.MapToDto(idRed, centroId);
            var funcionalidadesIds = await client.CallWithHeaders<List<int>>(UrlConstants.FuncionalidadByIdRedCentroApi, null, HttpMethod.Post, funcionalidadOutputDto);
            return await mapper.MapFromDto(funcionalidadesIds);
        }

        public Task<IList<Funcionalidad>> DoGet(object parameters)
        {
            return GetAll();
        }

        public Task<Funcionalidad> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Funcionalidad>> DoPost(object body)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
