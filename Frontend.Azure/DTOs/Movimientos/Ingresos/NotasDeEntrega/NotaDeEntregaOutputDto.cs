using System;
using System.Collections.Generic;

namespace Frontend.Azure.DTOs
{
    public class NotaDeEntregaOutputDto
    {
        public int PedidoId { get; set; }
        public string NumeroNotaDeEntrega { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaContabilizacion { get; set; }
        public string CartaDePorte { get; set; }
        public string TextoDeCabecera { get; set; }
        public string NumeroPedido { get; set; }
        public string UsuarioCreacion { get; set; }
        public List<DetalleNotaDeEntregaOutputDto> DetallesNotaDeEntrega { get; set; }
    }
}
