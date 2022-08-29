namespace Frontend.Azure.DTOs.Movimientos.SalidasInternas
{
    public class DetalleSalidaInternaInputDto
    {
        public int Id { get; set; }
        public string  Posicion { get; set; }
        public int MaterialId { get; set; }
        public double CantidadPendiente { get; set; }
        public string UnidadDeMedida { get; set; }
    }
}
