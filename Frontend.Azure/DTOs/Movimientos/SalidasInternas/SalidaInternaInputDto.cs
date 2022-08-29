using System.Collections.Generic;

namespace Frontend.Azure.DTOs.Movimientos.SalidasInternas
{
    public class SalidaInternaInputDto
    {
        public int Id { get; set; }
        public string NumeroPedido { get; set; }
        public int CentroReceptorId { get; set; }
        public string ClaseDeMovimientoCodigo { get; set; }
        public int EstadoMovimientoId { get; set; }
        public IList<DetalleSalidaInternaInputDto> DetallesSalidasInternas { get; set; }
    }
}
