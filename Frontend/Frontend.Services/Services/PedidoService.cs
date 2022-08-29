using Frontend.Business.LecturaQRs;
using Frontend.Business.Materiales;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Ingresos;
using Frontend.Business.Movimientos.Ingresos.Core;
using Frontend.Business.Movimientos.Ingresos.Searchers;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly DetallePedidoSearcher detallePedidoSearcher;
        private readonly PedidoSearcher pedidoSearcher;
        private readonly PedidoFactory pedidoFactory;
        private readonly ISettingsService settingsService;


        public PedidoService(DetallePedidoSearcher detallePedidoSearcher, PedidoSearcher pedidoSearcher, PedidoFactory pedidoFactory, ISettingsService settingsService)
        {
            this.detallePedidoSearcher = detallePedidoSearcher;
            this.pedidoSearcher = pedidoSearcher;
            this.pedidoFactory = pedidoFactory;
            this.settingsService = settingsService;
        }

        public async Task<IList<int>> GetPedidosByCodigoMaterial(string searchValue)
        {
            return await detallePedidoSearcher.GetPedidoIdsByCodigoMaterial(searchValue);
        }

        public async Task<IList<int>> GetPedidosDescripcionMaterial(string searchValue)
        {
            return await detallePedidoSearcher.GetPedidoIdsDescripcionMaterial(searchValue);
        }

        public async Task<IList<Material>> GetAllMaterialAutocomplete()
        {
            return await detallePedidoSearcher.GetAllMaterialAutocomplete();
        }

        public async Task<IList<string>> GetAllNumeroDePedidosAutocomplete()
        {
            return await pedidoSearcher.GetAllNumeroDePedidosAutocomplete();
        }

        public async Task<IList<Pedido>> GetAllPedidos()
        {
            return await pedidoSearcher.GetAll();
        }

        public async Task<IList<Pedido>> GetAllByIds(IList<int> pedidoIds)
        {
            return await pedidoSearcher.GetAllByIds(pedidoIds);
        }

        public async Task<IList<Pedido>> GetAllByIds(IList<int> pedidoIds, EstadoMovimiento estadoMovimiento)
        {
            var pedidos = await pedidoSearcher.GetAllByIds(pedidoIds);
            return pedidos.Where(x => x.Estado == estadoMovimiento).ToList();
        }

        public async Task<Pedido> CreatePedido()
        {
            return pedidoFactory.Create();
        }

        public async Task<Pedido> GetWithChildren(int pedidoId)
        {
            return await pedidoSearcher.GetById(pedidoId);
        }

        public async Task<IList<Pedido>> GetAllBy(EstadoMovimiento estadoIngreso)
        {
            return await pedidoSearcher.GetAllBy(estadoIngreso);
        }
        public async Task<IList<Material>> GetAllMaterialBy(EstadoMovimiento estadoIngreso)
        {
            return await detallePedidoSearcher.GetAllMaterialBy(estadoIngreso);
        }

        public bool ValidatePosicionesSinClase(List<DetallePedido> detallePedidos)
        {
            return detallePedidos.All(dp => dp.DetallesPedidoPosicion.All(dps => string.IsNullOrWhiteSpace(dps.ClaseMovimientoCodigo)));
        }

        public bool ValidatePosicionesTodasClase103(List<DetallePedido> detallePedidos)
        {
            return detallePedidos.All(dp => dp.DetallesPedidoPosicion.All(dps => dps.ClaseMovimientoCodigo == ClaseDeMovimientoPedido.ClaseDeMovimiento[ClaseDeMovimientoPedido.CLASE_103]));
        }

        public async Task<IList<DetallePedido>> GetPedidosByAsync(LecturaQR lecturaQR)
        {
            return await detallePedidoSearcher.GetDetallesPedidoIdsBy(lecturaQR);
        }
        public async Task<IList<Pedido>> GetAllBy(LecturaQR lecturaQR)
        {
            var detalles = await detallePedidoSearcher.GetDetallesPedidoIdsBy(lecturaQR);
            var pedidos = await pedidoSearcher.GetAllByIds(detalles.Select(x => x.PedidoId).Distinct().ToList());
            return pedidos.Where(p => string.IsNullOrWhiteSpace(lecturaQR.NumeroMovimiento) || p.NumeroPedido == lecturaQR.NumeroMovimiento).ToList();
        }
    }
}
