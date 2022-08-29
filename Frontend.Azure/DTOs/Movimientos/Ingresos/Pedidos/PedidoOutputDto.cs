using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Azure.DTOs
{
    public class PedidoOutputDto
    {
        public int Id { get; set;}
        public List<NotaDeEntregaOutputDto> NotasDeEntrega { get; set; }
    }
}
