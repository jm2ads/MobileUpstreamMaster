using Frontend.Business.Commons;
using Frontend.Business.DetallesInventarioLocal;
using Frontend.Business.DetallesInventarioLocal.Searchers;
using Frontend.Commons.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.InventariosLocales.Searchers
{
    public class InventarioLocalSearcher
    {
        private readonly IRepository<InventarioLocal> repository;
        private readonly DetalleInventarioLocalSearcher detalleInventarioSearcher;

        public InventarioLocalSearcher(IRepository<InventarioLocal> repository, DetalleInventarioLocalSearcher detalleInventarioSearcher)
        {
            this.repository = repository;
            this.detalleInventarioSearcher = detalleInventarioSearcher;
        }

        public async Task<IList<InventarioLocal>> GetAllProvisorios()
        {
            return await repository.Where(x => x.Estado == EstadoInventario.Provisorio);
        }

        public async Task<InventarioLocal> GetById(int id)
        {
            return await repository.FindFirstWithChildren(x => x.Id == id);
        }

        public DetalleInventarioLocal GetDetalleInventarioDuplicated(InventarioLocal inventario, DetalleInventarioLocal detalleInventario)
        {
            return detalleInventarioSearcher.GetDetalleInventarioDuplicated(detalleInventario, inventario.DetallesInventario);
        }
    }
}
