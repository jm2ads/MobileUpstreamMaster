using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Usuarios.Validators
{
    public class UsuarioValidator
    {
        private readonly IRepository<Usuario> repository;

        public UsuarioValidator(IRepository<Usuario> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> ValidatePin(Usuario usuario, string pin)
        {
            return usuario.Pin.ToUpper() == pin.ToUpper();
        }
    }
}
