using Frontend.Business.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.NotasDeReservas.Searchers
{
    public class DetalleNotaDeReservaSearcher
    {
        private readonly IRepository<DetalleNotaDeReserva> repository;

        public DetalleNotaDeReservaSearcher(IRepository<DetalleNotaDeReserva> repository)
        {
            this.repository = repository;
        }

        public async Task<IList<DetalleNotaDeReserva>> GetAll()
        {
            return await repository.GetAll();
        }
    }
}
