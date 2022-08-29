
using Frontend.Business.LecturaQRs;
using Frontend.Business.Materiales;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.SalidasInternas;
using Frontend.Business.Synchronizer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface ISalidaInternaService
    {
        Task Update(SalidaInterna salidaInterna, SyncState syncState = SyncState.Updated);
        Task Update(DetalleSalidaInterna detalleSalidaInterna);
        bool Validate(SalidaInterna salidaInterna);
        bool HasContados(SalidaInterna salidaInterna);
        bool Validate(DetalleSalidaInterna detalleSalidaInterna);
        Task<IList<Material>> GetAllMaterialBy(EstadoMovimiento recibir, string claseMovimientoCodigo);
        Task<IList<int>> GetSalidasBy(string codigoMaterial, string claseValoracion);
        Task<IList<int>> GetSalidasDescripcionMaterial(string searchValue);
        Task<IList<SalidaInterna>> GetAllByIds(IList<int> salidasIds, EstadoMovimiento estado);
        Task<SalidaInterna> GetWithChildren(int id);
        Task<IList<SalidaInterna>> GetAllBy(EstadoMovimiento estadoIngreso, string claseMovimientoCodigo);
        Task<IList<SalidaInterna>> GetAllBy(LecturaQR lecturaQR, string claseMovimientoCodigo);
    }
}
