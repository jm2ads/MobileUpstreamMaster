using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Ingresos.Core
{
    public class PedidoUpdater
    {
        private readonly IRepository<Pedido> repository;

        public PedidoUpdater(IRepository<Pedido> repository)
        {
            this.repository = repository;
        }

        public async Task Update(Pedido pedido)
        {
            await repository.UpdateWithChildren(pedido);
        }
    }
}
