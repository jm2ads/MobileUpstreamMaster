using System;
using System.Collections.Generic;

namespace Frontend.Azure.DTOs.Movimientos.Reservas
{
    public class NotaDeReservaOutputDto
    {
        public int ReservaId { get; set; }
        public string TipoDeReserva { get; set; }
        public string TextoDeCabecera { get; set; }
        public string UsuarioReserva { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaContabilizacion { get; set; }
        public string NumeroReserva { get; set; }
        public string ClaseDeMovimientoCodigo { get; set; }
        public string Imputacion { get; set; }
        public IList<DetalleNotaDeReservaOutputDto> DetallesNotasDeReservas { get; set; }
    }
}
