using Frontend.Business.Users;
using Frontend.Business.Usuarios;
using System.Threading.Tasks;

namespace Frontend.Business.IAzureRestServices
{
    public interface IUserAzureRestService
    {
        Task<Usuario> Login(Usuario usuario);

        Task<Usuario> Register(Usuario usuario);

        Task<Usuario> Validate(Usuario usuario);
    }
}
