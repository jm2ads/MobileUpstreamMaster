using System;
using System.Collections.Generic;

namespace Frontend.Azure.DTOs.Movimientos.SalidasInternas
{
    public class SalidaInternaOutputDto
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string FechaDocumento { get; set; }
        public string FechaContabilizacion { get; set; }
        public IList<DetalleSalidaInternaOutputDto> DetallesSalidasInternas { get; set; }
    }
}
