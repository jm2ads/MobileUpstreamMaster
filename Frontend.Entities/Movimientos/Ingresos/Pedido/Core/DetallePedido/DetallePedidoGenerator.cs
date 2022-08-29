using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Ingresos.Core
{
    public class DetallePedidoGenerator
    {
        private readonly IRepository<DetallePedido> repository;

        public DetallePedidoGenerator(IRepository<DetallePedido> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(DetallePedido detallePedido)
        {
            await repository.SaveWithChildren(detallePedido);
        }
    }
}
