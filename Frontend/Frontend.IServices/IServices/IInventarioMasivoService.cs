using Frontend.Business.Almacenes;
using Frontend.Business.Centros;
using Frontend.Business.InventariosMasivos;
using Frontend.Business.Synchronizer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IInventarioMasivoService
    {
        Task Insert(InventarioMasivo inventarioMasivo, SyncState syncState = SyncState.New);
        Task Update(InventarioMasivo inventarioMasivo, SyncState syncState = SyncState.Updated);
        Task Delete(InventarioMasivo inventarioMasivo);
        bool Validate(InventarioMasivo inventarioMasivo);
        bool ValidateDistribuido(InventarioMasivo inventarioMasivo);
        Task Delete(DetalleInventarioMasivo detalleInventarioMasivo);
        Task<DetalleInventarioMasivo> Duplicar(DetalleInventarioMasivo detalleInventarioMasivo);
        InventarioMasivo Create(Centro centro);
        Task<IList<InventarioMasivoOrden>> GetOrden();
        Task<List<Almacen>> GetAlmacenes();
        Task<InventarioMasivo> Distribuir(InventarioMasivo inventarioMasivo);
        Task SumarCantidades(InventarioMasivo inventarioMasivo, int posicion, double cantidad);
        Task<List<Almacen>> GetAlmacenes(int materialId);
        Task<IList<InventarioMasivo>> GetAllProvisoriosWithChildren();
        Task Save(InventarioMasivo inventarioMasivo);
    }
}
