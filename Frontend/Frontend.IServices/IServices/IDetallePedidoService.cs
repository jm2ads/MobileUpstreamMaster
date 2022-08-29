using Frontend.Business.Movimientos.Ingresos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IDetallePedidoService
    {
        Task Update(DetallePedido detallePedido);
    }
}
