using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Ingresos.Core
{
    public class DetallePedidoUpdater
    {
        private readonly IRepository<DetallePedido> repository;

        public DetallePedidoUpdater(IRepository<DetallePedido> repository)
        {
            this.repository = repository;
        }

        public async Task Update(DetallePedido detallePedido)
        {
            await repository.UpdateWithChildren(detallePedido);
        }
    }
}
