using Frontend.Business.Movimientos.NotasDeReservas;
using Frontend.Business.Movimientos.SalidasInternas;
using Frontend.Core.Commons.Observables;
using System;

namespace Frontend.Core.Areas.Movimientos.SalidaTraspaso.Models
{
    public class ListadoPosicionesSalidaInternaModel : NotificationObject
    {
        private DetalleSalidaInterna _detalleSalidaInterna;
        public DetalleSalidaInterna detalleSalidaInterna
        {
            get { return _detalleSalidaInterna; }
            set
            {
                SetProperty(ref _detalleSalidaInterna, value);
            }
        }

        public bool EsContado
        {
            get { return detalleSalidaInterna.EsContado; }
            set
            {
                if (value && detalleSalidaInterna.CantidadEnviada == 0 && !detalleSalidaInterna.EsContado)
                {
                    detalleSalidaInterna.CantidadEnviada = detalleSalidaInterna.CantidadPendiente;
                }
                else if (!value)
                {
                    detalleSalidaInterna.CantidadEnviada = 0;
                }
                var _esContado = detalleSalidaInterna.EsContado;
                SetProperty(ref _esContado, value, onChanged: EsContadoAction);
                RaisePropertyChanged("detalleSalidaInterna");
            }
        }

        public Action EsContadoAction { get; set; }
    }
}
