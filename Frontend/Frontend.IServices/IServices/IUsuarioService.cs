using Frontend.Business.Centros;
using Frontend.Business.Usuarios;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IUsuarioService
    {
        Task<Usuario> GetById(int id);
        Task<Usuario> GetByIdRed(string idRed);
        Usuario Create();
        Task Update(Usuario usuario);
        Task<Usuario> Generate(Usuario usuario);
        Task<bool> ValidatePin(Usuario usuario, string pin);
        Task DoLogin(string idRed, string password);
        Task<bool> HasToken(string idRed);
        Task<bool> ValidateToken(Usuario usuario);
        Task DeleteCurrentUser();
        void DropData();
        Task UpdateFuncionalidades(Usuario usuario, int centroId);
    }
}
