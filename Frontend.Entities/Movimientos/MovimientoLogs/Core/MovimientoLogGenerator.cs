using Frontend.Business.Commons;
using Frontend.Business.Movimientos.Ingresos.Searchers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Core
{
    public class MovimientoLogGenerator
    {
        private readonly IRepository<MovimientoLog> repository;
        private readonly PedidoSearcher pedidoSearcher;

        public MovimientoLogGenerator(IRepository<MovimientoLog> repository, PedidoSearcher pedidoSearcher)
        {
            this.repository = repository;
            this.pedidoSearcher = pedidoSearcher;
        }

        public async Task<MovimientoLog> Generate(MovimientoLog movimientoLog)
        {
            if (movimientoLog.Label.Contains("Pedido inexistente"))
            {
                var pedido = await pedidoSearcher.GetById(movimientoLog.IdRemoto);
                if (pedidoSearcher != null)
                {
                    movimientoLog.NumeroMovimiento = pedido.NumeroPedido;
                }
            }
            return await repository.SaveWithChildren(movimientoLog);
        }

        public async Task<IList<MovimientoLog>> Generate(IList<MovimientoLog> movimientoLogList)
        {
            try
            {
                foreach (var movimientoLog in movimientoLogList)
                {
                    await Generate(movimientoLog);
                }
                return movimientoLogList;

            }
            catch (System.Exception e)
            {
                throw;
            }
        }
    }
}
