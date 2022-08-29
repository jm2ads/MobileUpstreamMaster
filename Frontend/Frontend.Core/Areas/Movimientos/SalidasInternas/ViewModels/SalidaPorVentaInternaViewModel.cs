using Frontend.Core.Areas.Movimientos.SalidasInternas.IViewModels;
using Frontend.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Core.Areas.Movimientos.SalidasInternas.ViewModels
{
    public class SalidaPorVentaInternaViewModel : BaseViewModel, ISalidaPorVentaInternaViewModel
    {
        public SalidaPorVentaInternaViewModel()
        {
            Title = "Salida por venta interna";
        }
    }
}
