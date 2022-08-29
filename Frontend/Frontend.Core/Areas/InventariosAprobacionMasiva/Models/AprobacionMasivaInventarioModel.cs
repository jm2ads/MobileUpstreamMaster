using Frontend.Business.DetallesInventario;
using Frontend.Business.Inventarios;
using Frontend.Core.Commons.Observables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frontend.Core.Areas.InventariosAprobacionMasiva.Models
{
    public class AprobacionMasivaInventarioModel : NotificationObject
    {
        public Inventario Inventario { get; set; }
        private List<AprobacionMasivaDetalleModel> _detalles;
        public List<AprobacionMasivaDetalleModel> Detalles
        {
            get { return _detalles; }
            set
            {
                SetProperty(ref _detalles, value);
            }
        }

        public AprobacionMasivaInventarioModel(Inventario inventario)
        {
            Inventario = inventario;
            Detalles = new List<AprobacionMasivaDetalleModel>();
            Detalles.AddRange(inventario.DetallesInventario.Select(detalle => new AprobacionMasivaDetalleModel(detalle)));
        }
    }
}
