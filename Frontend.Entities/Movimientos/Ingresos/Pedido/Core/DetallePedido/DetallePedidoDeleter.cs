using Frontend.Business.Commons;
using Frontend.Business.Movimientos.Ingresos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Ingresos.Core
{
    public class DetallePedidoDeleter
    {
        private readonly IRepository<DetallePedido> repository;

        public DetallePedidoDeleter(IRepository<DetallePedido> repository)
        {
            this.repository = repository;
        }

        public async Task DeleteAll()
        {
            await repository.DeleteAll();
        }
    }
}
