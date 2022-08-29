namespace Frontend.Azure.DTOs.Movimientos.Reservas
{
    public class ReservaOutputDto
    {
        public int CentroId { get; set; }
        public string delta { get; set; }
        public int[] EstadoMovimientoIds { get; set; }
    }
}
