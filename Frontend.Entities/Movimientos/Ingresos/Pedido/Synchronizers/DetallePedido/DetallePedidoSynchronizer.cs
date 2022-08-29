using Frontend.Business.Commons;
using Frontend.Business.IRestServices;
using Frontend.Business.Synchronizer;

namespace Frontend.Business.Movimientos.Ingresos.Synchronizers
{
    public class DetallePedidoSynchronizer : FullSynchronizer<DetallePedido>
    {
        public DetallePedidoSynchronizer(IRepository<DetallePedido> repository, ISyncRestService<DetallePedido> restService)
            : base(repository, restService)
        {

        }
    }
}
