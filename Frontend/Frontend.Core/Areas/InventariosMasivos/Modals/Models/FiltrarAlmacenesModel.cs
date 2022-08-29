using Frontend.Business.Almacenes;
using Frontend.Core.Commons.Observables;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Core.Areas.InventariosMasivos.Modals.Models
{
    public class FiltrarAlmacenesModel : NotificationObject
    {
        public Almacen Almacen { get; set; }
        private bool _esExcluido = false;
        public bool EsExcluido
        {
            get { return _esExcluido; }
            set
            {
                SetProperty(ref _esExcluido, value);
            }
        }
    }
}
