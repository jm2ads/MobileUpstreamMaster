using Frontend.Business.Movimientos.Ingresos;
using Frontend.Core.Commons.Observables;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Core.Areas.Movimientos.Ingresos.Models
{
    public class CabeceraDePedidoModel : NotificationObject
    {
        private NotaDeEntrega _notaDeEntrega;
        public NotaDeEntrega notaDeEntrega
        {
            get { return _notaDeEntrega; }
            set
            {
                SetProperty(ref _notaDeEntrega, value);
            }
        }

        private string _claseMovimiento;
        public string claseMovimiento
        {
            get { return _claseMovimiento; }
            set
            {
                SetProperty(ref _claseMovimiento, value);
            }
        }
    }
}
