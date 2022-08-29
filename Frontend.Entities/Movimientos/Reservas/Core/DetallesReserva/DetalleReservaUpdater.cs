using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Reservas.Core.DetallesReservas
{
    public class DetalleReservaUpdater
    {
        private readonly IRepository<DetalleReserva> repository;

        public DetalleReservaUpdater(IRepository<DetalleReserva> repository)
        {
            this.repository = repository;
        }
        
        public async Task Update(DetalleReserva detalleReserva)
        {
            await repository.UpdateWithChildren(detalleReserva);
        }
    }
}
