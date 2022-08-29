using System.Collections.Generic;
using System.Linq;

namespace Frontend.Business.Movimientos.Ingresos.Validations
{
    public class DetalleNotaDeEntregaValidator
    {
        public bool Validate(DetalleNotaDeEntrega detalleNotaDeEntrega)
        {
            return detalleNotaDeEntrega != null && detalleNotaDeEntrega.Almacen != null
                && detalleNotaDeEntrega.ClaseDeValoracion != null;
        }

        public bool Validate(IList<DetalleNotaDeEntrega> detalleDetalleNotaDeEntregaList)
        {
            return detalleDetalleNotaDeEntregaList.All(x => x.Almacen != null
                && x.ClaseDeValoracion != null);
        }
    }
}