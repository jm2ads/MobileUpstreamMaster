using Frontend.Business.DetallesInventario.TiposStock;
using Frontend.Business.DetallesInventario.TiposStock.Searchers;
using Frontend.IServices.IServices;
using System.Collections.Generic;

namespace Frontend.Services.Services
{
    public class TipoStockService : ITipoStockService
    {
        private readonly TipoStockSearcher searcher;

        public TipoStockService(TipoStockSearcher searcher)
        {
            this.searcher = searcher;
        }

        public IList<TipoStock> GetAll()
        {
            return searcher.GetAll();
        }

        public TipoStock GetByCodigo(string codigo)
        {
            return searcher.GetByCodigo(codigo);
        }

        public TipoStock GetById(int id)
        {
            return searcher.GetById(id);
        }
    }
}
