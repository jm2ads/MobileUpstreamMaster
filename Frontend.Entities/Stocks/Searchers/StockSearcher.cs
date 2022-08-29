using Frontend.Business.Almacenes;
using Frontend.Business.Commons;
using Frontend.Business.DetallesStocksEspeciales.Searchers;
using Frontend.Business.Materiales;
using Frontend.Business.Materiales.Searchers;
using Frontend.Commons.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Business.Stocks.Searchers
{
    public class StockSearcher
    {
        private readonly IRepository<Stock> repository;
        private readonly DetalleStockEspecialSearcher detalleStockEspecialSearcher;
        private readonly MaterialSearcher materialSearcher;

        public StockSearcher(IRepository<Stock> repository, DetalleStockEspecialSearcher detalleStockEspecialSearcher,
            MaterialSearcher materialSearcher)
        {
            this.repository = repository;
            this.detalleStockEspecialSearcher = detalleStockEspecialSearcher;
            this.materialSearcher = materialSearcher;
        }

        public async Task<Stock> GetById(int id)
        {
            return await repository.GetWithChildren(id);
        }

        public async Task<IList<Stock>> GetAllByIds(IList<int> idList)
        {
            return await repository.GetAllByIds(idList);
        }

        public async Task<IList<Stock>> GetAllByIdMaterial(int id)
        {
            return await repository.Where(x => x.IdMaterial == id);
        }

        public async Task<IList<Stock>> GetBy(int idCentro, Nullable<int> idAlmacen, Nullable<int> idClaseDeValoracion, Nullable<int> idMaterial, Nullable<int> idGrupoDeArticulo)
        {
            List<Stock> list = new List<Stock>();
            List<Stock> listRet = new List<Stock>();

            list.AddRange(await repository.Where(x => x.IdCentro == idCentro
            && (x.CantidadAlmacen > 0
            || x.CantidadBloqueado > 0
            || x.CantidadCalidad > 0)));

            if (idMaterial.HasValue)
            {
                list = (list.Where(x => x.IdMaterial == idMaterial.Value).ToList());
            }

            if (idAlmacen.HasValue)
            {
                list = (list.Where(x => x.IdAlmacen == idAlmacen.Value).ToList());
            }
            if (idClaseDeValoracion.HasValue)
            {
                list = (list.Where(x => x.IdClaseDeValoracion == idClaseDeValoracion.Value).ToList());
            }
            foreach (var item in list)
            {
                listRet.Add(await repository.GetWithChildren(item.Id));
            }
            return listRet;
        }

        public async Task<IList<Stock>> GetBy(int? idAlmacen, int? idClaseDeValoracion, int? idMaterial, string ubicacion, string pep)
        {
            var materialHasValue = idMaterial.HasValue;
            var almacenHasValue = idAlmacen.HasValue;
            var claseDeValoracionHasValue = idClaseDeValoracion.HasValue;
            var ubicacionHasValue = !string.IsNullOrWhiteSpace(ubicacion);

            var list = await repository.FindWithChildren(x => (materialHasValue && x.IdMaterial == idMaterial)
               && (!almacenHasValue || x.IdAlmacen == idAlmacen)
               && (!claseDeValoracionHasValue || x.IdClaseDeValoracion == idClaseDeValoracion)
               && (!ubicacionHasValue || x.Ubicacion == ubicacion));

            return string.IsNullOrEmpty(pep) ? list : list.Where(x => x.DetalleStockEspecial.Detalle == pep).ToList();
        }

        public async Task<IList<string>> GetAllCodigoMaterialAutocomplete(int centroId, int? almacenId, int stockEspecialId)
        {
            return (await GetAllMaterialBy(centroId, almacenId, stockEspecialId)).Select(x => x.Codigo).Distinct().ToList();
        }

        public async Task<IList<Stock>> GetByCodigoMaterial(int centroId, int? almacenId, int stockEspecialId, string searchValue)
        {
            var material = await materialSearcher.GetByCodigo(searchValue);
            var detalleStockEspecialIds = (await detalleStockEspecialSearcher.GetByStockEspecialId(stockEspecialId)).Select(x => x.Id).ToList();

            var stockList = new List<Stock>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (detalleStockEspecialIds.Count / tamanioPagina); i++)
            {
                var list = detalleStockEspecialIds.Skip(i * tamanioPagina).Take(tamanioPagina);

                stockList.AddRange(await repository.FindWithChildren(x => x.IdCentro == centroId
                    && x.IdAlmacen == almacenId
                    && x.IdMaterial == material.Id
                    && list.Contains(x.IdDetalleStockEspecial)));
            }

            return stockList;
        }

        public async Task<IList<Stock>> GetBy(int centroId, int? almacenId, int stockEspecialId, string codigoMaterial, int? claseValoracionId)
        {
            var material = await materialSearcher.GetByCodigo(codigoMaterial);
            var detalleStockEspecialIds = (await detalleStockEspecialSearcher.GetByStockEspecialId(stockEspecialId)).Select(x => x.Id).ToList();

            var stockList = new List<Stock>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (detalleStockEspecialIds.Count / tamanioPagina); i++)
            {
                var list = detalleStockEspecialIds.Skip(i * tamanioPagina).Take(tamanioPagina);

                if (claseValoracionId == null)
                {
                    stockList.AddRange(await repository.FindWithChildren(x => x.IdCentro == centroId
                        && x.IdAlmacen == almacenId
                        && x.IdMaterial == material.Id
                        && list.Contains(x.IdDetalleStockEspecial)));
                }
                else
                {
                    stockList.AddRange(await repository.FindWithChildren(x => x.IdCentro == centroId
                        && x.IdAlmacen == almacenId
                        && x.IdMaterial == material.Id
                        && x.IdClaseDeValoracion == claseValoracionId
                        && list.Contains(x.IdDetalleStockEspecial)));
                }
            }

            return stockList;
        }

        public async Task<IList<Stock>> GetBy(int centroId, string ubicacion, string codigoMaterial, string claseValoracion)
        {
            var material = await materialSearcher.GetByCodigo(codigoMaterial);

            if (String.IsNullOrEmpty(claseValoracion))
            {
                return await repository.FindWithChildren(x => x.IdCentro == centroId
                && x.Ubicacion == ubicacion
                && x.IdMaterial == material.Id);
            }
            else
            {
                return await repository.FindWithChildren(x => x.IdCentro == centroId
                && x.Ubicacion == ubicacion
                && x.IdMaterial == material.Id
                && x.ClaseDeValoracion.Codigo == claseValoracion);
            }
        }

        public async Task<IList<string>> GetAllDescripcionMaterialAutocomplete(int centroId, int? almacenId, int stockEspecialId)
        {
            return (await GetAllMaterialBy(centroId, almacenId, stockEspecialId)).Select(x => x.Descripcion).Distinct().ToList();
        }

        public async Task<IList<Material>> GetAllMaterialBy(int centroId, int? almacenId, int stockEspecialId)
        {
            var detalleStockEspecialIds = (await detalleStockEspecialSearcher.GetByStockEspecialId(stockEspecialId)).Select(x => x.Id).ToList();

            var stockList = new List<Stock>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (detalleStockEspecialIds.Count / tamanioPagina); i++)
            {
                var list = detalleStockEspecialIds.Skip(i * tamanioPagina).Take(tamanioPagina);

                stockList.AddRange(await repository.Where(x => x.IdCentro == centroId
                && x.IdAlmacen == almacenId
                && list.Contains(x.IdDetalleStockEspecial)));
            }

            return await materialSearcher.GetAllByIds(stockList.Select(x => x.IdMaterial).Distinct().ToList());
        }
        
        public async Task<IList<Material>> GetAllMaterialBy(int centroId, IList<int> listAlmacenesIds)
        {
            var stockList = new List<Stock>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (listAlmacenesIds.Count / tamanioPagina); i++)
            {
                var list = listAlmacenesIds.Skip(i * tamanioPagina).Take(tamanioPagina);

                stockList.AddRange(await repository.Where(x => x.IdCentro == centroId
                && (x.IdAlmacen != null && list.Contains((int)x.IdAlmacen))));
            }

            return await materialSearcher.GetAllByIds(stockList.Select(x => x.IdMaterial).Distinct().ToList());
        }

        public async Task<IList<Material>> GetAllMaterialBy(int centroId, int? almacenId)
        {
            var stockList = await repository.Where(x => x.IdCentro == centroId
                && (almacenId == null || x.IdAlmacen == almacenId));

            return await materialSearcher.GetAllByIds(stockList.Select(x => x.IdMaterial).Distinct().ToList());
        }

        public async Task<IList<Material>> GetAllMaterialBy(int centroId, string ubicacion, List<int> stockEspecialesIds)
        {
            var detalleStockEspecialIds = (await detalleStockEspecialSearcher.GetAllByCodigos(stockEspecialesIds));

            var retList = new List<Stock>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (detalleStockEspecialIds.Count / tamanioPagina); i++)
            {
                var list = detalleStockEspecialIds.Skip(i * tamanioPagina).Take(tamanioPagina);
                if (String.IsNullOrEmpty(ubicacion))
                {
                    retList.AddRange((await repository.Where(x => list.Contains(x.IdDetalleStockEspecial))));
                }
                else
                {
                    retList.AddRange((await repository.Where(x => x.Ubicacion != null && x.Ubicacion == ubicacion && list.Contains(x.IdDetalleStockEspecial))));
                }
            }

            return await materialSearcher.GetAllByIds(retList.Select(x => x.IdMaterial).Distinct().ToList());
        }

        public async Task<IList<Stock>> GetByDescripcionMaterial(int centroId, int? almacenId, int stockEspecialId, string searchValue)
        {
            var material = await materialSearcher.GetByDescripcion(searchValue);
            var detalleStockEspecialIds = (await detalleStockEspecialSearcher.GetByStockEspecialId(stockEspecialId)).Select(x => x.Id).ToList();

            var stockList = new List<Stock>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (detalleStockEspecialIds.Count / tamanioPagina); i++)
            {
                var list = detalleStockEspecialIds.Skip(i * tamanioPagina).Take(tamanioPagina);

                stockList.AddRange(await repository.FindWithChildren(x => x.IdCentro == centroId
                && x.IdAlmacen == almacenId
                && x.IdMaterial == material.Id
                && list.Contains(x.IdDetalleStockEspecial)));
            }

            return stockList;
        }

        public async Task<IList<Stock>> GetByDescripcionMaterial(int centroId, string ubicacion, string searchValue)
        {
            var material = await materialSearcher.GetByDescripcion(searchValue);

            var stock = await repository.FindWithChildren(x => x.IdCentro == centroId
                && x.Ubicacion == ubicacion
                && x.IdMaterial == material.Id);
            return stock;
        }

        public async Task<IList<Material>> GetAllMaterial()
        {
            var detalleStockEspecialIds = (await detalleStockEspecialSearcher.GetAll()).Select(x => x.Id).ToList();

            var stockList = new List<Stock>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (detalleStockEspecialIds.Count / tamanioPagina); i++)
            {
                var list = detalleStockEspecialIds.Skip(i * tamanioPagina).Take(tamanioPagina);

                stockList.AddRange(await repository.Where(x => list.Contains(x.IdDetalleStockEspecial)));
            }

            return await materialSearcher.GetAllByIds(stockList.Select(x => x.IdMaterial).Distinct().ToList());
        }

        public async Task<IList<Stock>> GetBy(int materialId)
        {
            var list = (await repository.FindWithChildren(x => x.IdMaterial == materialId)).Distinct().ToList();
            return list;
        }

        public async Task<IList<Stock>> GetBy(int materialId, string claseValoracion)
        {
            if (String.IsNullOrEmpty(claseValoracion))
            {
                return (await repository.FindWithChildren(x => x.IdMaterial == materialId)).Distinct().ToList();
            }
            else
            {
                return (await repository.FindWithChildren(x => x.IdMaterial == materialId && x.ClaseDeValoracion.Codigo == claseValoracion)).Distinct().ToList();
            }
        }
        public async Task<IList<Stock>> GetByUbicacion(int materialId, string ubicacion)
        {
            if (String.IsNullOrEmpty(ubicacion))
            {
                return (await repository.FindWithChildren(x => x.IdMaterial == materialId )).Distinct().ToList();
            }
            else
            {
                return (await repository.FindWithChildren(x => x.IdMaterial == materialId && x.Ubicacion == ubicacion)).Distinct().ToList();
            }
        }

        public async Task<IList<Stock>> GetBy(int materialId, string claseValoracion, int stockEspecialId)
        {
            var detalleStockEspecialIds = (await detalleStockEspecialSearcher.GetByStockEspecialId(stockEspecialId)).Select(x => x.Id).ToList();

            var stockList = new List<Stock>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (detalleStockEspecialIds.Count / tamanioPagina); i++)
            {
                var list = detalleStockEspecialIds.Skip(i * tamanioPagina).Take(tamanioPagina);
                if (String.IsNullOrEmpty(claseValoracion))
                {
                    stockList.AddRange(await repository.FindWithChildren(x => x.IdMaterial == materialId
                    && list.Contains(x.IdDetalleStockEspecial)));
                }
                else
                {
                    stockList.AddRange(await repository.FindWithChildren(x => x.IdMaterial == materialId
                    && x.ClaseDeValoracion.Codigo == claseValoracion
                    && list.Contains(x.IdDetalleStockEspecial)));
                }
            }
            return stockList;
        }

        public async Task<IList<Stock>> GetByAndExceptStockEspecial(int materialId, int? claseValoracionId, int stockEspecialId)
        {
            var detalleStockEspecialIds = (await detalleStockEspecialSearcher.GetExceptByStockEspecialId(stockEspecialId)).Select(x => x.Id).ToList();

            var stockList = new List<Stock>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (detalleStockEspecialIds.Count / tamanioPagina); i++)
            {
                var list = detalleStockEspecialIds.Skip(i * tamanioPagina).Take(tamanioPagina);
                if (!claseValoracionId.HasValue)
                {
                    stockList.AddRange(await repository.FindWithChildren(x => x.IdMaterial == materialId
                    && list.Contains(x.IdDetalleStockEspecial)));
                }
                else
                {
                    stockList.AddRange(await repository.FindWithChildren(x => x.IdMaterial == materialId
                    && x.IdClaseDeValoracion == claseValoracionId
                    && list.Contains(x.IdDetalleStockEspecial)));
                }
            }
            return stockList;
        }

        public async Task<IList<Stock>> GetByAndExceptStockEspecial(int materialId, int? claseValoracionId, params int[] listStockEspecialId)
        {
            var detalleStockEspecialIds = (await detalleStockEspecialSearcher.GetExceptByStockEspecialId(listStockEspecialId)).Select(x => x.Id).ToList();

            var stockList = new List<Stock>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (detalleStockEspecialIds.Count / tamanioPagina); i++)
            {
                var list = detalleStockEspecialIds.Skip(i * tamanioPagina).Take(tamanioPagina);
                if (!claseValoracionId.HasValue)
                {
                    stockList.AddRange(await repository.FindWithChildren(x => x.IdMaterial == materialId
                    && list.Contains(x.IdDetalleStockEspecial)));
                }
                else
                {
                    stockList.AddRange(await repository.FindWithChildren(x => x.IdMaterial == materialId
                    && x.IdClaseDeValoracion == claseValoracionId
                    && list.Contains(x.IdDetalleStockEspecial)));
                }
            }
            return stockList;
        }

        public async Task<IList<Material>> GetAllMaterialBy(int stockEspecialId)
        {
            var detalleStockEspecialIds = (await detalleStockEspecialSearcher.GetByStockEspecialId(stockEspecialId)).Select(x => x.Id).ToList();

            var stockList = new List<Stock>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (detalleStockEspecialIds.Count / tamanioPagina); i++)
            {
                var list = detalleStockEspecialIds.Skip(i * tamanioPagina).Take(tamanioPagina);
                stockList.AddRange(await repository.Where(x => list.Contains(x.IdDetalleStockEspecial)));
            }
            return await materialSearcher.GetAllByIds(stockList.Select(x => x.IdMaterial).Distinct().ToList());
        }

        public async Task<IList<Material>> GetAllMaterialExceptBy(int stockEspecialId)
        {
            var detalleStockEspecialIds = (await detalleStockEspecialSearcher.GetExceptByStockEspecialId(stockEspecialId)).Select(x => x.Id).ToList();
            var stockList = new List<Stock>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (detalleStockEspecialIds.Count / tamanioPagina); i++)
            {
                var list = detalleStockEspecialIds.Skip(i * tamanioPagina).Take(tamanioPagina);
                stockList.AddRange(await repository.Where(x => list.Contains(x.IdDetalleStockEspecial)));
            }
            return await materialSearcher.GetAllByIds(stockList.Select(x => x.IdMaterial).Distinct().ToList());
        }
        
        public async Task<IList<Material>> GetAllMaterialExceptWithStockCalidad(int stockEspecialId)
        {
            var detalleStockEspecialIds = (await detalleStockEspecialSearcher.GetExceptByStockEspecialId(stockEspecialId)).Select(x => x.Id).ToList();
            var stockList = new List<Stock>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (detalleStockEspecialIds.Count / tamanioPagina); i++)
            {
                var list = detalleStockEspecialIds.Skip(i * tamanioPagina).Take(tamanioPagina);
                stockList.AddRange(await repository.Where(x => list.Contains(x.IdDetalleStockEspecial)));
            }
            return await materialSearcher.GetAllByIds(stockList.Where(st => st.CantidadCalidad > 0).Select(x => x.IdMaterial).Distinct().ToList());
        }

        public async Task<IList<Material>> GetAllMaterialExceptBy(params int[] listStockEspecialId)
        {
            var listDetalleStockEspecialIds = (await detalleStockEspecialSearcher.GetExceptByStockEspecialId(listStockEspecialId)).Select(x => x.Id).ToList();
            var stockList = new List<Stock>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (listDetalleStockEspecialIds.Count / tamanioPagina); i++)
            {
                var list = listDetalleStockEspecialIds.Skip(i * tamanioPagina).Take(tamanioPagina);
                stockList.AddRange(await repository.Where(x => list.Contains(x.IdDetalleStockEspecial)));
            }
            return await materialSearcher.GetAllByIds(stockList.Select(x => x.IdMaterial).Distinct().ToList());
        }

        public async Task<IList<string>> GetAllUbicaciones(int centroId)
        {
            var list = (await repository.Where(x => x.IdCentro == centroId
            && x.Ubicacion != string.Empty)).Select(x => x.Ubicacion).Distinct().ToList();
            return list;
        }

        public async Task<string> GetUbicacionBy(int centroId, int materialId, int almacenId, int loteId)
        {
            var stock = (await repository.Where(x => x.IdCentro == centroId
                                                    && x.IdMaterial == materialId
                                                    && x.IdAlmacen == almacenId
                                                    && x.IdClaseDeValoracion == loteId)).FirstOrDefault();
            return stock?.Ubicacion;
        }
    }
}
