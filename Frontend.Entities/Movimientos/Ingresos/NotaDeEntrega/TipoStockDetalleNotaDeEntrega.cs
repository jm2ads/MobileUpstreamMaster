using System.Collections.Generic;

namespace Frontend.Business.Movimientos.Ingresos
{
    public class TipoStockDetalleNotaDeEntrega
    {
        public const string LIBRE_UTILIZACION = "";
        public const string CONTROL_DE_CALIDAD = "2";
        public const string BLOQUEADO = "3";

        public Dictionary<string, string> TipoStock { get; set; } = new Dictionary<string, string>()
        {
            { "", "Libre utilización" },
            { "2", "Control de calidad" },
            { "3", "Bloqueado" }
        };
    }
}
