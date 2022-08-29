using Frontend.Business.Commons;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Business.GruposDeArticulos.Searchers
{
    public class GrupoDeArticuloSearcher
    {
        private readonly IRepository<GrupoDeArticulo> repository;

        public GrupoDeArticuloSearcher(IRepository<GrupoDeArticulo> repository)
        {
            this.repository = repository;
        }

        public async Task<GrupoDeArticulo> GetById(int Id)
        {
            return await repository.FindFirstWithChildren(x => x.Id == Id);
        }

        public async Task<IList<GrupoDeArticulo>> GetAll()
        {
            return await repository.GetAllWithChildren();
        }

        public async Task<GrupoDeArticulo> GetByCodigo(string codigo)
        {
            return await repository.FindFirstWithChildren(x => x.Codigo == codigo);
        }

        public async Task<IList<string>> GetAllCodigoAutocomplete()
        {
            var list = await repository.GetAll();
            return list.Select(x => x.Codigo).Distinct().ToList();
        }
    }
}
