using Frontend.Business.Movimientos;

namespace Frontend.Azure.DTOs.Movimientos.ClasesDeMovimientos
{
    public class ClaseDeMovimientoInputDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public Movimiento Movimiento { get; set; }
    }
}
