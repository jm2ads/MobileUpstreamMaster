using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Business.Movimientos.Traslados.Helper
{
    public class ClaseDeMovimientoTrasladoHelper
    {
        private readonly List<string> listaClaseDeMovimiento;

        public ClaseDeMovimientoTrasladoHelper()
        {
            listaClaseDeMovimiento = new List<string>();
        }
        public List<string> GetClasesDeMovimientoTraslados()
        {
            listaClaseDeMovimiento.Add(ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_309]);
            listaClaseDeMovimiento.Add(ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_311]);
            listaClaseDeMovimiento.Add(ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_321]);
            listaClaseDeMovimiento.Add(ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_541]);
            listaClaseDeMovimiento.Add(ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_542]);
            return listaClaseDeMovimiento;
        }
    }
}
