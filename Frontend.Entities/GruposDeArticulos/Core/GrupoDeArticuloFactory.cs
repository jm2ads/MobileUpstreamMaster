using Frontend.Business.Materiales;
using System.Collections.Generic;

namespace Frontend.Business.GruposDeArticulos.Core
{
    public class GrupoDeArticuloFactory
    {
        public GrupoDeArticulo Create()
        {
            return new GrupoDeArticulo()
            {
                //Materiales = new List<Material>()
            };
        }
    }
}
