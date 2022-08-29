using System.Collections.Generic;

namespace Frontend.Azure.DTOs.Movimientos.Reservas
{
    public class ReservaInputDto
    {
        public int Id { get; set; }
        public string NumeroReserva { get; set; }
        public string Imputacion { get; set; }
        public string ClaseDeMovimientoCodigo { get; set; }
        public int EstadoMovimientoId { get; set; } 
        public int TipoDeReserva { get; set; } 
        public List<DetalleReservaInputDto> DetallesReservas { get; set; }
    }
}
