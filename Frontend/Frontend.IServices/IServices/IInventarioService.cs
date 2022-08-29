using Frontend.Business.DetallesInventario;
using Frontend.Business.Inventarios;
using Frontend.Business.Materiales;
using Frontend.Commons.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IInventarioService
    {
        Task<Inventario> Create();
        Task<Inventario> Generate(Inventario inventario);
        Task<Inventario> Save(Inventario inventario);
        Task UpdateWithChildren(Inventario inventario);
        Task Delete(Inventario inventario);
        Task DeleteAll();
        bool IsDuplicatedDetalleInventario(Inventario inventario, DetalleInventario detalleInventario);
        bool IsValidToFinish(Inventario inventario);
        DetalleInventario GetDetalleInventarioDuplicated(Inventario inventario, DetalleInventario detalleInventario);
        Task<IList<Inventario>> GetAllProvisorios();
        Task<IList<Inventario>> GetAllRechazados();
        Task<IList<Inventario>> GetAllSap();
        Task<IList<Inventario>> GetAllPendienteAprobacion();
        Task<IList<Inventario>> GetAllPendienteAprobacionWithChildren();
        Task<IList<Inventario>> GetAllPendienteAprobacionSap();
        Task<IList<Inventario>> GetAllPendienteAprobacionSapWithChildren();
        Task<Inventario> GetInventarioBy(string codigo, EstadoInventario estadoInventario);
        Task<Inventario> GetInventarioById(int id);
        Task SetToPendienteAprobacion(Inventario inventario);
        Task SetToPendienteAprobacionSap(Inventario inventario);
        Task SetToAprobado(Inventario inventario);
        Task SetToAprobadoParcial(Inventario inventario);
        Task SetToAprobado(IList<Inventario> listInventario);
        Task SetToRechazado(Inventario inventario);
        Task SetToRechazado(IList<Inventario> listInventario);
        Task SetToRechazadoParcial(Inventario inventario);
        Task<IList<string>> GetAllCodigoAutocomplete();
        Task<IList<InventarioLog>> Generate(IList<InventarioLog> listInventarioLog);
        Task<IList<InventarioLog>> GetAllInventarioLog();
        Task<IList<InventarioLog>> GetAllInventarioLogError();
        Task<IList<Material>> GetAllMaterialAutocompleteRecuento();
        Task SetComentario(IList<Inventario> listInventario, string comentario);
        Task SetComentario(Inventario inventario, string comentario);
        Task<IList<Inventario>> GetById(IList<Inventario> inventarios);
        Task DeleteLog(IList<int> listIdRemoto);
        Task DeleteLogOlderThan(int cantidadDias);
    }
}
