using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Azure.DTOs.Movimientos.Traslados
{
    public class TrasladoOutputDto
    {
        public string NumeroProvisorio { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaContabilizacion { get; set; }
        public string ClaseDeMovimientoCodigo { get; set; }
        public List<DetalleTrasladoOutputDto> DetallesTraslado { get; set; }
    }
}
