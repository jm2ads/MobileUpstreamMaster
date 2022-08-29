using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Frontend.Core.Areas.Movimientos.SalidasInternas.IViewModels
{
    public interface ISalidaPorVentaInternaMaterialViewModel
    {
        ICommand GetAllMaterialCommand { get; set; }
    }
}
