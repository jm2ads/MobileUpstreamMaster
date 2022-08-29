using System;

namespace Frontend.Business.Movimientos.Core
{
    public class MovimientoLogFactory
    {
        public MovimientoLogFactory()
        {
        }

        public MovimientoLog Create()
        {
            var movimientoLog = new MovimientoLog();
            movimientoLog.FechaCreacion = DateTime.UtcNow;
            return movimientoLog;
        }
    }
}
