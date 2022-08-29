using Frontend.Business.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.Funcionalidades.Core
{
    public class FuncionalidadUpdater
    {
        private readonly IRepository<Funcionalidad> repository;

        public FuncionalidadUpdater(IRepository<Funcionalidad> repository)
        {
            this.repository = repository;
        }

        public async Task Update(Funcionalidad funcionalidad)
        {
            await repository.UpdateWithChildren(funcionalidad);
        }

        public async Task Update(IList<Funcionalidad> funcionalidades)
        {
            foreach (var item in funcionalidades)
            {
                await Update(item);
            }
        }
    }
}
