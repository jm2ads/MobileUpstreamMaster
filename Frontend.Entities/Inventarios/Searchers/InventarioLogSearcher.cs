using Frontend.Business.Commons;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.Inventarios.Searchers
{
    public class InventarioLogSearcher
    {
        private readonly IRepository<InventarioLog> repository;

        public InventarioLogSearcher(IRepository<InventarioLog> repository)
        {
            this.repository = repository;
        }

        public Task<IList<InventarioLog>> GetAll()
        {
            return repository.GetAllWithChildren();
        }

        public Task<IList<InventarioLog>> GetAllError()
        {
            return repository.Where(x => !x.Success);
        }

        public Task<InventarioLog> GetBy(int idRemoto)
        {
            return repository.First(x => x.IdRemoto == idRemoto);
        }

        public Task<IList<InventarioLog>> GetAllBy(int idRemoto)
        {
            return repository.Where(x => x.IdRemoto == idRemoto);
        }

        public Task<IList<InventarioLog>> GetOlderThan(int cantidadDias)
        {
            var fecha = DateTime.UtcNow.AddDays(-cantidadDias);
            return repository.Where(x => x.FechaCreacion < fecha);
        }
    }
}
