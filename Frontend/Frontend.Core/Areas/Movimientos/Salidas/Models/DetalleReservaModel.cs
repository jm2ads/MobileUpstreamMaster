using Frontend.Business.Movimientos.NotasDeReservas;
using Frontend.Core.Commons.Observables;
using System;

namespace Frontend.Core.Areas.Movimientos.Salidas.Models
{
    public class DetalleReservaModel : NotificationObject
    {
        private DetalleNotaDeReserva _detalleNotaDeReserva;
        public DetalleNotaDeReserva DetalleNotaDeReserva
        {
            get { return _detalleNotaDeReserva; }
            set
            {
                SetProperty(ref _detalleNotaDeReserva, value);
            }
        }

        public bool EsContado
        {
            get { return DetalleNotaDeReserva.EsContado; }
            set
            {
                if (value && DetalleNotaDeReserva.CantidadIngresada == 0 && !DetalleNotaDeReserva.EsContado)
                {
                    DetalleNotaDeReserva.CantidadIngresada = DetalleNotaDeReserva.DetalleReserva.CantidadReserva;
                }
                else if (!value)
                {
                    DetalleNotaDeReserva.CantidadIngresada = 0;
                }
                var _esContado = DetalleNotaDeReserva.EsContado;
                SetProperty(ref _esContado, value, onChanged: EsContadoAction);
                RaisePropertyChanged("DetalleNotaDeReserva");
            }
        }

        public Action EsContadoAction { get; set; }

        public string Ubicacion { get; set; }
    }
}
