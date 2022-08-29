using Frontend.Commons.Enums;
using System.Collections.Generic;

namespace Frontend.Azure.DTOs
{
    public class InventarioOutputDto
    {
        public int CentroId { get; set; }
        public string delta { get; set; }
        public int[] EstadoInventarioIds { get; set; }
    }
}
