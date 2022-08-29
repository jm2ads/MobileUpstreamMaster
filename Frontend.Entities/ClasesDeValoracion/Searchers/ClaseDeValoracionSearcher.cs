using Frontend.Business.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Business.ClasesDeValoracion.Searchers
{
    public class ClaseDeValoracionSearcher
    {
        private readonly IRepository<ClaseDeValoracion> repository;

        public ClaseDeValoracionSearcher(IRepository<ClaseDeValoracion> repository)
        {
            this.repository = repository;
        }

        public async Task<ClaseDeValoracion> GetById(int id)
        {
            return await repository.GetWithChildren(id);
        }

        public async Task<IList<ClaseDeValoracion>> GetAllByCodigoMaterial(string codigoMaterial)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<ClaseDeValoracion>> GetAll()
        {
            return await repository.GetAllWithChildren();
        }

        public async Task<ClaseDeValoracion> GetByCodigo(string codigo)
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
