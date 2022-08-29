using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Core;
using Frontend.Business.Movimientos.MovimientoLogs.Core;
using Frontend.Business.Movimientos.MovimientoLogs.Searchers;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class MovimientoLogService : IMovimientoLogService
    {
        private readonly MovimientoLogGenerator movimientoLogGenerator;
        private readonly MovimientoLogSearcher movimientoLogSearcher;
        private readonly MovimientoLogDeleter movimientoLogDeleter;

        public MovimientoLogService(MovimientoLogGenerator movimientoLogGenerator, MovimientoLogSearcher movimientoLogSearcher,
            MovimientoLogDeleter movimientoLogDeleter)
        {
            this.movimientoLogGenerator = movimientoLogGenerator;
            this.movimientoLogSearcher = movimientoLogSearcher;
            this.movimientoLogDeleter = movimientoLogDeleter;
        }

        public async Task<IList<MovimientoLog>> Generate(IList<MovimientoLog> listMovimientoLog)
        {
            return await movimientoLogGenerator.Generate(listMovimientoLog);
        }

        public async Task<IList<MovimientoLog>> GetAllMovimientoLogError()
        {
            return await movimientoLogSearcher.GetAllError();
        }

        public async Task Delete(IList<int> listIdRemoto)
        {
            await movimientoLogDeleter.Delete(listIdRemoto);
        }

        public async Task DeleteOlderThan(int cantidadDias)
        {
            await movimientoLogDeleter.DeleteOlderThan(cantidadDias);
        }
    }
}
