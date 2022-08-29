using System.Collections.Generic;

namespace Frontend.Business.Movimientos.Reservas
{
    public static class ClaseDeMovimientoReserva
    {
        private static Dictionary<string, string> _clasesDeMovimientos = new Dictionary<string, string>
        {
            { "201", "Centro de costo" },
            { "221", "Elemento PEP" },
            { "261", "Orden" },
            { "281", "Grafo" },
            { "202", "Centro de costo" },
            { "222", "Elemento PEP" },
            { "262", "Orden" },
            { "282", "Grafo" },
            { "301", "" },
            { "311", "" }
        };

        public static Dictionary<string, string> Get()
        {
            return _clasesDeMovimientos;
        }
    }
}
