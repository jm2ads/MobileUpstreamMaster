using Frontend.Business.Movimientos.Traslados;
using Frontend.Business.Synchronizer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface ITrasladoService
    {
        Task Update(Traslado traslado, SyncState pendingToSync);
        Task DeleteDetalle(IList<DetalleTraslado> list);
        Task Update(Traslado traslado);
        bool Validate(Traslado traslado);
        Task Delete(DetalleTraslado detalleTraslado);
        Task Delete(Traslado traslado);
        Task Update(DetalleTraslado detalleTraslado);
        Task<Traslado> GetWithChildren(int id);
    }
}
