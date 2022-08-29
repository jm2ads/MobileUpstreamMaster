using Frontend.Business.Almacenes;
using Frontend.Business.Centros;
using Frontend.Business.InventariosMasivos;
using Frontend.Business.InventariosMasivos.Core;
using Frontend.Business.InventariosMasivos.Core.InventariosMasivos;
using Frontend.Business.InventariosMasivos.Searchers;
using Frontend.Business.InventariosMasivos.Validations;
using Frontend.Business.Stocks;
using Frontend.Business.Stocks.Searchers;
using Frontend.Business.Synchronizer;
using Frontend.Commons.Enums;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class InventarioMasivoService : IInventarioMasivoService
    {
        private readonly IStockService stockService;
        private readonly InventarioMasivoSearcher inventarioMasivoSearcher;
        private readonly InventarioMasivoUpdater inventarioMasivoUpdater;
        private readonly InventarioMasivoDeleter inventarioMasivoDeleter;
        private readonly InventarioMasivoValidator inventarioMasivoValidator;
        private readonly InventarioMasivoGenerator inventarioMasivoGenerator;
        private readonly DetalleInventarioMasivoFactory detalleInventarioMasivoFactory;
        private readonly DetalleInventarioMasivoDeleter detalleInventarioMasivoDeleter;
        private readonly InventarioMasivoOrdenSearcher inventarioMasivoOrdenSearcher;
        private readonly InventarioMasivoSaver inventarioMasivoSaver;
        private readonly StockSearcher stockSearcher;
        private readonly InventarioMasivoFactory inventarioMasivoFactory;

        public InventarioMasivoService(IStockService stockService,
            InventarioMasivoSearcher inventarioMasivoSearcher,
            InventarioMasivoUpdater inventarioMasivoUpdater,
            InventarioMasivoDeleter inventarioMasivoDeleter,
            InventarioMasivoFactory inventarioMasivoFactory,
            InventarioMasivoValidator inventarioMasivoValidator,
            InventarioMasivoGenerator inventarioMasivoGenerator,
            DetalleInventarioMasivoFactory detalleInventarioMasivoFactory,
            DetalleInventarioMasivoDeleter detalleInventarioMasivoDeleter,
            InventarioMasivoOrdenSearcher inventarioMasivoOrdenSearcher,
            InventarioMasivoSaver inventarioMasivoSaver,
            StockSearcher stockSearcher)
        {
            this.stockService = stockService;
            this.inventarioMasivoSearcher = inventarioMasivoSearcher;
            this.inventarioMasivoFactory = inventarioMasivoFactory;
            this.inventarioMasivoUpdater = inventarioMasivoUpdater;
            this.inventarioMasivoDeleter = inventarioMasivoDeleter;
            this.inventarioMasivoValidator = inventarioMasivoValidator;
            this.inventarioMasivoGenerator = inventarioMasivoGenerator;
            this.stockSearcher = stockSearcher;
            this.detalleInventarioMasivoFactory = detalleInventarioMasivoFactory;
            this.detalleInventarioMasivoDeleter = detalleInventarioMasivoDeleter;
            this.inventarioMasivoOrdenSearcher = inventarioMasivoOrdenSearcher;
            this.inventarioMasivoSaver = inventarioMasivoSaver;
        }

        public async Task Update(InventarioMasivo inventarioMasivo, SyncState syncState = SyncState.Updated)
        {
            await inventarioMasivoUpdater.Update(inventarioMasivo, syncState);
        }

        public async Task Insert(InventarioMasivo inventarioMasivo, SyncState syncState = SyncState.New)
        {
            await inventarioMasivoGenerator.Generate(inventarioMasivo, syncState);
        }

        public async Task Delete(InventarioMasivo inventarioMasivo)
        {
            await inventarioMasivoDeleter.Delete(inventarioMasivo);
        }

        public async Task Delete(DetalleInventarioMasivo detalleInventarioMasivo)
        {
            await detalleInventarioMasivoDeleter.Delete(detalleInventarioMasivo);
        }

        public bool Validate(InventarioMasivo inventarioMasivo)
        {
            return inventarioMasivoValidator.Validate(inventarioMasivo);
        }

        public bool ValidateDistribuido(InventarioMasivo inventarioMasivo)
        {
            return inventarioMasivoValidator.ValidateDistribuido(inventarioMasivo);
        }

        public async Task<DetalleInventarioMasivo> Duplicar(DetalleInventarioMasivo detalleInventarioMasivo)
        {
            return await detalleInventarioMasivoFactory.Duplicar(detalleInventarioMasivo);
        }
        public InventarioMasivo Create(Centro centro)
        {
            return inventarioMasivoFactory.Create(centro);
        }

        public async Task<IList<InventarioMasivoOrden>> GetOrden()
        {
            return await inventarioMasivoOrdenSearcher.Get();
        }

        public async Task<InventarioMasivo> Distribuir(InventarioMasivo inventarioMasivo)
        {
            var orden = await GetOrden();
            orden = orden.Where(x => !inventarioMasivo.AlmacenesExcluidos.Exists(y => x.AlmacenId == y.Id)).ToList();
            return await inventarioMasivoFactory.Create(inventarioMasivo, orden);
        }

        public async Task SumarCantidades(InventarioMasivo inventarioMasivo, int posicion, double cantidad)
        {
            inventarioMasivo.DetallesInventarioMasivo.FirstOrDefault(di => di.Posicion == posicion).Cantidad += cantidad;
            await Update(inventarioMasivo);
        }

        public async Task<List<Almacen>> GetAlmacenes()
        {
            return (await GetOrden())
                .Where(alm => alm.Almacen != null)
                .Select(inv => inv.Almacen)
                .GroupBy(alm => alm.Id)
                .Select(x => x.First())
                .OrderBy(x => x.DisplayDescription)
                .ToList();
        }

        public async Task<List<Almacen>> GetAlmacenes(int materialId)
        {
            return (await stockSearcher.GetByUbicacion(materialId, null)).Where(stock => (stock.DetalleStockEspecial.StockEspecial.Codigo == "Q" || stock.DetalleStockEspecial.StockEspecial.Codigo == "S") && ValidateCantidades(stock))
                .Select(x => x.Almacen).ToList();
        }

        private bool ValidateCantidades(Stock stock)
        {
            return (stock.CantidadAlmacen > 0 || stock.CantidadBloqueado > 0 || stock.CantidadCalidad > 0);
        }

        public Task<IList<InventarioMasivo>> GetAllProvisoriosWithChildren()
        {
            return inventarioMasivoSearcher.GetWithChildrenBy(EstadoInventario.Provisorio);
        }
        
        public async Task Save(InventarioMasivo inventarioMasivo)
        {
            await inventarioMasivoSaver.Save(inventarioMasivo);
        }
    }
}
