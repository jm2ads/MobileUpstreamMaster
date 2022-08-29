using Frontend.Business.Almacenes;
using Frontend.Core.Commons.Observables;

namespace Frontend.Core.Areas.CambiosUbicacion.Modals.Models
{
    public class SeleccionarAlmacenesModel : NotificationObject
    {
        public Almacen Almacen { get; set; }
        private bool _esIncluido = false;
        public bool EsIncluido
        {
            get { return _esIncluido; }
            set
            {
                SetProperty(ref _esIncluido, value);
            }
        }
    }
}
