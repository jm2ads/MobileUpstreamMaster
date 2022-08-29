using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Frontend.Core.Areas.Movimientos.SalidaTraspaso.IViewModels
{
    public interface IListadoPosicionesSalidaTraspasoViewModel
    {
        ICommand FiltroPosicionCommand { get; set; }
    }
}
