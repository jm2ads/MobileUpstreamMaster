using Frontend.Business.Commons;
using Frontend.Business.LecturaQRs;
using Frontend.Business.Materiales;
using Frontend.Business.Materiales.Searchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Ingresos.Searchers
{
    public class DetallePedidoSearcher
    {
        private readonly IRepository<DetallePedido> repository;
        private readonly MaterialSearcher materialSearcher;
        private readonly PedidoSearcher pedidoSearcher;

        public DetallePedidoSearcher(IRepository<DetallePedido> repository, MaterialSearcher materialSearcher, PedidoSearcher pedidoSearcher)
        {
            this.materialSearcher = materialSearcher;
            this.pedidoSearcher = pedidoSearcher;
            this.repository = repository;
        }
        public async Task<IList<DetallePedido>> GetAll()
        {
            return await repository.GetAll();
        }

        public async Task<IList<DetallePedido>> GetAllBy(EstadoMovimiento estadoIngreso)
        {
            var pedidos = await pedidoSearcher.GetAllBy(estadoIngreso);
            var pedidosIds = pedidos.Select(x => x.Id);
            return await repository.Where(x => pedidosIds.Contains(x.PedidoId));
        }

        public async Task<IList<int>> GetPedidoIdsByMaterial(int materialId)
        {
            var detalles = await repository.GetAll();
            var pedidosIds = detalles.Where(x => x.MaterialId == materialId).Select(x => x.PedidoId).Distinct().ToList();
            return pedidosIds;
        }

        public async Task<IList<int>> GetPedidoIdsByCodigoMaterial(string searchValue)
        {
            var detalles = await repository.GetAllWithChildren();
            var pedidosIds = detalles.Where(x => x.Material.Codigo.TrimStart('0').ToUpper() == searchValue.ToUpper()).Select(x => x.PedidoId).Distinct().ToList();
            return pedidosIds;
        }

        public async Task<IList<int>> GetPedidoIdsDescripcionMaterial(string searchValue)
        {
            var detalles = await repository.GetAllWithChildren();
            var pedidosIds = detalles.Where(x => x.Material.Descripcion.ToUpper() == searchValue.ToUpper()).Select(x => x.PedidoId).Distinct().ToList();
            return pedidosIds;
        }

        public async Task<IList<DetallePedido>> GetDetallesPedidoIdsBy(LecturaQR lecturaQR)
        {
            var materialIdHasValue = lecturaQR.MaterialId.HasValue;
            var almacenIdHasValue = lecturaQR.AlmacenId.HasValue;
            var loteIdHasValue = lecturaQR.LoteId.HasValue;
            return await repository.Where(x => (materialIdHasValue && x.MaterialId == lecturaQR.MaterialId)
            && (!almacenIdHasValue || x.AlmacenId == lecturaQR.AlmacenId)
            && (!loteIdHasValue || x.ClaseDeValoracionId == lecturaQR.LoteId));
        }

        public async Task<IList<Material>> GetAllMaterialAutocomplete()
        {
            try
            {
                var detalles = await repository.GetAllWithChildren();
                var materialesIds = detalles.Select(x => x.MaterialId).Distinct().ToList();
                return await materialSearcher.GetAllByIds(materialesIds);
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public async Task<IList<Material>> GetAllMaterialBy(EstadoMovimiento estadoIngreso)
        {
            var detalles = await GetAllBy(estadoIngreso);
            var materialesIds = detalles.Select(x => x.MaterialId).Distinct().ToList();
            return await materialSearcher.GetAllByIds(materialesIds);
        }
    }
}
