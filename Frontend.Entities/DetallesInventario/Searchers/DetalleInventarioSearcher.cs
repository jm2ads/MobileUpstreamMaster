using Frontend.Business.Commons;
using Frontend.Business.DetallesInventario.Validators;
using Frontend.Business.LecturaQRs;
using Frontend.Business.Materiales.Searchers;
using Frontend.Business.Stocks.Searchers;
using Frontend.Commons.Commons;
using Frontend.Commons.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Business.DetallesInventario.Searchers
{
    public class DetalleInventarioSearcher
    {
        private readonly IRepository<DetalleInventario> repository;
        private readonly DetalleInventarioValidator detalleInventarioValidator;
        private readonly StockSearcher stockSearcher;
        private readonly MaterialSearcher materialSearcher;

        public DetalleInventarioSearcher(IRepository<DetalleInventario> repository, DetalleInventarioValidator detalleInventarioValidator, StockSearcher stockSearcher,
            MaterialSearcher materialSearcher)
        {
            this.repository = repository;
            this.detalleInventarioValidator = detalleInventarioValidator;
            this.stockSearcher = stockSearcher;
            this.materialSearcher = materialSearcher;
        }

        public async Task<IList<DetalleInventario>> GetAll()
        {
            return await repository.GetAll();
        }

        public async Task<IList<DetalleInventario>> GetAllByIds(IList<int> idList)
        {
            return await repository.GetAllByIds(idList);
        }

        public async Task<DetalleInventario> GetByIdMaterial(int id)
        {
            var stockIdsList = (await stockSearcher.GetAllByIdMaterial(id)).Select(y => y.Id).ToList();

            var detalle = new DetalleInventario();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (stockIdsList.Count / tamanioPagina); i++)
            {
                var list = stockIdsList.Skip(i * tamanioPagina).Take(tamanioPagina);

                detalle = await repository.FindFirstWithChildren(x => list.Contains(x.StockId));
            }

            return detalle;
        }

        public async Task<IList<int>> GetInventariosIdsByIdMaterial(int id)
        {
            var stockList = (await stockSearcher.GetAllByIdMaterial(id)).Select(y => y.Id).ToList();
            var detalles = await repository.Where(x => stockList.Contains(x.StockId));

            var detallesList = new List<DetalleInventario>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (stockList.Count / tamanioPagina); i++)
            {
                var list = stockList.Skip(i * tamanioPagina).Take(tamanioPagina);
                detallesList.AddRange((await repository.Where(x => list.Contains(x.InventarioId))));
            }
            return detallesList.Select(x => x.InventarioId).ToList();
        }

        public async Task<IList<int>> GetInventariosIdsBy(LecturaQR lecturaQR)
        {
            var stockIdsList = (await stockSearcher.GetBy(lecturaQR.AlmacenId, lecturaQR.LoteId, lecturaQR.MaterialId, lecturaQR.Ubicacion, lecturaQR.PEP)).Select(y => y.Id).ToList();
            
            var detallesList = new List<DetalleInventario>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (stockIdsList.Count / tamanioPagina); i++)
            {
                var list = stockIdsList.Skip(i * tamanioPagina).Take(tamanioPagina);

                detallesList.AddRange(await repository.Where(x => list.Contains(x.StockId)));
            }

            return detallesList.Select(x => x.InventarioId).ToList();
        }

        public async Task<IList<DetalleInventario>> GetAllByInventaarioIds(IList<int> idList)
        {
            var detalleInventarioList = new List<DetalleInventario>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (idList.Count / tamanioPagina); i++)
            {
                var list = idList.Skip(i * tamanioPagina).Take(tamanioPagina);
                detalleInventarioList.AddRange((await repository.Where(x => list.Contains(x.InventarioId))));
            }
            return detalleInventarioList;
        }

        public DetalleInventario GetDetalleInventarioDuplicated(DetalleInventario detalleInventario, List<DetalleInventario> list)
        {
            return list.FirstOrDefault(x => detalleInventarioValidator.IsEqual(x, detalleInventario));
        }

        public async Task<IList<string>> GetAllDescripcionMaterialAutocompleteRecuento()
        {
            return (await repository.GetAllWithChildren()).Where(x => x.Inventario.Estado == Frontend.Commons.Enums.EstadoInventario.Recuento)?.Select(x => x.Stock.Material.Descripcion).Distinct().ToList();
        }

        public async Task<DetalleInventario> GetMaterialByCodigo(string searchValue)
        {
            return (await repository.GetAllWithChildren()).FirstOrDefault(x => x.Stock.Material.Codigo.ToUpper() == searchValue.ToUpper());
        }

        public async Task<DetalleInventario> GetMaterialByDescripcion(string searchValue)
        {
            return (await repository.GetAllWithChildren()).FirstOrDefault(x => x.Stock.Material.Descripcion.ToUpper() == searchValue.ToUpper());
        }

        public async Task<DetalleInventario> GetMaterialByCodigoAndEstado(string searchValue, EstadoInventario estadoInventario)
        {
            return (await repository.GetAllWithChildren()).FirstOrDefault(x => x.Inventario.Estado == estadoInventario && x.Stock.Material.Codigo.ToUpper() == searchValue.ToUpper());
        }

        public async Task<DetalleInventario> GetMaterialByDescripcionAndEstado(string searchValue, EstadoInventario estadoInventario)
        {
            return (await repository.GetAllWithChildren()).FirstOrDefault(x => x.Inventario.Estado == estadoInventario && x.Stock.Material.Descripcion.ToUpper() == searchValue.ToUpper());
        }

        public async Task<IList<DetalleInventario>> GetByIdInventario(int idInventario)
        {
            return await repository.FindWithChildren(x => x.InventarioId == idInventario);
        }

        public async Task<DetalleInventario> GetById(int id)
        {
            return await repository.FindFirstWithChildren(x => x.Id == id);
        }
    }
}
