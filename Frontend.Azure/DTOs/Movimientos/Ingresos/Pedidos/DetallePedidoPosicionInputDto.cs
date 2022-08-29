namespace Frontend.Azure.DTOs
{
    public class DetallePedidoPosicionInputDto
    {
        public int Id { get; set; }
        public string DocumentoReferencia { get; set; }
        public int PosicionDocumento { get; set; }
        public double CantidadPendiente { get; set; }
        public string ClaseDeMovimientoCodigo { get; set; }
        public int Ejercicio { get; set; }
        public int DetallePedidoId { get; set; }
    }
}
