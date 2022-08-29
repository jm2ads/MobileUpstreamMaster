namespace Frontend.Azure.DTOs.InventariosMasivos
{
    public class InventarioMasivoOrdenInputDto
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public int CentroId { get; set; }
        public int StockEspecialId { get; set; }
        public int AlmacenId { get; set; }
        public int LoteId { get; set; }
    }
}
