using Frontend.Business.Movimientos.Ingresos;
using Frontend.Business.Synchronizer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface INotaDeEntregaService
    {
        Task<NotaDeEntrega> GetOrCreate(Pedido pedido);
        Task Update(NotaDeEntrega notaDeEntrega, SyncState syncState = SyncState.Updated);
        Task Update(DetalleNotaDeEntregaPosicion detalleNotaDeEntregaPosicion, string claseMovimientoCodigo = null, SyncState syncState = SyncState.Updated);
        bool Validate(DetalleNotaDeEntrega detalleNotaDeEntrega);
        Task DeleteDetalle(IList<DetalleNotaDeEntregaPosicion> detalleNotaDeEntregaList);
        bool ValidatePosicion(DetalleNotaDeEntrega detalleNotaDeEntrega);
    }
}
