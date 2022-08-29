using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Usuarios.Searchers
{
    public class UsuarioSearcher
    {
        private readonly IRepository<Usuario> repository;
        
        public UsuarioSearcher(IRepository<Usuario> repository)
        {
            this.repository = repository;
        }

        public async Task<Usuario> GetById(int id)
        {
            return await repository.FindFirstWithChildren(x => x.Id == id);
        }

        public async Task<Usuario> GetByIdRed(string idRed)
        {
            return await repository.FindFirstWithChildren(x => x.IdRed.ToUpper() == idRed.ToUpper());
        }
    }
}
