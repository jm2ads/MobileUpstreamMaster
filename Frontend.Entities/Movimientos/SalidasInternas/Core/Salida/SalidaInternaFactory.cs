using System.Collections.Generic;

namespace Frontend.Business.Movimientos.SalidasInternas.Core
{
    public class SalidaInternaFactory
    {  
        public SalidaInterna Create()
        {
            return new SalidaInterna()
            {
                DetallesSalidaInterna = new List<DetalleSalidaInterna>()
            };
        }
    }
}