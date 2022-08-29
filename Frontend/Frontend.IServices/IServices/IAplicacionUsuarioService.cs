using Frontend.Business.Users.Devices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IAplicacionUsuarioService
    {
        Task<IList<AplicacionUsuario>> GetAll();
        Task GetCredentialsApp(string userId);
    }
}
