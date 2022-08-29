using Frontend.Business.Movimientos.NotasDeReservas;
using Frontend.Business.Movimientos.Reservas;
using Frontend.Business.Synchronizer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface INotaDeReservaService
    {
        Task<NotaDeReserva> GetOrCreate(Reserva reserva);
        Task<IList<DetalleNotaDeReserva>> CreateDetalle(IList<DetalleReserva> listDetalleReserva);
        Task Update(NotaDeReserva notaDeReserva, SyncState syncState = SyncState.Updated);
        Task Update(DetalleNotaDeReserva detalleNotaDeReserva, SyncState syncState = SyncState.Updated);
        Task<IList<DetalleNotaDeReserva>> GetAllDetalles();
        Task DeleteDetalle(IList<DetalleNotaDeReserva> detalleNotaDeReservaList);
        bool Validate(DetalleNotaDeReserva detalleNotaDeReserva);
    }
}
