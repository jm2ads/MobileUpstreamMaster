using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend.Business.Commons;
using Frontend.Business.LecturaQRs;
using Frontend.Business.Materiales;
using Frontend.Business.Materiales.Searchers;

namespace Frontend.Business.Movimientos.SalidasInternas.Searchers
{
    public class DetalleSalidaInternaSearcher
    {
        private readonly IRepository<DetalleSalidaInterna> repository;
        private readonly MaterialSearcher materialSearcher;
        private readonly SalidaInternaSearcher salidaInternaSearcher;
        public DetalleSalidaInternaSearcher(IRepository<DetalleSalidaInterna> repository, SalidaInternaSearcher salidaInternaSearcher, MaterialSearcher materialSearcher)
        {
            this.repository = repository;
            this.salidaInternaSearcher = salidaInternaSearcher;
            this.materialSearcher = materialSearcher;
        }

        public async Task<IList<DetalleSalidaInterna>> GetAllBy(EstadoMovimiento estadoIngreso, string claseMovimientoCodigo)
        {
            var salidas = await salidaInternaSearcher.GetAllBy(estadoIngreso, claseMovimientoCodigo);
            var salidasIds = salidas.Select(x => x.Id);
            return await repository.Where(x => salidasIds.Contains(x.SalidaInternaId));
        }

        public async Task<IList<Material>> GetAllMaterialByAsync(EstadoMovimiento estadoIngreso, string claseMovimientoCodigo)
        {
            var detalles = await GetAllBy(estadoIngreso, claseMovimientoCodigo);
            var materialesIds = detalles.Select(x => x.MaterialId).Distinct().ToList();
            return await materialSearcher.GetAllByIds(materialesIds);
        }

        public async Task<IList<int>> GetSalidaIdsBy(string codigoMaterial, string claseValoracion)
        {
            var detalles = await repository.GetAllWithChildren();
            if (String.IsNullOrEmpty(claseValoracion))
            {
                return detalles.Where(x => x.Material.Codigo.TrimStart('0').ToUpper() == codigoMaterial.ToUpper()).Select(x => x.SalidaInternaId).Distinct().ToList();
            }
            else
            {
                return detalles.Where(x => x.Material.Codigo.TrimStart('0').ToUpper() == codigoMaterial.ToUpper() && x.ClaseDeValoracion.Codigo.ToUpper() == claseValoracion.ToUpper()).Select(x => x.SalidaInternaId).Distinct().ToList();
            }
        }

        public async Task<IList<int>> GetSalidaIdsDescripcionMaterial(string searchValue)
        {
            var detalles = await repository.GetAllWithChildren();
            var salidasIds = detalles.Where(x => x.Material.Descripcion.ToUpper() == searchValue.ToUpper()).Select(x => x.SalidaInternaId).Distinct().ToList();
            return salidasIds;
        }
        public async Task<IList<DetalleSalidaInterna>> GetDetallesIdsBy(LecturaQR lecturaQR)
        {
            var materialIdHasValue = lecturaQR.MaterialId.HasValue;
            var almacenIdHasValue = lecturaQR.AlmacenId.HasValue;
            var loteIdHasValue = lecturaQR.LoteId.HasValue;
            return await repository.Where(x => (materialIdHasValue && x.MaterialId == lecturaQR.MaterialId)
            && (!almacenIdHasValue || x.AlmacenId == lecturaQR.AlmacenId)
            && (!loteIdHasValue || x.ClaseDeValoracionId == lecturaQR.LoteId));
        }

    }
}
