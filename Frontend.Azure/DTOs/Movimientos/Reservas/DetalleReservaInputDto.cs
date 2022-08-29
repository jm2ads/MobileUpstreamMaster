namespace Frontend.Azure.DTOs.Movimientos.Reservas
{
    public class DetalleReservaInputDto
    {
        public int Id { get; set; }
        public string Posicion { get; set; }
        public double CantidadReserva { get; set; }
        public string Unidad { get; set; }
        public string TextoPosicion { get; set; }
        public string PuestoDeDescarga { get; set; }
        public string Destinatario { get; set; }
        public int ReservaId { get; set; }
        public int? ClaseDeValoracionId { get; set; }
        public int? AlmacenId { get; set; }
        public int MaterialId { get; set; }
        public int CentroId { get; set; }
        public string ClaseDeMovimientoCodigo { get; set; }
    }
}
