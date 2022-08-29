using Frontend.Business.Almacenes;
using Frontend.Business.Centros;
using Frontend.Business.Synchronizer;
using System;
using System.Collections.Generic;

namespace Frontend.Business.CambiosUbicacion.Core
{
    public class CambioUbicacionFactory
    {
        public CambioUbicacionFactory()
        {
        }

        public CambioUbicacion Create(Centro centro, string usuarioActivo)
        {
            var cambioUbicacion = new CambioUbicacion()
            {
                Centro = centro,
                IdCentro = centro.Id,
                FechaCreacion = DateTime.Now,
                SyncState = SyncState.New,
                Usuario = usuarioActivo,
                AlmacenesIncluidos = new List<Almacen>()
            };

            return cambioUbicacion;
        }
    }
}
