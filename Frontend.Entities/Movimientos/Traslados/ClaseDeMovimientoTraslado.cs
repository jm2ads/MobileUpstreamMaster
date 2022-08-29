using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Business.Movimientos.Traslados
{
    public class ClaseDeMovimientoTraslado
    {
        public const int CLASE_309 = 1;
        public const int CLASE_311 = 2;
        public const int CLASE_541 = 3;
        public const int CLASE_542 = 4;
        public const int CLASE_321 = 5;

        public static Dictionary<int, string> ClaseDeMovimiento { get; set; } = new Dictionary<int, string>()
        {
            { 1, "309" },
            { 2, "311" },
            { 3, "541" },
            { 4, "542" },
            { 5, "321" }
        };
    }
}
