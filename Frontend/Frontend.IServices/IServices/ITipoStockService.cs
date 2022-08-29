using Frontend.Business.DetallesInventario.TiposStock;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.IServices.IServices
{
    public interface ITipoStockService
    {
        IList<TipoStock> GetAll();
        TipoStock GetByCodigo(string codigo);
        TipoStock GetById(int id);
    }
}
