using Frontend.Commons.Enums;

namespace Frontend.Azure.DTOs.InventariosMasivos
{
    public class DetalleInventarioMasivoOutputDto
    {
        public double CantidadContada { get; set; }
        public string Unidad { get; set; }
        public int TipoStockId { get; set; }
        public int StockId { get; set; }
        public bool HayConteoErroneo { get; set; }
        public EstadoConteoEnum EstadoConteo { get; set; }
        public string Ubicacion { get; set; }
    }
}
