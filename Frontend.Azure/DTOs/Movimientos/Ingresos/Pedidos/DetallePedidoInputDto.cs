using System.Collections.Generic;

namespace Frontend.Azure.DTOs
{
    public class DetallePedidoInputDto
    {
        public int Id { get; set; }
        public string Posicion { get; set; }
        public string Unidad { get; set; }
        public double Tolerancia { get; set; }
        public int PedidoId { get; set; }
        public int MaterialId { get; set; }
        public int? ClaseDeValoracionId { get; set; }
        public int? AlmacenId { get; set; }
        public int CentroId { get; set; }
        public string TipoDeStock { get; set; }       //llega un caracter: '','2' o '3'
        public int StockEspecialId { get; set; }
        public List<DetallePedidoPosicionInputDto> DetallesPedidoPosicion { get; set; }
    }
}
