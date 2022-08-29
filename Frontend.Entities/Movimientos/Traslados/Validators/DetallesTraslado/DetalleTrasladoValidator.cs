using System.Collections.Generic;

namespace Frontend.Business.Movimientos.Traslados.Validators
{
    public class DetalleTrasladoValidator
    {
        public bool Validate(DetalleTraslado detalleTraslado)
        {
            bool ret = true;

            if (detalleTraslado.Traslado.ClaseDeMovimientoCodigo == ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_309])
            {
                ret = !string.IsNullOrWhiteSpace(detalleTraslado.CodigoMaterial)
                    && detalleTraslado.Almacen != null
                    && detalleTraslado.ClaseDeValoracion != null;
            }

            if (detalleTraslado.Traslado.ClaseDeMovimientoCodigo == ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_311])
            {
                ret = detalleTraslado.Almacen != null
                    && detalleTraslado.ClaseDeValoracion != null;
            }

            if (detalleTraslado.Traslado.ClaseDeMovimientoCodigo == ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_542])
            {
                ret = detalleTraslado.Almacen != null
                    && string.IsNullOrWhiteSpace(detalleTraslado.Proveedor);
            }

            if (detalleTraslado.Traslado.ClaseDeMovimientoCodigo != ClaseDeMovimientoTraslado.ClaseDeMovimiento[ClaseDeMovimientoTraslado.CLASE_321])
            {
                return ret && detalleTraslado.Cantidad > 0
                  && detalleTraslado.Cantidad <= detalleTraslado.Stock.CantidadAlmacen
                  && detalleTraslado.StockEspecial != null;
            }

            return ret && detalleTraslado.Cantidad > 0
                && detalleTraslado.Cantidad <= detalleTraslado.Stock.CantidadCalidad
                && detalleTraslado.StockEspecial != null;
        }

        public bool Validate(IList<DetalleTraslado> list)
        {
            foreach (var item in list)
            {
                if (!Validate(item))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
