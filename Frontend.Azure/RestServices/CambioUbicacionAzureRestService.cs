using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Frontend.Azure.Common;
using Frontend.Azure.Mappers.CambiosUbicacion;
using Frontend.Business.CambiosUbicacion;
using Frontend.Business.IData;

namespace Frontend.Azure.RestServices
{
    public class CambioUbicacionAzureRestService : ISyncRestService<CambioUbicacion>
    {
        #region Private Properties

        private readonly YpfAzureHttpClient client;
        private readonly CambioUbicacionMapper cambioUbicacionMapper;

        #endregion

        #region Public Methods

        public CambioUbicacionAzureRestService(YpfAzureHttpClient client, CambioUbicacionMapper cambioUbicacionMapper)
        {
            this.client = client;
            this.cambioUbicacionMapper = cambioUbicacionMapper;
        }

        public Task<IList<CambioUbicacion>> DoGet(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<CambioUbicacion> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<CambioUbicacion>> DoPost(object body)
        {
            var entities = (IList<CambioUbicacion>)body;
            return await SendCambiosUbicacion(entities);
        }

        private async Task<IList<CambioUbicacion>> SendCambiosUbicacion(IList<CambioUbicacion> listaCambiosUbicacion)
        {
            var cambiosUbicacion = await cambioUbicacionMapper.MapToDto(listaCambiosUbicacion);
            await client.CallWithHeaders(UrlConstants.InventariosCambioUbicacion, cambiosUbicacion, HttpMethod.Post, null);

            return listaCambiosUbicacion;
        }

        #endregion
    }
}
