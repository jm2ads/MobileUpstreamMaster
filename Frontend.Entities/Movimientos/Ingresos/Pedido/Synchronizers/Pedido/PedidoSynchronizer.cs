using Frontend.Business.Commons;
using Frontend.Business.IRestServices;
using Frontend.Business.Synchronizer;

namespace Frontend.Business.Movimientos.Ingresos.Synchronizers
{
    public class PedidoSynchronizer : FullSynchronizer<Pedido>
    {
        public PedidoSynchronizer(IRepository<Pedido> repository, ISyncRestService<Pedido> restService)
            : base(repository, restService)
        {

        }
    }
}
