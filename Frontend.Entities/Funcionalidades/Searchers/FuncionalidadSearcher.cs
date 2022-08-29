using Frontend.Business.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.Funcionalidades.Searchers
{
    public class FuncionalidadSearcher
    {
        private readonly IRepository<Funcionalidad> repository;

        public FuncionalidadSearcher(IRepository<Funcionalidad> repository)
        {
            this.repository = repository;
        }

        public async Task<IList<Funcionalidad>> GetByAllIds(IList<int> listIds)
        {
            return await repository.GetAllByIds(listIds);
        }
    }
}
