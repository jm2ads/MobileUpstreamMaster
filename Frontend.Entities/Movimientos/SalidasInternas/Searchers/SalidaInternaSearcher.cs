using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frontend.Business.Commons;

namespace Frontend.Business.Movimientos.SalidasInternas.Searchers
{
    public class SalidaInternaSearcher
    {
        private readonly IRepository<SalidaInterna> repository;

        public SalidaInternaSearcher(IRepository<SalidaInterna> repository)
        {
            this.repository = repository;
        }
        public async Task<IList<SalidaInterna>> GetAllBy(EstadoMovimiento estadoIngreso, string claseMovimientoCodigo)
        {
            return await repository.Where(x => x.Estado == estadoIngreso && x.ClaseDeMovimientoCodigo == claseMovimientoCodigo);
        }

        public async Task<IList<SalidaInterna>> GetAllByIdsAsync(IList<int> salidasIds)
        {
            return await repository.GetAllByIds(salidasIds);
        }

        public async Task<SalidaInterna> GetById(int id)
        {
            return await repository.GetWithChildren(id);
        }
    }
}
