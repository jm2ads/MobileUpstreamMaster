using Frontend.Business.Movimientos.Ingresos;
using Frontend.Core.Commons.Observables;
using System;

namespace Frontend.Core.Areas.Movimientos.Ingresos.Models
{
    public class PosicionesDePedidoModel : NotificationObject
    {
        private DetalleNotaDeEntregaPosicion _detalleNotaDeEntregaPosicion;
        public DetalleNotaDeEntregaPosicion detalleNotaDeEntregaPosicion
        {
            get { return _detalleNotaDeEntregaPosicion; }
            set
            {
                SetProperty(ref _detalleNotaDeEntregaPosicion, value);
            }
        }

        public bool EsContado
        {
            get { return detalleNotaDeEntregaPosicion.EsContado; }
            set
            {
                if (value && detalleNotaDeEntregaPosicion.CantidadRecibida == 0 && !detalleNotaDeEntregaPosicion.EsContado)
                {
                    detalleNotaDeEntregaPosicion.CantidadRecibida = detalleNotaDeEntregaPosicion.DetallePedidoPosicion.CantidadPendiente;
                }
                else if (!value)
                {
                    detalleNotaDeEntregaPosicion.CantidadRecibida = 0;
                }
                var _esContado = detalleNotaDeEntregaPosicion.EsContado;
                SetProperty(ref _esContado, value, onChanged: EsContadoAction);
                RaisePropertyChanged("detalleNotaDeEntregaPosicion");
            }
        }

        public Action EsContadoAction { get; set; }
    }
}
