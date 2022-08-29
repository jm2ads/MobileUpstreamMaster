using Frontend.Business.Movimientos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IMovimientoLogService
    {
        Task<IList<MovimientoLog>> Generate(IList<MovimientoLog> listMovimientoLog);
        Task<IList<MovimientoLog>> GetAllMovimientoLogError();
        Task Delete(IList<int> listIdRemoto);
        Task DeleteOlderThan(int cantidadDias);
    }
}
