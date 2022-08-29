using Frontend.Azure.DTOs.DetallesInventarios;
using System.Collections.Generic;

namespace Frontend.Azure.DTOs.Inventarios
{
    public class CrearInventarioInputDto
    {
        public string NumeroProvisorio { get; set; }
        public string ProvisorioAnterior { get; set; }
        public bool EsProvisorio { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaRecuento { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public int EstadoInventarioId { get; set; }
        public int CentroId { get; set; }
        public int? AlmacenId { get; set; }
        public int StockEspecialId { get; set; }
        public string Comentario { get; set; }

        public List<DetalleCrearInventarioInputDto> DetallesInventario { get; set; }
    }
}
