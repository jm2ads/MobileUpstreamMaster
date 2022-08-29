using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frontend.Business.DetallesInventario.TiposStock.Searchers
{
    public class TipoStockSearcher
    {
        private IList<TipoStock> TiposStock;

        public TipoStockSearcher()
        {
            TiposStock = new List<TipoStock>()
            {
                new TipoStock() { Id = TipoStockEnum.LibreUtilizacion.GetHashCode(), Codigo = "1", Descripcion = "1 - Libre Utilización" },
                new TipoStock() { Id = TipoStockEnum.Bloqueado.GetHashCode(), Codigo = "2", Descripcion = "2 - Bloqueado" },
                new TipoStock() { Id = TipoStockEnum.Calidad.GetHashCode(), Codigo = "4", Descripcion = "4 - Calidad" }
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

        public TipoStock GetById(int id)
        {
            return TiposStock.FirstOrDefault(x => x.Id == id);
        }
    }
}
