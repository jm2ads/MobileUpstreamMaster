using Frontend.Commons.Enums;

namespace Frontend.Azure.DTOs
{
    public class DetalleInventarioInputDto
    {
        public int Id { get; set; }
        public double Cantidad { get; set; }
        public double CantidadContada { get; set; }
        public bool EsContado { get; set; }
        public string Posicion { get; set; }
        public string Ubicacion { get; set; }
        public string Comentario { get; set; }
        public string UnidadAlmacenamiento { get; set; }
        public int StockEspecialId { get; set; }
        public int TipoStockId { get; set; }
        public int StockId { get; set; }
        public int ClaseDeValoracionId { get; set; }
        public int DetalleStockEspecialId { get; set; }
        public bool HayConteoErroneo { get; set; }
        public EstadoConteoEnum EstadoConteo { get; set; }
    }
}
