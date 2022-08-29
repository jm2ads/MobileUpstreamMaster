using Frontend.Business.Movimientos.Ingresos;
using Frontend.Business.Movimientos.Ingresos.Core;
using Frontend.Business.Movimientos.Ingresos.Searchers;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class DetallePedidoService : IDetallePedidoService
    {
        private readonly DetallePedidoFactory detallePedidoFactory;
        private readonly DetallePedidoDeleter detallePedidoDeleter;
        private readonly DetallePedidoSearcher detallePedidoSearcher;
        private readonly DetallePedidoUpdater detallePedidoUpdater;

        public DetallePedidoService(DetallePedidoFactory detallePedidoFactory, DetallePedidoDeleter detallePedidoDeleter, DetallePedidoSearcher detallePedidoSearcher,
            DetallePedidoUpdater detallePedidoUpdater)
        {
            this.detallePedidoFactory = detallePedidoFactory;
            this.detallePedidoDeleter = detallePedidoDeleter;
            this.detallePedidoSearcher = detallePedidoSearcher;
            this.detallePedidoUpdater = detallePedidoUpdater;
        }
        public async Task Update(DetallePedido detallePedido)
        {
            await detallePedidoUpdater.Update(detallePedido);
        }
    }
}
