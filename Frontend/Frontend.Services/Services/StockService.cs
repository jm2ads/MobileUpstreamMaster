using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.Inventarios.StockEspeciales.Searchers;
using Frontend.Business.Materiales;
using Frontend.Business.Stocks;
using Frontend.Business.Stocks.Searchers;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class StockService : IStockService
    {
        private readonly StockSearcher stockSearcher;
        private readonly StockEspecialSearcher stockEspecialSearcher;

        public StockService(StockSearcher stockSearcher, StockEspecialSearcher stockEspecialSearcher)
        {
            this.stockSearcher = stockSearcher;
            this.stockEspecialSearcher = stockEspecialSearcher;
        }

        public async Task<IList<Stock>> GetBy(int centro, Nullable<int> idAlmacen, Nullable<int> idClaseDeValoracion, Nullable<int> idMaterial, Nullable<int> idGrupoDeArticulo)
        {
            return await stockSearcher.GetBy(centro, idAlmacen, idClaseDeValoracion, idMaterial, idGrupoDeArticulo);
        }

        public async Task<IList<string>> GetAllCodigoMaterialAutocomplete(int centroId, int? almacenId, int stockEspecialId)
        {
            return await stockSearcher.GetAllCodigoMaterialAutocomplete(centroId, almacenId, stockEspecialId);
        }

        public async Task<IList<Stock>> GetByCodigoMaterial(int centroId, int? almacenId, int stockEspecialId, string searchValue)
        {
            return await stockSearcher.GetByCodigoMaterial(centroId, almacenId, stockEspecialId, searchValue);
        }

        public async Task<IList<Stock>> GetBy(int centroId, int? almacenId, int stockEspecialId, string codigoMaterial, int? claseValoracionId)
        {
            return await stockSearcher.GetBy(centroId, almacenId, stockEspecialId, codigoMaterial, claseValoracionId);
        }

        public async Task<IList<Stock>> GetBy(int centroId, string ubicacion, string codigoMaterial, string claseValoracion)
        {
            return await stockSearcher.GetBy(centroId, ubicacion, codigoMaterial, claseValoracion);
        }

        public async Task<IList<string>> GetAllDescripcionMaterialAutocomplete(int centroId, int? almacenId, int stockEspecialId)
        {
            return await stockSearcher.GetAllDescripcionMaterialAutocomplete(centroId, almacenId, stockEspecialId);
        }

        public async Task<IList<Stock>> GetByDescripcionMaterial(int centroId, int? almacenId, int stockEspecialId, string searchValue)
        {
            return await stockSearcher.GetByDescripcionMaterial(centroId, almacenId, stockEspecialId, searchValue);
        }

        public async Task<IList<Stock>> GetByDescripcionMaterial(int centroId, string ubicacion, string searchValue)
        {
            return await stockSearcher.GetByDescripcionMaterial(centroId, ubicacion, searchValue);
        }

        public async Task<IList<Material>> GetAllMaterialBy(int centroId, int? almacenId, int stockEspecialId)
        {
            return await stockSearcher.GetAllMaterialBy(centroId, almacenId, stockEspecialId);
        }

        public async Task<IList<Material>> GetAllMaterialBy(int centroId, IList<int> listAlmacenIds)
        {
            return await stockSearcher.GetAllMaterialBy(centroId, listAlmacenIds);
        }

        public async Task<IList<Material>> GetAllMaterialBy(int centroId, int? almacenId)
        {
            return await stockSearcher.GetAllMaterialBy(centroId, almacenId);
        }

        public async Task<IList<Material>> GetAllMaterial()
        {
            return await stockSearcher.GetAllMaterial();
        }

        public async Task<IList<Material>> GetAllMaterialBy(int stockEspecialId)
        {
            return await stockSearcher.GetAllMaterialBy(stockEspecialId);
        }

        public async Task<IList<Material>> GetAllMaterialBy(int centroId, string ubicacion)
        {
            var stockEspecialesIds = (await stockEspecialSearcher.GetByCodigos("S", "Q")).Select(x => x.Id).ToList();
            return await stockSearcher.GetAllMaterialBy(centroId, ubicacion, stockEspecialesIds);
        }

        public async Task<IList<Material>> GetAllMaterialExceptBy(int stockEspecialId)
        {
            return await stockSearcher.GetAllMaterialExceptBy(stockEspecialId);
        }
        
        public async Task<IList<Material>> GetAllMaterialExceptWithStockCalidad(int stockEspecialId)
        {
            return await stockSearcher.GetAllMaterialExceptWithStockCalidad(stockEspecialId);
        }

        public async Task<IList<Material>> GetAllMaterialExceptBy(params int[] listStockEspecialId)
        {
            return await stockSearcher.GetAllMaterialExceptBy(listStockEspecialId);
        }

        public async Task<IList<Stock>> GetBy(int materialId)
        {
            return await stockSearcher.GetBy(materialId);
        }
        public async Task<IList<Stock>> GetBy(int materialId, string claseValoracion)
        {
            return await stockSearcher.GetBy(materialId, claseValoracion);
        }
        public async Task<IList<Stock>> GetBy(int materialId, string claseValoracion, int stockEspecialId)
        {
            return await stockSearcher.GetBy(materialId, claseValoracion, stockEspecialId);
        }

        public async Task<IList<Stock>> GetByAndExceptStockEspecial(int materialId, int? claseValoracionId, int stockEspecialId)
        {
            return await stockSearcher.GetByAndExceptStockEspecial(materialId, claseValoracionId, stockEspecialId);
        }

        public async Task<IList<Stock>> GetByAndExceptStockEspecial(int materialId, int? claseValoracionId, params int[] listStockEspecialId)
        {
            return await stockSearcher.GetByAndExceptStockEspecial(materialId, claseValoracionId, listStockEspecialId);
        }

        public async Task<IList<string>> GetAllUbicaciones(int centroId)
        {
            return await stockSearcher.GetAllUbicaciones(centroId);
        }

        public async Task<IList<ClaseDeValoracion>> GetAllClaseDeValoracionBy(int materialId, string ubicacion)
        {
            return (await stockSearcher.GetByUbicacion(materialId, ubicacion)).Select(x => x.ClaseDeValoracion)
                .GroupBy(claseDeValoracion => claseDeValoracion.Id).Select(group => group.First()).ToList();
        }

        public async Task<string> GetUbicacionBy(int centroId,int materialId, int almacenId, int loteId)
        {
            return await stockSearcher.GetUbicacionBy(centroId, materialId, almacenId, loteId);
        }
    }
}
