using Frontend.Business.Inventarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Core.Areas.Inventarios.Models
{
    public class AgregarComentarioModel
    {
        public List<Inventario> Inventarios { get; set; }
        public bool EsGenerico { get; set; }
        public bool Retornar { get; set; }
    }
}
