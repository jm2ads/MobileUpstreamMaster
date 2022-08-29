using Frontend.Business.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Traslados.Core
{
    public class DetalleTrasladoDeleter
    {
        private readonly IRepository<DetalleTraslado> repository;

        public DetalleTrasladoDeleter(IRepository<DetalleTraslado> repository)
        {
            this.repository = repository;
        }

        public async Task Delete(DetalleTraslado detalleTraslado)
        {
            await repository.Delete(detalleTraslado);
        }

        public async Task Delete(IList<DetalleTraslado> detalleTrasladoList)
        {
            foreach (var detalleTraslado in detalleTrasladoList)
            {
                await Delete(detalleTraslado);
            }
        }
    }
}
