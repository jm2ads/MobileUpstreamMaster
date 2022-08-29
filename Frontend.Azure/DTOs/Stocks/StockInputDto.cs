namespace Frontend.Azure.DTOs
{
    public class StockInputDto
    {
        public int Id { get; set; }
        public double CantidadAlmacen { get; set; }
        public double CantidadBloqueado { get; set; }
        public double CantidadCalidad { get; set; }
        public string Ubicacion { get; set; }
        public int MaterialId { get; set; }
        public int? AlmacenId { get; set; }
        public int ClaseDeValoracionId { get; set; }
        //public int CentroId { get; set; }
        public int DetalleStockEspecialId { get; set; }
    }
}
