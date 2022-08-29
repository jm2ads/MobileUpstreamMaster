using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Ingresos.Core
{
    public class PedidoGenerator
    {
        private readonly IRepository<Pedido> repository;

        public PedidoGenerator(IRepository<Pedido> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(Pedido pedido)
        {
            await repository.SaveWithChildren(pedido);
        }
    }
}
