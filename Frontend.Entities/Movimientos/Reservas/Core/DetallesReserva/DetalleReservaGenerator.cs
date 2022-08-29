using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Reservas.Core.DetallesReservas
{
    public class DetalleReservaGenerator
    {
        private readonly IRepository<DetalleReserva> repository;

        public DetalleReservaGenerator(IRepository<DetalleReserva> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(DetalleReserva detalleReserva)
        {
            await repository.Save(detalleReserva);
        }
    }
}
