using Frontend.Business.Commons;
using Frontend.Business.DetallesInventario;
using Frontend.Business.IData;
using Frontend.Business.Inventarios;
using Frontend.Business.InventariosMasivos;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Ingresos;
using Frontend.Business.Movimientos.NotasDeReservas;
using Frontend.Business.Movimientos.Reservas;
using Frontend.Business.Movimientos.SalidasInternas;
using Frontend.Commons.Bootstrapper;
using Frontend.Commons.Commons.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Business.Synchronizer
{
    public class Synchronizer<T> where T : SyncEntity
    {
        protected readonly IRepository<T> repository;

        protected readonly ISyncRestService<T> restService;
        private readonly IDatabaseManager databaseManager;

        public Synchronizer(IRepository<T> repository, ISyncRestService<T> restService, IDatabaseManager databaseManager)
        {
            this.restService = restService;
            this.databaseManager = databaseManager;
            this.repository = repository;
        }

        public async Task DropTables()
        {
            await repository.DeleteAll();
        }

        public void DropAllTables()
        {
            databaseManager.ResetDB();
        }

        public async Task Sync(object param = null)
        {
            await Upload();
            await Download(param);
        }

        public async Task PartialSync()
        {
            await Upload();
            await PartialDownload();
        }

        public async Task Upload()
        {
            var entities = await repository.FindWithChildren(x => x.SyncState == SyncState.PendingToSync);
            if (entities != null && entities.Count > 0)
            {
                await restService.DoPost(entities);
                var entitiesSyncronized = entities.Map(SetUploadedSync);
                await repository.UpdateAll(entitiesSyncronized);
            }
        }

        public async Task Download(object param)
        {
            var entities = await restService.DoGet(param);
            var entitiesSyncronized = entities.Map(SetSync);

            if (typeof(T) == typeof(Pedido))
            {
                await databaseManager.ResetDB(new List<Type>()
                {
                    typeof(Pedido),
                    typeof(DetallePedido),
                    typeof(DetallePedidoPosicion)
                });
                var detallePedidoRepository = ContainerManager.Resolve<IRepository<DetallePedido>>();
                var detallePedidoList = entitiesSyncronized.SelectMany(pedido => (pedido as Pedido).DetallesPedido).ToList();

                var detallePedidoPosicionRepository = ContainerManager.Resolve<IRepository<DetallePedidoPosicion>>();
                var detallePedidoPosicionList = detallePedidoList.SelectMany(detallePedido => (detallePedido as DetallePedido).DetallesPedidoPosicion).ToList();

                var taskPedidos = repository.InsertAll(entitiesSyncronized);
                var taskDetallesPedidos = detallePedidoRepository.InsertAll(detallePedidoList);
                var taskDetallePedidosPosicion = detallePedidoPosicionRepository.InsertAll(detallePedidoPosicionList);

                await taskPedidos;
                await taskDetallesPedidos;
                await taskDetallePedidosPosicion;

                return;
            }

            else if (typeof(T) == typeof(Reserva))
            {
                await databaseManager.ResetDB(new List<Type>()
                {
                    typeof(Reserva),
                    typeof(DetalleReserva),
                    typeof(NotaDeReserva),
                    typeof(DetalleNotaDeReserva)
                });

                var detalleReservaRepository = ContainerManager.Resolve<IRepository<DetalleReserva>>();
                var detalleReservaList = entitiesSyncronized.SelectMany(reserva => (reserva as Reserva).DetallesReserva).ToList();

                var taskReservas = repository.InsertAll(entitiesSyncronized);
                var taskDetallesReservas = detalleReservaRepository.InsertAll(detalleReservaList);

                await taskReservas;
                await taskDetallesReservas;

                return;
            }

            else if (typeof(T) == typeof(SalidaInterna))
            {
                await databaseManager.ResetDB(new List<Type>()
                {
                    typeof(SalidaInterna),
                    typeof(DetalleSalidaInterna)
                });

                var detalleSalidaInternaRepository = ContainerManager.Resolve<IRepository<DetalleSalidaInterna>>();
                var detalleSalidaInternaList = entitiesSyncronized.SelectMany(salidaInterna => (salidaInterna as SalidaInterna).DetallesSalidaInterna).ToList();

                var taskSalidasInternas = repository.InsertAll(entitiesSyncronized);
                var taskDetallesSalidasInternas = detalleSalidaInternaRepository.InsertAll(detalleSalidaInternaList);

                await taskSalidasInternas;
                await taskDetallesSalidasInternas;
                return;
            }

            else if (typeof(T) == typeof(Inventario))
            {
                var detalleInventarioRepository = ContainerManager.Resolve<IRepository<DetalleInventario>>();
                var detalleInventarioList = entitiesSyncronized.SelectMany(inventario => (inventario as Inventario).DetallesInventario).ToList();

                var taskInventarios = repository.InsertAll(entitiesSyncronized);
                var taskDetallesInventarios = detalleInventarioRepository.InsertAll(detalleInventarioList);

                await taskInventarios;
                await taskDetallesInventarios;
                return;
            }

            else if (typeof(T) == typeof(Movimiento))
            {
                await databaseManager.ResetDB(new List<Type>()
                {
                    typeof(Movimiento),
                    typeof(ClaseDeMovimiento)
                });


                var claseDeMovimientoRepository = ContainerManager.Resolve<IRepository<ClaseDeMovimiento>>();
                var claseDeMovimientoList = entitiesSyncronized.SelectMany(movimiento => (movimiento as Movimiento).ClasesDeMovimientos).ToList();

                var taskMovimientos = repository.InsertAll(entitiesSyncronized);
                var taskClaseDeMovimiento = claseDeMovimientoRepository.InsertAll(claseDeMovimientoList);
                await taskMovimientos;
                await taskClaseDeMovimiento;
                
                return;
            }

            else if (typeof(T) == typeof(InventarioMasivoOrden))
            {
                await databaseManager.ResetDB(new List<Type>()
                {
                    typeof(InventarioMasivoOrden)
                });
                var taskInventarioMasivoOrden = repository.InsertAll(entitiesSyncronized);
                await taskInventarioMasivoOrden;
                return;
            }
            else
            {
                await repository.InsertAll(entitiesSyncronized);
            }
        }

        public async Task PartialDownload()
        {
            var entities = await restService.DoGet(null);
            var entitiesSyncronized = entities.Map(SetSync);

            if (typeof(T) == typeof(Pedido))
            {
                await databaseManager.ResetDB(new List<Type>()
                {
                    typeof(Pedido),
                    typeof(DetallePedido),
                    typeof(DetallePedidoPosicion)
                });
                var detallePedidoRepository = ContainerManager.Resolve<IRepository<DetallePedido>>();
                var detallePedidoList = entitiesSyncronized.SelectMany(pedido => (pedido as Pedido).DetallesPedido).ToList();

                var detallePedidoPosicionRepository = ContainerManager.Resolve<IRepository<DetallePedidoPosicion>>();
                var detallePedidoPosicionList = detallePedidoList.SelectMany(detallePedido => (detallePedido as DetallePedido).DetallesPedidoPosicion).ToList();

                var taskPedidos = repository.InsertAll(entitiesSyncronized);
                var taskDetallesPedidos = detallePedidoRepository.InsertAll(detallePedidoList);
                var taskDetallePedidosPosicion = detallePedidoPosicionRepository.InsertAll(detallePedidoPosicionList);

                await taskPedidos;
                await taskDetallesPedidos;
                await taskDetallePedidosPosicion;

                return;
            }

            else if (typeof(T) == typeof(Reserva))
            {
                await databaseManager.ResetDB(new List<Type>()
                {
                    typeof(Reserva),
                    typeof(DetalleReserva),
                    typeof(NotaDeReserva),
                    typeof(DetalleNotaDeReserva)
                });

                var detalleReservaRepository = ContainerManager.Resolve<IRepository<DetalleReserva>>();
                var detalleReservaList = entitiesSyncronized.SelectMany(reserva => (reserva as Reserva).DetallesReserva).ToList();

                var taskReservas = repository.InsertAll(entitiesSyncronized);
                var taskDetallesReservas = detalleReservaRepository.InsertAll(detalleReservaList);

                await taskReservas;
                await taskDetallesReservas;

                return;
            }

            else if (typeof(T) == typeof(SalidaInterna))
            {
                await databaseManager.ResetDB(new List<Type>()
                {
                    typeof(SalidaInterna),
                    typeof(DetalleSalidaInterna)
                });

                var detalleSalidaInternaRepository = ContainerManager.Resolve<IRepository<DetalleSalidaInterna>>();
                var detalleSalidaInternaList = entitiesSyncronized.SelectMany(salidaInterna => (salidaInterna as SalidaInterna).DetallesSalidaInterna).ToList();

                var taskSalidasInternas = repository.InsertAll(entitiesSyncronized);
                var taskDetallesSalidasInternas = detalleSalidaInternaRepository.InsertAll(detalleSalidaInternaList);

                await taskSalidasInternas;
                await taskDetallesSalidasInternas;
                return;
            }

            else if (typeof(T) == typeof(Inventario))
            {
                await databaseManager.ResetDB(new List<Type>()
                {
                    typeof(Inventario),
                    typeof(DetalleInventario)
                });

                var detalleInventarioRepository = ContainerManager.Resolve<IRepository<DetalleInventario>>();
                var detalleInventarioList = entitiesSyncronized.SelectMany(inventario => (inventario as Inventario).DetallesInventario).ToList();

                var taskInventarios = repository.InsertAll(entitiesSyncronized);
                var taskDetallesInventarios = detalleInventarioRepository.InsertAll(detalleInventarioList);

                await taskInventarios;
                await taskDetallesInventarios;
                return;
            }

            else if (typeof(T) == typeof(Movimiento))
            {
                await databaseManager.ResetDB(new List<Type>()
                {
                    typeof(Movimiento),
                    typeof(ClaseDeMovimiento)
                }); 
                
                var claseDeMovimientoRepository = ContainerManager.Resolve<IRepository<ClaseDeMovimiento>>();
                var claseDeMovimientoList = entitiesSyncronized.SelectMany(movimiento => (movimiento as Movimiento).ClasesDeMovimientos).ToList();

                var taskMovimientos = repository.InsertAll(entitiesSyncronized);
                var taskClaseDeMovimiento = claseDeMovimientoRepository.InsertAll(claseDeMovimientoList);
                await taskMovimientos;
                await taskClaseDeMovimiento;
                return;
            }

            else if (typeof(T) == typeof(InventarioMasivoOrden))
            {
                await databaseManager.ResetDB(new List<Type>()
                {
                    typeof(InventarioMasivoOrden)
                });
                var taskInventarioMasivoOrden = repository.InsertAll(entitiesSyncronized);
                await taskInventarioMasivoOrden;
                return;
            }

            else
            {
                await repository.SaveAllWithChildren(entitiesSyncronized);
            }
        }

        public virtual async Task Rollback()
        {
            await this.repository.DropTableAsync();
        }

        private T SetSync(T entity)
        {
            entity.Downloaded = DateTime.UtcNow;
            entity.SyncState = SyncState.Synchronized;
            return entity;
        }

        private T SetUploadedSync(T entity)
        {
            entity.Uploaded = DateTime.UtcNow;
            entity.SyncState = SyncState.Synchronized;
            return entity;
        }
    }
}