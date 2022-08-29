namespace Frontend.Azure.DTOs
{
    public class DetalleNotaDeEntregaPosicionOutputDto
    {
        public string DocumentoReferencia { get; set; }
        public int PosicionDocumento { get; set; }
        public double CantidadRecibida { get; set; }
        public double CantidadPendiente{ get; set; }
        public string ClaseDeMovimientoCodigo { get; set; }
        public int Ejercicio { get; set; }
    }
}
