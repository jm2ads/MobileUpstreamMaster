using Frontend.Business.LecturaQRs;
using Frontend.Business.Materiales;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.SalidasInternas;
using Frontend.Business.Movimientos.SalidasInternas.Core;
using Frontend.Business.Movimientos.SalidasInternas.Searchers;
using Frontend.Business.Movimientos.SalidasInternas.Validators;
using Frontend.Business.Synchronizer;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class SalidaInternaService : ISalidaInternaService
    {
        private readonly SalidaInternaSearcher salidaInternaSearcher;
        private readonly DetalleSalidaInternaSearcher detalleSalidaInternaSearcher;
        private readonly SalidaInternaValidator salidaInternaValidator;
        private readonly SalidaInternaUpdater salidaInternaUpdater;
        private readonly DetalleSalidaInternaUpdater detalleSalidaInternaUpdater;
        private readonly DetalleSalidaInternaValidator detalleSalidaInternaValidator;
        private readonly SalidaInternaFactory salidaInternaFactory;

        public SalidaInternaService(SalidaInternaSearcher salidaInternaSearcher, DetalleSalidaInternaSearcher detalleSalidaInternaSearcher, SalidaInternaValidator salidaInternaValidator,
            SalidaInternaUpdater salidaInternaUpdater, SalidaInternaFactory salidaInternaFactory, SalidaInternaGenerator salidaInternaGenerato, DetalleSalidaInternaUpdater detalleSalidaInternaUpdater,
            DetalleSalidaInternaValidator detalleSalidaInternaValidator)
        {
            this.salidaInternaSearcher = salidaInternaSearcher;
            this.detalleSalidaInternaSearcher = detalleSalidaInternaSearcher;
            this.salidaInternaValidator = salidaInternaValidator;
            this.salidaInternaUpdater = salidaInternaUpdater;
            this.salidaInternaFactory = salidaInternaFactory;
            this.detalleSalidaInternaUpdater = detalleSalidaInternaUpdater;
            this.detalleSalidaInternaValidator = detalleSalidaInternaValidator;
        }

        public async Task<IList<SalidaInterna>> GetAllByIds(IList<int> salidasIds, EstadoMovimiento estado)
        {
            var salidas = await salidaInternaSearcher.GetAllByIdsAsync(salidasIds);
            return salidas.Where(x => x.Estado == estado).ToList();
        }

        public async Task<IList<Material>> GetAllMaterialBy(EstadoMovimiento estado, string claseMovimientoCodigo)
        {
            return await detalleSalidaInternaSearcher.GetAllMaterialByAsync(estado, claseMovimientoCodigo);
        }

        public async Task<IList<int>> GetSalidasBy(string codigoMaterial, string claseValoracion)
        {
            return await detalleSalidaInternaSearcher.GetSalidaIdsBy(codigoMaterial, claseValoracion);
        }

        public async Task<IList<int>> GetSalidasDescripcionMaterial(string searchValue)
        {
            return await detalleSalidaInternaSearcher.GetSalidaIdsDescripcionMaterial(searchValue);
        }

        public async Task<SalidaInterna> GetWithChildren(int id)
        {
            return await salidaInternaSearcher.GetById(id);
        }
        public async Task<IList<SalidaInterna>> GetAllBy(EstadoMovimiento estadoIngreso, string claseMovimientoCodigo)
        {
            return await salidaInternaSearcher.GetAllBy(estadoIngreso, claseMovimientoCodigo);
        }

        public async Task Update(SalidaInterna salidaInterna, SyncState syncState = SyncState.Updated)
        {
            await salidaInternaUpdater.Update(salidaInterna, syncState);
        }

        public async Task Update(DetalleSalidaInterna detalleSalidaInterna)
        {
            await detalleSalidaInternaUpdater.Update(detalleSalidaInterna);
        }

        public bool Validate(SalidaInterna salidaInterna)
        {
            return salidaInternaValidator.Validate(salidaInterna);
        }

        public bool HasContados(SalidaInterna salidaInterna)
        {
            return salidaInternaValidator.HasContados(salidaInterna);
        }

        public bool Validate(DetalleSalidaInterna detalleSalidaInterna)
        {
            return detalleSalidaInternaValidator.Validate(detalleSalidaInterna);
        }

        public async Task<IList<SalidaInterna>> GetAllBy(LecturaQR lecturaQR, string claseMovimientoCodigo)
        {
            var detalles = await detalleSalidaInternaSearcher.GetDetallesIdsBy(lecturaQR);
            var salidas = await salidaInternaSearcher.GetAllByIdsAsync(detalles.Select(x => x.SalidaInternaId).Distinct().ToList());
            return salidas.Where(p => p.ClaseDeMovimientoCodigo == claseMovimientoCodigo 
            && (string.IsNullOrWhiteSpace(lecturaQR.NumeroMovimiento) || p.NumeroPedido == lecturaQR.NumeroMovimiento)).ToList();
        }
    }
}
