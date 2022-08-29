using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Reservas.Core.Reservas
{
    public class ReservaUpdater
    {
        private readonly IRepository<Reserva> repository;

        public ReservaUpdater(IRepository<Reserva> repository)
        {
            this.repository = repository;
        }

        public async Task Update(Reserva reserva)
        {
            await repository.UpdateWithChildren(reserva);
        }
    }
}
