using System.Collections.Generic;
using System.Linq;

namespace Frontend.Business.Movimientos.NotasDeReservas.Searchers
{
    public class TipoStockSearcher
    {
        public static string LibreUtilizacion = "";
        public static string ControlDeCalidad = "2";
        public static string Bloqueado = "3";

        private IList<TipoStock> TiposStock;

        public TipoStockSearcher()
        {
            TiposStock = new List<TipoStock>()
            {
                new TipoStock() {Codigo = LibreUtilizacion, Descripcion = "Libre Utilización" },
                new TipoStock() {Codigo = ControlDeCalidad, Descripcion = "2 - Control de calidad" },
                new TipoStock() {Codigo = Bloqueado, Descripcion = "3 - Bloqueado" }
            };
        }

        public IList<TipoStock> GetAll()
        {
            return TiposStock;
        }

        public TipoStock GetByCodigo(string codigo)
        {
            return TiposStock.FirstOrDefault(x => x.Codigo == codigo);
        }
    }
}
