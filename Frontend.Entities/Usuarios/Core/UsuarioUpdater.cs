using Frontend.Business.Commons;
using Frontend.Business.Funcionalidades.Core;
using System.Threading.Tasks;

namespace Frontend.Business.Usuarios.Core
{
    public class UsuarioUpdater
    {
        private readonly IRepository<Usuario> repository;
        private readonly FuncionalidadUpdater funcionalidadUpdater;

        public UsuarioUpdater(IRepository<Usuario> repository, FuncionalidadUpdater funcionalidadUpdater)
        {
            this.repository = repository;
            this.funcionalidadUpdater = funcionalidadUpdater;
        }

        public async Task Update(Usuario usuario)
        {
            await repository.SaveWithChildren(usuario);
        }
    }
}
