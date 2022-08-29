namespace Frontend.Azure.DTOs.Movimientos.Traslados
{
    public class DetalleTrasladoOutputDto
    {
        public int StockId { get; set; }
        public int CentroId { get; set; }
        public string CodigoMaterial { get; set; }
        public int? AlmacenId { get; set; }
        public double Cantidad { get; set; }
        public int ClaseDeValoracionId { get; set; }
        public int? StockEspecialId { get; set; }
        public string Proveedor { get; set; }
        public string ElementoPEP { get; set; }
        public string Posicion { get; set; }
        public string Textobreve { get; set; }
    }
}
