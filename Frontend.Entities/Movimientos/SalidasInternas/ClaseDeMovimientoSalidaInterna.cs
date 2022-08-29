using System.Collections.Generic;

namespace Frontend.Business.Movimientos.SalidasInternas
{
    public class ClaseDeMovimientoSalidaInterna
    {
        public const int CLASE_643 = 1;
        public const int CLASE_351 = 2;

        public static Dictionary<int, string> ClaseDeMovimiento { get; set; } = new Dictionary<int, string>()
        {
            { 1, "643" },
            { 2, "351" }
        };
    }
}
