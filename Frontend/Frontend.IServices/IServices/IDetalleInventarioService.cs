using Frontend.Business.DetallesInventario;
using Frontend.Business.Inventarios;
using Frontend.Business.LecturaQRs;
using Frontend.Business.Stocks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IDetalleInventarioService
    {
        Task<DetalleInventario> Create(Inventario inventario, List<Stock> stocksDisponibles, LecturaQR lecturaQR);
        Task Update(DetalleInventario detalleInventario);
        Task Update(IList<DetalleInventario> detalleInventarios);
        DetalleInventario Duplicar(DetalleInventario detalleInventario);
        Task Delete(DetalleInventario detalleInventario);
        Task DeleteAll();
        Task<IList<Inventario>> GetInventariosByIdMaterial(int idMaterial);
        Task<IList<Inventario>> GetInventariosByIdMaterial(LecturaQR lecturaQR);
        Task<DetalleInventario> GetByIdMaterial(int idMaterial);
        Task<IList<string>> GetAllDescripcionMaterialAutocompleteRecuento();
        Task<DetalleInventario> GetMaterialByCodigo(string searchValue);
        Task<DetalleInventario> GetMaterialByDescripcion(string searchValue);
        Task<DetalleInventario> GetMaterialByCodigoRecuento(string searchValue);
        Task<DetalleInventario> GetMaterialByDescripcionRecuento(string searchValue);
        Task<IList<DetalleInventario>>GetByIdInventario(int idInventario);
        Task<IList<DetalleInventario>>GetDetallesAprobacionMasiva();
        Task<DetalleInventario> GetById(int id);
    }
}
