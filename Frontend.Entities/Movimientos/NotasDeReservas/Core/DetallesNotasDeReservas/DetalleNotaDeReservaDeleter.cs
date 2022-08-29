using Frontend.Business.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.NotasDeReservas.Core.DetallesNotasDeReservas
{
    public class DetalleNotaDeReservaDeleter
    {
        private readonly IRepository<DetalleNotaDeReserva> repository;

        public DetalleNotaDeReservaDeleter(IRepository<DetalleNotaDeReserva> repository)
        {
            this.repository = repository;
        }

        public async Task Delete(DetalleNotaDeReserva detalleNotaDeReserva)
        {
            await repository.Delete(detalleNotaDeReserva);
        }

        public async Task Delete(IList<DetalleNotaDeReserva> detalleNotaDeReservaList)
        {
            foreach (var detalleNotaDeReserva in detalleNotaDeReservaList)
            {
                await Delete(detalleNotaDeReserva);
            }
        }
    }
}
