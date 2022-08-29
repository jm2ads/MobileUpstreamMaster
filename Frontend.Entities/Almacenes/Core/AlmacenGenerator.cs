using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Almacenes.Core
{
    public class AlmacenGenerator
    {
        private readonly IRepository<Almacen> repository;

        public AlmacenGenerator(IRepository<Almacen> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(Almacen almacen)
        {
            await repository.SaveWithChildren(almacen);
        }
    }
}
