using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.InventariosMasivos;
using Frontend.Business.Materiales;
using Frontend.Business.Stocks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IDetalleInventarioMasivoService
    {
        Task<DetalleInventarioMasivo> Create(InventarioMasivo inventarioMasivo, Material material);
        Task<IList<string>> PepEditable(IList<Stock> listaStock, string ubicacion, int? almacenId, int? claseDeValoracionId);
        IList<ClaseDeValoracion> LoteEditable(IList<ClaseDeValoracion> lotes, int materialId);
        Task<bool> ValidateDuplicado(InventarioMasivo inventarioMasivo, DetalleInventarioMasivo detalleInventarioMasivo);
        Task<bool> ValidateMaterial(Material material);
    }
}
