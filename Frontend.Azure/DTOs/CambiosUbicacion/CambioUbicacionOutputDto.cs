using System;

namespace Frontend.Azure.DTOs.CambiosUbicacion
{
    public class CambioUbicacionOutputDto
    {
        public string Ubicacion { get; set; }
        public string UsuarioAprobador { get; set; }
        public DateTime FechaEnvio { get; set; }
        public int[] AlmacenesIncluidosIds { get; set; }
        public int CentroId { get; set; }
        public int MaterialId { get; set; }
    }
}
