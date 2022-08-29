using Frontend.Business.Commons;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.MovimientoLogs.Searchers
{
    public class MovimientoLogSearcher
    {
        private readonly IRepository<MovimientoLog> repository;

        public MovimientoLogSearcher(IRepository<MovimientoLog> repository)
        {
            this.repository = repository;
        }

        public Task<IList<MovimientoLog>> GetAll()
        {
            return repository.GetAllWithChildren();
        }

        public Task<IList<MovimientoLog>> GetAllError()
        {
            return repository.Where(x => !x.Success);
        }

        public Task<MovimientoLog> GetBy(int idRemoto)
        {
            return repository.First(x => x.IdRemoto == idRemoto);
        }

        public Task<IList<MovimientoLog>> GetAllBy(int idRemoto)
        {
            return repository.Where(x => x.IdRemoto == idRemoto);
        }

        public Task<IList<MovimientoLog>> GetOlderThan(int cantidadDias)
        {
            var fecha = DateTime.UtcNow.AddDays(-cantidadDias);
            return repository.Where(x => x.FechaCreacion < fecha);
        }
    }
}
