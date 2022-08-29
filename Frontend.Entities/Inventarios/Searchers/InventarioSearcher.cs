using Frontend.Business.Commons;
using Frontend.Business.DetallesInventario;
using Frontend.Business.DetallesInventario.Searchers;
using Frontend.Business.Materiales;
using Frontend.Business.Materiales.Searchers;
using Frontend.Business.Stocks.Searchers;
using Frontend.Commons.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Business.Inventarios.Searchers
{
    public class InventarioSearcher
    {
        private readonly IRepository<Inventario> repository;
        private readonly DetalleInventarioSearcher detalleInventarioSearcher;
        private readonly StockSearcher stockSearcher;
        private readonly MaterialSearcher materialSearcher;

        public InventarioSearcher(IRepository<Inventario> repository, DetalleInventarioSearcher detalleInventarioSearcher, StockSearcher stockSearcher,
            MaterialSearcher materialSearcher)
        {
            this.repository = repository;
            this.detalleInventarioSearcher = detalleInventarioSearcher;
            this.stockSearcher = stockSearcher;
            this.materialSearcher = materialSearcher;
        }

        public async Task<IList<Inventario>> GetAll()
        {
            return await repository.GetAll();
        }

        public async Task<IList<Material>> GetAllMaterialRecuento()
        {
            var listInventario = await repository.GetAll();
            var listDetalleInventario = await detalleInventarioSearcher.GetAllByInventaarioIds(listInventario.Where(x => x.Estado == EstadoInventario.Recuento)
                .Select(x => x.Id).ToList());
            var listaStock = await stockSearcher.GetAllByIds(listDetalleInventario.Select(x => x.StockId).Distinct().ToList());
            var listMateriales = await materialSearcher.GetAllByIds(listaStock.Select(x => x.IdMaterial).Distinct().ToList());

            return listMateriales;
        }

        public DetalleInventario GetDetalleInventarioDuplicated(Inventario inventario, DetalleInventario detalleInventario)
        {
            return detalleInventarioSearcher.GetDetalleInventarioDuplicated(detalleInventario, inventario.DetallesInventario);
        }

        public async Task<IList<Inventario>> GetAllByEstado(params object[] estadosInventario)
        {
            return (await repository.Where(x => estadosInventario.Contains(x.Estado))).OrderByDescending(x => x.FechaCreacion).ToList();
        }

        public async Task<IList<Inventario>> GetAllByEstadoWithChildren(params object[] estadosInventario)
        {
            return (await repository.FindWithChildren(x => estadosInventario.Contains(x.Estado))).OrderByDescending(x => x.FechaCreacion).ToList();
        }

        public async Task<Inventario> GetBy(string codigo, EstadoInventario estadoInventario)
        {
            return await repository.FindFirstWithChildren(x => x.Estado == estadoInventario && x.NumeroSAP.ToUpper() == codigo.ToUpper());
        }

        public async Task<Inventario> GetById(int id)
        {
            return await repository.FindFirstWithChildren(x => x.Id == id);
        }

        public async Task<IList<string>> GetAllCodigoAutocomplete()
        {
            return (await this.GetAllByEstado(EstadoInventario.Recuento)).Select(x => x.Codigo.TrimStart('0')).Distinct().ToList();
        }

        public async Task<IList<Inventario>> GetAllByIds(IList<int> ids)
        {
            return (await repository.FindWithChildren(x => ids.Contains(x.Id)));
        }

        public async Task<IList<Inventario>> GetAllBy(IList<int> ids, EstadoInventario estadoInventario)
        {
            return (await repository.Where(x => ids.Contains(x.Id) && x.Estado == estadoInventario));
        }
    }
}
