using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Traslados.Core
{
    public class TasladoDeleter
    {
        private readonly IRepository<Traslado> repository;
        private readonly DetalleTrasladoDeleter detalleTrasladoDeleter;

        public TasladoDeleter(IRepository<Traslado> repository, DetalleTrasladoDeleter detalleTrasladoDeleter)
        {
            this.repository = repository;
            this.detalleTrasladoDeleter = detalleTrasladoDeleter;
        }

        public async Task Delete(Traslado traslado)
        {
            await detalleTrasladoDeleter.Delete(traslado.DetallesTraslado);
            await repository.Delete(traslado);
        }
    }
}
