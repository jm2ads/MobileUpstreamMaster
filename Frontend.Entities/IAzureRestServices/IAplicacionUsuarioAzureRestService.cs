using Frontend.Business.Users.Devices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.IAzureRestServices
{
    public interface IAplicacionUsuarioAzureRestService
    {
        Task<IList<AplicacionUsuario>> GetCredentialsApp(string userId);
    }
}
