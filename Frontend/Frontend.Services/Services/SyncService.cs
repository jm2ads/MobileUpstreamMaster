using Frontend.Business.Almacenes;
using Frontend.Business.CambiosUbicacion;
using Frontend.Business.Centros;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.Commons;
using Frontend.Business.DetallesStocksEspeciales;
using Frontend.Business.EstadosInventarios;
using Frontend.Business.Funcionalidades;
using Frontend.Business.GruposDeArticulos;
using Frontend.Business.Inventarios;
using Frontend.Business.InventariosLocales;
using Frontend.Business.InventariosMasivos;
using Frontend.Business.Materiales;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Ingresos;
using Frontend.Business.Movimientos.NotasDeReservas;
using Frontend.Business.Movimientos.Reservas;
using Frontend.Business.Movimientos.SalidasInternas;
using Frontend.Business.Movimientos.Traslados;
using Frontend.Business.Stocks;
using Frontend.Business.StocksEspeciales;
using Frontend.Business.Synchronizer;
using Frontend.Commons.Commons;
using Frontend.IServices.IServices;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Frontend.Services.Services
{
    public class SyncService : ISyncService
    {
        private readonly ISettingsService settingsService;
        private readonly IMovimientoLogService movimientoLogService;
        private readonly IInventarioService inventarioService;
        private readonly IDatabaseManager databaseManager;
        private readonly Synchronizer<Centro> centroSynchronizer;
        private readonly Synchronizer<Almacen> almacenSynchronizer;
        private readonly Synchronizer<ClaseDeValoracion> claseDeValoracionSynchronizer;
        private readonly Synchronizer<Material> materialSynchronizer;
        private readonly Synchronizer<Stock> stockSynchronizer;
        private readonly Synchronizer<StockEspecial> stockEspecialSynchronizer;
        private readonly Synchronizer<Inventario> inventarioSynchronizer;
        private readonly Synchronizer<EstadoInventario> estadoInventarioSynchronizer;
        private readonly Synchronizer<GrupoDeArticulo> grupoDeArticuloSynchronizer;
        private readonly Synchronizer<DetalleStockEspecial> detalleStockEspecialSynchronizer;
        private readonly Synchronizer<Funcionalidad> funcionalidadSynchronizer;
        private readonly Synchronizer<Pedido> pedidoSynchronizer;
        private readonly Synchronizer<SalidaInterna> salidaInternaSynchronizer;
        private readonly Synchronizer<InventarioMasivoOrden> inventarioMasivoOrdenSynchronizer;
        private readonly Synchronizer<Movimiento> movimientoSynchronizer;
        private readonly LocalSynchronizer<InventarioLocal> inventarioLocalSynchronizer;
        private readonly LocalSynchronizer<NotaDeReserva> notaDeReservaLocalSynchronizer;
        private readonly LocalSynchronizer<NotaDeEntrega> notaDeEntregaLocalSynchronizer;
        private readonly LocalSynchronizer<Traslado> trasladoLocalSynchronizer;
        private readonly LocalSynchronizer<InventarioMasivo> inventarioMasivoSynchronizer;
        private readonly Synchronizer<Reserva> reservaSynchronizer;
        private readonly LocalSynchronizer<CambioUbicacion> cambioUbicacionLocalSynchronizer;

        public SyncService(ISettingsService settingsService,
                            IMovimientoLogService movimientoLogService,
                            IInventarioService inventarioService,
                            IDatabaseManager databaseManager,
                            Synchronizer<Centro> centroSynchronizer,
                            Synchronizer<Almacen> almacenSynchronizer,
                            Synchronizer<ClaseDeValoracion> claseDeValoracionSynchronizer,
                            Synchronizer<Material> materialSynchronizer,
                            Synchronizer<Stock> stockSynchronizer,
                            Synchronizer<StockEspecial> stockEspecialSynchronizer,
                            Synchronizer<Inventario> inventarioSynchronizer,
                            Synchronizer<EstadoInventario> estadoInventarioSynchronizer,
                            Synchronizer<GrupoDeArticulo> grupoDeArticuloSynchronizer,
                            Synchronizer<DetalleStockEspecial> detalleStockEspecialSynchronizer,
                            Synchronizer<Funcionalidad> funcionalidadSynchronizer,
                            Synchronizer<Reserva> reservaSynchronizer,
                            Synchronizer<Pedido> pedidoSynchronizer,
                            Synchronizer<SalidaInterna> salidaInternaSynchronizer,
                            Synchronizer<InventarioMasivoOrden> inventarioMasivoOrdenSynchronizer,
                            Synchronizer<Movimiento> movimientoSynchronizer,
                            LocalSynchronizer<InventarioLocal> inventarioLocalSynchronizer,
                            LocalSynchronizer<NotaDeReserva> notaDeReservaLocalSynchronizer,
                            LocalSynchronizer<NotaDeEntrega> notaDeEntregaLocalSynchronizer,
                            LocalSynchronizer<Traslado> trasladoLocalSynchronizer,
                            LocalSynchronizer<InventarioMasivo> inventarioMasivoSynchronizer,
                            LocalSynchronizer<CambioUbicacion> cambioUbicacionLocalSynchronizer)
        {
            this.settingsService = settingsService;
            this.movimientoLogService = movimientoLogService;
            this.inventarioService = inventarioService;
            this.databaseManager = databaseManager;
            this.centroSynchronizer = centroSynchronizer;
            this.almacenSynchronizer = almacenSynchronizer;
            this.claseDeValoracionSynchronizer = claseDeValoracionSynchronizer;
            this.materialSynchronizer = materialSynchronizer;
            this.stockSynchronizer = stockSynchronizer;
            this.stockEspecialSynchronizer = stockEspecialSynchronizer;
            this.inventarioSynchronizer = inventarioSynchronizer;
            this.estadoInventarioSynchronizer = estadoInventarioSynchronizer;
            this.grupoDeArticuloSynchronizer = grupoDeArticuloSynchronizer;
            this.detalleStockEspecialSynchronizer = detalleStockEspecialSynchronizer;
            this.funcionalidadSynchronizer = funcionalidadSynchronizer;
            this.pedidoSynchronizer = pedidoSynchronizer;
            this.salidaInternaSynchronizer = salidaInternaSynchronizer;
            this.inventarioMasivoOrdenSynchronizer = inventarioMasivoOrdenSynchronizer;
            this.movimientoSynchronizer = movimientoSynchronizer;
            this.inventarioLocalSynchronizer = inventarioLocalSynchronizer;
            this.notaDeReservaLocalSynchronizer = notaDeReservaLocalSynchronizer;
            this.notaDeEntregaLocalSynchronizer = notaDeEntregaLocalSynchronizer;
            this.trasladoLocalSynchronizer = trasladoLocalSynchronizer;
            this.inventarioMasivoSynchronizer = inventarioMasivoSynchronizer;
            this.reservaSynchronizer = reservaSynchronizer;
            this.cambioUbicacionLocalSynchronizer = cambioUbicacionLocalSynchronizer;
        }

        public async Task DropData()
        {
            await databaseManager.ResetDB();
        }

        public async Task SyncDataBase(string idRed)
        {
            await centroSynchronizer.DropTables();
            await centroSynchronizer.Sync(idRed);
            await funcionalidadSynchronizer.DropTables();
            await funcionalidadSynchronizer.Sync();
        }

        public async Task SyncData()
        {
            var settings = await settingsService.GetWithChildren();
            settings.LastSync = ApplicationConstants.DefaultDateSync;
            await settingsService.Update(settings);

            var funcionalidades = FuncionalidadesSync();
            await funcionalidades;

            var inventarios = inventarioSynchronizer.Sync();
            var pedidos = pedidoSynchronizer.Sync();
            var reservas = reservaSynchronizer.Sync();
            var salidasInternas = salidaInternaSynchronizer.Sync();

            var centros = centroSynchronizer.PartialSync();
            var almacen = almacenSynchronizer.Sync();
            var claseDeValoracion = claseDeValoracionSynchronizer.Sync();
            var stockEspecial = stockEspecialSynchronizer.Sync();
            var grupoDeArticulo = grupoDeArticuloSynchronizer.Sync();
            var materiales = materialSynchronizer.Sync();
            var datellesStockEspecial = detalleStockEspecialSynchronizer.Sync();
            var stocks = stockSynchronizer.Sync();

            var inventarioMasivoOrden = inventarioMasivoOrdenSynchronizer.Sync();
            var movimientos = movimientoSynchronizer.Sync();

            await inventarios;
            await pedidos;
            await reservas;
            await salidasInternas;
            await centros;
            await almacen;
            await claseDeValoracion;
            await stockEspecial;
            await grupoDeArticulo;
            await materiales;
            await datellesStockEspecial;
            await stocks;
            await inventarioMasivoOrden;
            await movimientos;

            await UpdateLastSync();
        }

        public async Task SyncDataParcial()
        {
            var inventarioMasivoOrden = inventarioMasivoOrdenSynchronizer.Sync();
            //Se sincronizan entidades con deltas
            var centros = centroSynchronizer.PartialSync();
            var almacenes = almacenSynchronizer.PartialSync();
            var clasesDeValoracion = claseDeValoracionSynchronizer.PartialSync();
            var gruposDeArticulo = grupoDeArticuloSynchronizer.PartialSync();
            var materiales = materialSynchronizer.PartialSync();
            var detallesEspeciales = detalleStockEspecialSynchronizer.PartialSync();
            var stocks = stockSynchronizer.PartialSync();

            var inventarios = InventarioSync();
            var reservas = ReservaSync();
            var pedidos = PedidoSync();
            var traslados = trasladoLocalSynchronizer.Sync();
            var salidasInternas = salidaInternaSynchronizer.Sync();
            var cambiosUbicacion = cambioUbicacionLocalSynchronizer.Sync();
            var movimientos = movimientoSynchronizer.Sync();

            await inventarios;
            await pedidos;
            await reservas;
            await salidasInternas;
            await traslados;
            await centros;
            await almacenes;
            await clasesDeValoracion;
            await gruposDeArticulo;
            await materiales;
            await detallesEspeciales;
            await stocks;
            await inventarioMasivoOrden;
            await cambiosUbicacion;
            await movimientos;

            await UpdateLastSync();
        }

        private async Task InventarioSync()
        {
            //Push - Se envían inventarios masivos creados
            await inventarioMasivoSynchronizer.Sync();
            //Push - Se envían inventarios creados
            await inventarioLocalSynchronizer.Sync();
            //Pull - Se traen inventarios de backoffice
            await inventarioSynchronizer.PartialSync();
        }

        private async Task FuncionalidadesSync()
        {
            await funcionalidadSynchronizer.Sync();
        }

        private async Task ReservaSync()
        {
            //Push - Se envían notas de reserva creados
            await notaDeReservaLocalSynchronizer.Sync();
            //Pull - Se traen reservas de backoffice
            await reservaSynchronizer.PartialSync();
        }

        private async Task PedidoSync()
        {
            //Push - Se envían notas de entrega creadas
            await notaDeEntregaLocalSynchronizer.Sync();
            //Pull - Se traen pedidos de backoffice
            await pedidoSynchronizer.PartialSync();
        }

        public async Task UploadPedidos()
        {
            var centros = centroSynchronizer.PartialSync();
            var almacenes = almacenSynchronizer.PartialSync();
            var clasesDeValoracion = claseDeValoracionSynchronizer.PartialSync();
            var gruposDeArticulo = grupoDeArticuloSynchronizer.PartialSync();
            var materiales = materialSynchronizer.PartialSync();
            var detallesEspeciales = detalleStockEspecialSynchronizer.PartialSync();

            var stocks = stockSynchronizer.PartialSync();


            var pedidos = PedidoSync();

            await pedidos;
            await centros;
            await almacenes;
            await clasesDeValoracion;
            await gruposDeArticulo;
            await materiales;
            await detallesEspeciales;
            await stocks;
        }

        private async Task UpdateLastSync()
        {
            var settings = await settingsService.GetWithChildren();
            settings.LastSync = DateTime.Now.AddMinutes(ApplicationConstants.DelaySyncMinutes);
            settings.VersionNumber = DependencyService.Get<IApplicationInformation>().GetVersionNumber();
            settings.BuildNumber = DependencyService.Get<IApplicationInformation>().GetBuildNumber();

            var deleteMovimientoLogs = movimientoLogService.DeleteOlderThan(ApplicationConstants.LogsExpirationDays);
            var deleteInventarioLogs = inventarioService.DeleteLogOlderThan(ApplicationConstants.LogsExpirationDays);            

            await settingsService.Update(settings);
        }

    }
}
