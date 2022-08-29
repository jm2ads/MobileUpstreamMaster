using Frontend.Business.Inventarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Core.Areas.InventariosAprobacionMasiva.Models
{
    public class AgregarComentarioModel
    {
        public bool EsAprobacion { get; set; }
        public IList<AprobacionMasivaDetalleModel> ListaDetalles { get; set; }
    }
}
