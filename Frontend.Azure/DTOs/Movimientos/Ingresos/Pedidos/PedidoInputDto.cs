using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Azure.DTOs
{ 
    public class PedidoInputDto
    {
        public int Id { get; set; }
        public string NumeroPedido { get; set; }
        public int EstadoMovimientoId { get; set; }
        public DateTime FechaModificacion { get; set; }
        public ICollection<DetallePedidoInputDto> DetallesPedido { get; set; }
    }
}
