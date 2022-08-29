using System;

namespace Frontend.Business.Inventarios.Core
{
    public class InventarioLogFactory
    {
        public InventarioLogFactory()
        {
        }

        public InventarioLog Create()
        {
            var inventarioLog = new InventarioLog();
            inventarioLog.FechaCreacion = DateTime.UtcNow;
            return inventarioLog;
        }
    }
}
