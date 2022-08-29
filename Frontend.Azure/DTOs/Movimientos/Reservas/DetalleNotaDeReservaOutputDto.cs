namespace Frontend.Azure.DTOs.Movimientos.Reservas
{
    public class DetalleNotaDeReservaOutputDto
    {
        public double CantidadIngresada { get; set; }
        public string TextoPosicion { get; set; }
        public string PuestoDeDescarga { get; set; }
        public string DestinatarioDeMercancia { get; set; }
        public bool EsEntregaFinal { get; set; }
        public int DetalleReservaId { get; set; }
        public string TipoStock { get; set; }
        public int StockEspecialId { get; set; }
        public int ClaseDeValoracionId { get; set; }
        public int AlmacenId { get; set; }
        public string Posicion { get; set; }
        public string Unidad { get; set; }
        public double CantidadReserva { get; set; }
        public int CentroId { get; set; }
        public int MaterialId { get; set; }
    }
}
