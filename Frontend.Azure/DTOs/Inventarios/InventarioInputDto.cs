using Frontend.Commons.Enums;
using System.Collections.Generic;

namespace Frontend.Azure.DTOs
{
    public class InventarioInputDto
    {
        public int Id { get; set; }
        public string NumeroProvisorio { get; set; }
        public string ProvisorioAnterior { get; set; }
        public string NumeroSAP { get; set; }
        public bool EsProvisorio { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaModificacion { get; set; }
        public string FechaRecuento { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public EstadoInventario EstadoInventarioId { get; set; }
        public int CentroId { get; set; }
        public int? AlmacenId { get; set; }
        public int StockEspecialId { get; set; }
        public int Ejercicio { get; set; }
        public string Comentario { get; set; }
        public string AlmacenCodigo { get; set; }
        public string StockEspecialDescripcion { get; set; }
        public bool HayDiferencia { get; set; }
        public bool HayConteoErroneo { get; set; }
        public EstadoConteoEnum EstadoConteo { get; set; }

        public List<DetalleInventarioInputDto> DetallesInventario { get; set; }
    }
}
