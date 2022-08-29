using System.Collections.Generic;

namespace Frontend.Business.Movimientos.SalidasInternas.Validators
{
    public class DetalleSalidaInternaValidator
    {
        public bool Validate(IList<DetalleSalidaInterna> list)
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

        public bool Validate(DetalleSalidaInterna detalleSalidaInterna)
        {
            return !detalleSalidaInterna.EsContado 
                || (detalleSalidaInterna.Almacen != null
                && detalleSalidaInterna.ClaseDeValoracion != null
                && detalleSalidaInterna.CantidadEnviada > 0);
        }
    }
}
