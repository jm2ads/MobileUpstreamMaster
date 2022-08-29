using System;
using System.Collections.Generic;

namespace Frontend.Business.Movimientos.Traslados.Core
{
    public class TrasladoFactory
    {
        public Traslado Create(string usuarioCreacion)
        {
            return new Traslado()
            {
                NumeroProvisorio = "-" + DateTime.Now.ToString("ddMMyyhhmmssff"),
                Usuario = usuarioCreacion,
                DetallesTraslado = new List<DetalleTraslado>()
            };
        }
    }
}
