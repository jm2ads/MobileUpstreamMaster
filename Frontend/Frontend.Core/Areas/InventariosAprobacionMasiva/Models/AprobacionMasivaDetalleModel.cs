using Frontend.Business.DetallesInventario;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Core.Areas.InventariosAprobacionMasiva.Models
{
    public class AprobacionMasivaDetalleModel
    {
        public DetalleInventario DetalleInventario { get; set; }
        public bool IsSelected { get; set; }

        public AprobacionMasivaDetalleModel(DetalleInventario detalleInventario)
        {
            DetalleInventario = detalleInventario;
            IsSelected = false;
        }
    }
}
