using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Searchers
{
    public class MovimientoSearcher
    {
        private readonly IRepository<Movimiento> repository;

        public MovimientoSearcher(IRepository<Movimiento> repository)
        {
            this.repository = repository;
        }

        public async Task<Movimiento> GetWithChildrenBy(string nombre)
        {
            return await repository.FindFirstWithChildren(x => x.Nombre == nombre);
        }
    }
}
