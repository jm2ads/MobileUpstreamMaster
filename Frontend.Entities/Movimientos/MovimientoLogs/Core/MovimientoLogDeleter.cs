using Frontend.Business.Commons;
using Frontend.Business.Movimientos.MovimientoLogs.Searchers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.MovimientoLogs.Core
{
    public class MovimientoLogDeleter
    {
        private readonly IRepository<MovimientoLog> repository;
        private readonly MovimientoLogSearcher movimientoLogSearcher;

        public MovimientoLogDeleter(IRepository<MovimientoLog> repository, MovimientoLogSearcher movimientoLogSearcher)
        {
            this.repository = repository;
            this.movimientoLogSearcher = movimientoLogSearcher;
        }

        public async Task Delete(int id)
        {
            await repository.Delete(id);
        }

        public async Task Delete(IList<int> listIdRemoto)
        {
            foreach (var idRemoto in listIdRemoto)
            {
                var listMovimientoLog = await movimientoLogSearcher.GetAllBy(idRemoto);
                foreach (var movimientoLog in listMovimientoLog)
                {
                    await Delete(movimientoLog.Id);
                }
            }
        }

        public async Task DeleteOlderThan(int cantidadDias)
        {
            var listMovimientoLog = await movimientoLogSearcher.GetOlderThan(cantidadDias);
            foreach (var movimientoLog in listMovimientoLog)
            {
                await Delete(movimientoLog.Id);
            }
        }
    }
}
