namespace Frontend.Azure.DTOs.Movimientos.SalidasInternas
{
    public class SalidaInternaRequestDto
    {
        public int CentroId { get; set; }
        public string delta { get; set; }
        public int[] EstadoMovimientoIds { get; set; }
    }
}
