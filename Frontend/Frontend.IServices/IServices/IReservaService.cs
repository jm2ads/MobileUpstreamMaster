using Frontend.Business.LecturaQRs;
using Frontend.Business.Materiales;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.NotasDeReservas;
using Frontend.Business.Movimientos.Reservas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IReservaService
    {
        Task<IList<Reserva>> GetAll();
        Task<IList<Reserva>> GetAllBy(TipoReserva tipoReserva, params EstadoMovimiento[] estadosMovimiento);
        Task<Reserva> GetWithChildren(int id);
        Task<IList<Material>> GetAllMaterial();
        Task<IList<Material>> GetAllMaterialBy(TipoReserva tipoReserva, params EstadoMovimiento[] estadosMovimiento);
        Task<IList<Material>> GetAllMaterialBy(params EstadoMovimiento[] estadosMovimiento);
        Task<IList<Reserva>> GetAllBy(params EstadoMovimiento[] estadosMovimiento);
        Task<IList<Reserva>> GetAllBy(int materialId, string claseValoracion, TipoReserva tipoReserva);
        Task<IList<Reserva>> GetAllBy(LecturaQR lecturaQR, TipoReserva tipoReserva);
        Task<IList<Reserva>> GetAllBy(LecturaQR lecturaQR);
        Task<IList<DetalleReserva>> GetAllDetallesByReserva(int reservaId);
        Task<IList<DetalleReserva>> GetAllDetalles();
        IList<TipoStock> GetAllTipoStock();
        TipoStock GetTipoStockBy(string codigo);
    }
}
