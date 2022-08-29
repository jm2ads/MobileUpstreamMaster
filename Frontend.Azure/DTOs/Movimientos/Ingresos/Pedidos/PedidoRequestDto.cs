namespace Frontend.Azure.DTOs
{
    public class PedidoRequestDto
    {
        public int CentroId { get; set; }
        public string delta { get; set; }
        public int[] EstadoMovimientoIds { get; set; }
    }
}
