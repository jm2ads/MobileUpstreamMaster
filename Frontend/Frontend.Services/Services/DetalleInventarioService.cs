using Frontend.Business.DetallesInventario;
using Frontend.Business.DetallesInventario.Core;
using Frontend.Business.DetallesInventario.Searchers;
using Frontend.Business.Inventarios;
using Frontend.Business.Inventarios.Searchers;
using Frontend.Business.LecturaQRs;
using Frontend.Business.Stocks;
using Frontend.Commons.Enums;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class DetalleInventarioService : IDetalleInventarioService
    {
        private readonly DetalleInventarioFactory detalleInventarioFactory;
        private readonly DetalleInventarioDeleter detalleInventarioDeleter;
        private readonly DetalleInventarioSearcher detalleInventarioSearcher;
        private readonly InventarioSearcher inventarioSearcher;
        private readonly IInventarioService inventarioService;
        private readonly IDetalleStockEspecialService detalleStockEspecialService;
        private readonly IClaseDeValoracionService claseDeValoracionService;
        private readonly DetalleInventarioUpdater detalleInventarioUpdater;

        public DetalleInventarioService(DetalleInventarioFactory detalleInventarioFactory, DetalleInventarioDeleter detalleInventarioDeleter, DetalleInventarioSearcher detalleInventarioSearcher,
            DetalleInventarioUpdater detalleInventarioUpdater, InventarioSearcher inventarioSearcher, IInventarioService inventarioService, IDetalleStockEspecialService detalleStockEspecialService,
            IClaseDeValoracionService claseDeValoracionService)
        {
            this.detalleInventarioFactory = detalleInventarioFactory;
            this.detalleInventarioDeleter = detalleInventarioDeleter;
            this.detalleInventarioSearcher = detalleInventarioSearcher;
            this.detalleInventarioUpdater = detalleInventarioUpdater;
            this.inventarioSearcher = inventarioSearcher;
            this.inventarioService = inventarioService;
            this.detalleStockEspecialService = detalleStockEspecialService;
            this.claseDeValoracionService = claseDeValoracionService;
        }

        public async Task<DetalleInventario> Create(Inventario inventario, List<Stock> stocksDisponibles, LecturaQR lecturaQR)
        {
            var detalleStockEspecial = await detalleStockEspecialService.GetByPEP(lecturaQR.PEP);
            var claseDeValoracion = lecturaQR.LoteId != null ? await claseDeValoracionService.GetBy(lecturaQR.LoteId.GetValueOrDefault()) : null;
            return detalleInventarioFactory.Create(inventario, stocksDisponibles, lecturaQR, claseDeValoracion, detalleStockEspecial);
        }

        public async Task Update(DetalleInventario detalleInventario)
        {
            await detalleInventarioUpdater.Update(detalleInventario);
        }

        public DetalleInventario Duplicar(DetalleInventario detalleInventario)
        {
            return detalleInventarioFactory.CreateCopy(detalleInventario);
        }

        public async Task Delete(DetalleInventario detalleInventario)
        {
            await detalleInventarioDeleter.Delete(detalleInventario);
        }

        public async Task Update(IList<DetalleInventario> detalleInventarios)
        {
            await detalleInventarioUpdater.Update(detalleInventarios);
        }

        public async Task DeleteAll()
        {
            await detalleInventarioDeleter.DeleteAll();
        }

        public async Task<IList<string>> GetAllDescripcionMaterialAutocompleteRecuento()
        {
            return await detalleInventarioSearcher.GetAllDescripcionMaterialAutocompleteRecuento();
        }

        public async Task<DetalleInventario> GetMaterialByCodigo(string searchValue)
        {
            return await detalleInventarioSearcher.GetMaterialByCodigo(searchValue);
        }

        public async Task<DetalleInventario> GetMaterialByDescripcion(string searchValue)
        {
            return await detalleInventarioSearcher.GetMaterialByDescripcion(searchValue);
        }

        public async Task<DetalleInventario> GetMaterialByCodigoRecuento(string searchValue)
        {
            return await detalleInventarioSearcher.GetMaterialByCodigoAndEstado(searchValue, Commons.Enums.EstadoInventario.Recuento);
        }

        public async Task<DetalleInventario> GetByIdMaterial(int idMaterial)
        {
            return await detalleInventarioSearcher.GetByIdMaterial(idMaterial);
        }

        public async Task<IList<Inventario>> GetInventariosByIdMaterial(int idMaterial)
        {
            var inventariosIds = await detalleInventarioSearcher.GetInventariosIdsByIdMaterial(idMaterial);
            return await inventarioSearcher.GetAllBy(inventariosIds, EstadoInventario.Recuento);
        }

        public async Task<IList<Inventario>> GetInventariosByIdMaterial(LecturaQR lecturaQR)
        {
            var inventariosIds = await detalleInventarioSearcher.GetInventariosIdsBy(lecturaQR);
            return await inventarioSearcher.GetAllBy(inventariosIds, EstadoInventario.Recuento);
        }

        public async Task<DetalleInventario> GetMaterialByDescripcionRecuento(string searchValue)
        {
            return await detalleInventarioSearcher.GetMaterialByDescripcionAndEstado(searchValue, Commons.Enums.EstadoInventario.Recuento);
        }

        public async Task<IList<DetalleInventario>> GetByIdInventario(int idInventario)
        {
            return await detalleInventarioSearcher.GetByIdInventario(idInventario);
        }

        public async Task<DetalleInventario> GetById(int id)
        {
            return await detalleInventarioSearcher.GetById(id);
        }

        public async Task<IList<DetalleInventario>> GetDetallesAprobacionMasiva()
        {
            return (await inventarioSearcher.GetAllByEstado(EstadoInventario.PendienteAprobacion)).SelectMany(x => x.DetallesInventario).OrderByDescending(x => x.InventarioId) .ToList();
        }
    }
}
