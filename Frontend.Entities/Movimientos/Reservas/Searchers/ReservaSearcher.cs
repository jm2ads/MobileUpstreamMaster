using Frontend.Business.Commons;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Reservas.Searchers
{
    public class ReservaSearcher
    {
        private readonly IRepository<Reserva> repository;

        public ReservaSearcher(IRepository<Reserva> repository)
        {
            this.repository = repository;
        }

        public async Task<IList<Reserva>> GetAll()
        {
            return await repository.GetAll();
        }

        public async Task<IList<Reserva>> GetAllByIds(IList<int> ids, TipoReserva tipoReserva)
        {
            return await repository.Where(x => ids.Contains(x.Id) && x.TipoReserva == tipoReserva);
        }

        public async Task<IList<Reserva>> GetAllByIds(IList<int> ids)
        {
            return await repository.Where(x => ids.Contains(x.Id));
        }

        public async Task<IList<Reserva>> GetAllBy(TipoReserva tipoReserva, params EstadoMovimiento[] estadosMovimiento)
        {
            return await repository.Where(x => estadosMovimiento.Contains(x.Estado) && x.TipoReserva == tipoReserva);
        }

        public async Task<IList<Reserva>> GetAllBy(params EstadoMovimiento[] estadosMovimiento)
        {
            return await repository.Where(x => estadosMovimiento.Contains(x.Estado));
        }

        public async Task<IList<Reserva>> GetAllWithChildren()
        {
            return await repository.GetAllWithChildren();
        }

        public async Task<IList<Reserva>> GetAllBy(int materialId)
        {
            var reservas = await GetAll();
            return reservas.Where(x => x.DetallesReserva.Any(z => z.MaterialId == materialId)).ToList();
        }

        public async Task<Reserva> GetWithChildren(int id)
        {
            return await repository.GetWithChildren(id);
        }
    }
}
