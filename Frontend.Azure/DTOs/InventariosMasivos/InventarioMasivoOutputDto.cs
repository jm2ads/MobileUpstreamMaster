using System;
using System.Collections.Generic;

namespace Frontend.Azure.DTOs.InventariosMasivos
{
    public class InventarioMasivoOutputDto
    {
        public string NumeroProvisorio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaDocumento { get; set; }
        public string UsuarioCreacion { get; set; }
        public string AlmacenesExcluidos { get; set; }
        public int CentroId { get; set; }
        public IList<DetalleInventarioMasivoOutputDto> DetallesInventarioMasivo { get; set; }
    }
}
