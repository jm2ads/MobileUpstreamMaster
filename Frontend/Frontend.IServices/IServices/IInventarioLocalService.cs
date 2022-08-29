using Frontend.Business.DetallesInventarioLocal;
using Frontend.Business.Inventarios;
using Frontend.Business.InventariosLocales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IInventarioLocalService
    {
        Task<InventarioLocal> Create();
        Task<InventarioLocal> Save(InventarioLocal inventario);
        Task Delete(InventarioLocal inventario);
        Task<IList<InventarioLocal>> GetAllProvisorios();
        Task<InventarioLocal> GetInventarioById(int id);
        Task SetToPendienteAprobacion(InventarioLocal inventario);
        DetalleInventarioLocal GetDetalleInventarioDuplicated(InventarioLocal inventario, DetalleInventarioLocal detalleInventario);
        Task SetToRechazadoParcial(Inventario inventario, string comentario);
        Task DeleteDetalleLocal(DetalleInventarioLocal detalleInventarioLocal);
    }
}
