namespace Frontend.Azure.DTOs.Movimientos.SalidasInternas
{
    public class DetalleSalidaInternaOutputDto
    {
        public int Id { get; set; }
        public int ClaseDeValoracionId { get; set; }
        public string TextoPosicion { get; set; }
        public string DestinatarioDeMercancia { get; set; }
        public int AlmacenId { get; set; }
        public double CantidadEnviada { get; set; }
    }
}
