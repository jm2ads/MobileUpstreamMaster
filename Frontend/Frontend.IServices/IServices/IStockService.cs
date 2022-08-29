using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.Materiales;
using Frontend.Business.Stocks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IStockService
    {
        Task<IList<Stock>> GetBy(int centro, Nullable<int> idAlmacen, Nullable<int> idClaseDeValoracion, Nullable<int> idMaterial, Nullable<int> idGrupoDeArticulo);
        Task<IList<string>> GetAllCodigoMaterialAutocomplete(int centroId, int? almacenId, int stockEspecialId);
        Task<IList<Stock>> GetByCodigoMaterial(int centroId, int? almacenId, int stockEspecialId, string searchValue);
        Task<IList<Stock>> GetBy(int centroId, int? almacenId, int stockEspecialId, string codigoMaterial, int? claseValoracionId);
        Task<IList<string>> GetAllDescripcionMaterialAutocomplete(int centroId, int? almacenId, int stockEspecialId);
        Task<IList<Stock>> GetByDescripcionMaterial(int centroId, int? almacenId, int stockEspecialId, string searchValue);
        Task<IList<Material>> GetAllMaterialBy(int centroId, int? almacenId, int stockEspecialId);
        Task<IList<Material>> GetAllMaterialBy(int centroId, IList<int> listAlmacenIds);
        Task<IList<Material>> GetAllMaterialBy(int centroId, int? almacenId);
        Task<IList<Material>> GetAllMaterial();
        Task<IList<Stock>> GetBy(int materialId);
        Task<IList<Stock>> GetBy(int materialId, string claseValoracion);
        Task<IList<Stock>> GetBy(int materialId, string claseValoracion, int stockEspecialId);
        Task<IList<Stock>> GetByAndExceptStockEspecial(int materialId, int? claseValoracion, int stockEspecialId);
        Task<IList<Stock>> GetByAndExceptStockEspecial(int materialId, int? claseValoracion, params int[] listStockEspecialId);
        Task<IList<Material>> GetAllMaterialBy(int stockEspecialId);
        Task<IList<Material>> GetAllMaterialExceptBy(int stockEspecialId);
        Task<IList<Material>> GetAllMaterialExceptWithStockCalidad(int stockEspecialId);
        Task<IList<Material>> GetAllMaterialExceptBy(params int[] listStockEspecialId);
        Task<IList<Material>> GetAllMaterialBy(int centroId, string ubicacion);
        Task<IList<Stock>> GetBy(int centroId, string ubicacion, string codigoMaterial, string claseValoracion);
        Task<IList<Stock>> GetByDescripcionMaterial(int centroId, string ubicacion, string searchValue);
        Task<IList<string>> GetAllUbicaciones(int centroId);
        Task<IList<ClaseDeValoracion>> GetAllClaseDeValoracionBy(int materialId, string ubicacion);
        Task<string> GetUbicacionBy(int centroId, int materialId, int almacenId, int loteId);

    }
}
