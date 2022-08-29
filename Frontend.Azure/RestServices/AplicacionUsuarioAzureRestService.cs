using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers;
using Frontend.Business.IAzureRestServices;
using Frontend.Business.Users.Devices;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    public class AplicacionUsuarioAzureRestService : IAplicacionUsuarioAzureRestService
    {
        #region Private Properties

        private readonly AplicacionUsuarioMapper mapper;
        private readonly YpfAzureHttpClient client;

        #endregion

        #region Public Methods

        public AplicacionUsuarioAzureRestService(YpfAzureHttpClient client, AplicacionUsuarioMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<IList<AplicacionUsuario>> GetCredentialsApp(string userId)
        {
            var dto = new AplicacionUsuarioOutputDto() { User = userId };
            var credenciales = await client.CallWithHeaders<List<AplicacionUsuarioDto>>(UrlConstants.CredencialApi, null, HttpMethod.Post, dto);
            return credenciales.Select(x => mapper.Map(x)).ToList();
        }

        #endregion
    }
}
