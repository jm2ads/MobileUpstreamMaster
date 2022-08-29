using Frontend.Azure.DTOs.Movimientos.ClasesDeMovimientos;
using System.Collections.Generic;

namespace Frontend.Azure.DTOs.Movimientos
{
    public class MovimientoInputDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public IList<ClaseDeMovimientoInputDto> ClasesDeMovimiento { get; set; }
    }
}
