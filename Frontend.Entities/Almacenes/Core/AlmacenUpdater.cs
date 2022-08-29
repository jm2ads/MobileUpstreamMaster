using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Almacenes.Core
{
    public class AlmacenUpdater
    {
        private readonly IRepository<Almacen> repository;

        public AlmacenUpdater(IRepository<Almacen> repository)
        {
            this.repository = repository;
        }

        public async Task Update(Almacen almacen)
        {
            await repository.UpdateWithChildren(almacen);
        }
    }
}
