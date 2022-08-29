using System.Collections.Generic;
using System.Linq;

namespace Frontend.Business.Movimientos.NotasDeReservas.Validators
{
    public class DetalleNotaDeReservaValidator
    {
        public bool Validate(DetalleNotaDeReserva detalleNotaDeReserva)
        {
            return detalleNotaDeReserva.Almacen != null
                && detalleNotaDeReserva.ClaseDeValoracion != null;
        }
        public bool Validate(IList<DetalleNotaDeReserva> detalleNotaDeReservaList)
        {
            return detalleNotaDeReservaList.All(x=>x.Almacen != null
                && x.ClaseDeValoracion != null
                && string.IsNullOrWhiteSpace(x.TextoPosicion));
        }
    }
}
