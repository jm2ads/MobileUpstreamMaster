using Frontend.Business.Usuarios;
using System.Threading.Tasks;

namespace Frontend.Business.IRestServices
{
    public interface IUserRestService
    {
        Task<Usuario> Login(Usuario usuario);

        Task<Usuario> Register(Usuario usuario);

        Task<Usuario> Validate(Usuario usuario);
    }
}
