using Frontend.Business.Commons;
using Frontend.Business.Movimientos.Reservas;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Reservas.Core.Reservas
{
    public class ReservaGenerator
    {
        private readonly IRepository<Reserva> repository;

        public ReservaGenerator(IRepository<Reserva> repository) 
        {
            this.repository = repository;
        }

        public async Task Generate(Reserva reserva)
        {
            await repository.SaveWithChildren(reserva);
        }
    }
}
