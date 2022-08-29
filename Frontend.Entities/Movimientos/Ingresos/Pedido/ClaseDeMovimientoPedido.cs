using System.Collections.Generic;

namespace Frontend.Business.Movimientos.Ingresos
{
    public class ClaseDeMovimientoPedido
    {
        public const int CLASE_101 = 1;
        public const int CLASE_Z01 = 2;
        public const int CLASE_103 = 3;
        public const int CLASE_105 = 4;

        public static Dictionary<int, string> ClaseDeMovimiento{ get; set; } = new Dictionary<int, string>()
        {
            { 1, "101" },
            { 2, "Z01" },
            { 3, "103" },
            { 4, "105" }
        };
    }
}
