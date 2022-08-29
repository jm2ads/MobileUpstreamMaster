using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.InventariosMasivos;
using Frontend.Business.InventariosMasivos.Core;
using Frontend.Business.InventariosMasivos.Validations;
using Frontend.Business.Materiales;
using Frontend.Business.Stocks;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class DetalleInventarioMasivoService : IDetalleInventarioMasivoService
    {
        private readonly DetalleInventarioMasivoFactory detalleInventarioMasivoFactory;
        private readonly DetalleInventarioMasivoValidator detalleInventarioMasivoValidator;
        private readonly IStockEspecialService stockEspecialService;
        private readonly IInventarioMasivoService inventarioMasivoService;
        private readonly IStockService stockService;
        public DetalleInventarioMasivoService(DetalleInventarioMasivoFactory detalleInventarioMasivoFactory, DetalleInventarioMasivoValidator detalleInventarioMasivoValidator, IStockEspecialService stockEspecialService,
            IStockService stockService, IInventarioMasivoService inventarioMasivoService)
        {
            this.detalleInventarioMasivoFactory = detalleInventarioMasivoFactory;
            this.detalleInventarioMasivoValidator = detalleInventarioMasivoValidator;
            this.stockEspecialService = stockEspecialService;
            this.stockService = stockService;
            this.inventarioMasivoService = inventarioMasivoService;
        }
        public async Task<DetalleInventarioMasivo> Create(InventarioMasivo inventarioMasivo, Material material)
        {
            return await detalleInventarioMasivoFactory.Create(inventarioMasivo, material);
        }
        public async Task<IList<string>> PepEditable(IList<Stock> listaStock, string ubicacion, int? almacenId, int? claseDeValoracionId)
        {
            var stockEspecial = await stockEspecialService.GetByCodigo("Q");
            return detalleInventarioMasivoValidator.ValidatePep(listaStock, stockEspecial.Id, ubicacion, almacenId, claseDeValoracionId);
        }
        public IList<ClaseDeValoracion> LoteEditable(IList<ClaseDeValoracion> lotes, int materialId)
        {
            return detalleInventarioMasivoValidator.ValidateLote(lotes);
        }

        public async Task<bool> ValidateDuplicado(InventarioMasivo inventarioMasivo, DetalleInventarioMasivo detalleInventarioMasivo)
        {
            var detalleDuplicado = detalleInventarioMasivoValidator.EsDuplicado(inventarioMasivo, detalleInventarioMasivo);
            if (detalleDuplicado != null)
            {
                await inventarioMasivoService.SumarCantidades(inventarioMasivo, detalleDuplicado.Posicion, detalleInventarioMasivo.Cantidad);
                return true;
            }
            return false;
        }

        public async Task<bool > ValidateMaterial(Material material)
        {
            var stocks = await stockService.GetBy(material.Id);
            return detalleInventarioMasivoValidator.ValidateMaterial(material, stocks);
        }
    }
}
