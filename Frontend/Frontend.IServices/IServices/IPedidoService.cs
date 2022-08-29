using Frontend.Business.LecturaQRs;
using Frontend.Business.Materiales;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Ingresos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IPedidoService
    {
        Task<IList<int>> GetPedidosByCodigoMaterial(string searchValue);
        Task<IList<DetallePedido>> GetPedidosByAsync(LecturaQR lecturaQR);
        Task<IList<Pedido>> GetAllPedidos();
        Task<IList<Pedido>> GetAllByIds(IList<int> pedidoIds);
        Task<IList<Pedido>> GetAllByIds(IList<int> pedidoIds, EstadoMovimiento estadoMovimiento);
        Task<IList<int>> GetPedidosDescripcionMaterial(string searchValue);
        Task<IList<Material>> GetAllMaterialAutocomplete();
        Task<IList<string>> GetAllNumeroDePedidosAutocomplete();
        Task<Pedido> CreatePedido();
        Task<Pedido> GetWithChildren(int id);
        Task<IList<Pedido>> GetAllBy(EstadoMovimiento estadoIngreso);
        Task<IList<Material>> GetAllMaterialBy(EstadoMovimiento estadoIngreso);
        bool ValidatePosicionesSinClase(List<DetallePedido> detallePedidos);
        bool ValidatePosicionesTodasClase103(List<DetallePedido> detallePedidos);
        Task<IList<Pedido>> GetAllBy(LecturaQR lecturaQR);
    }
}
