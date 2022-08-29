using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Frontend.Core.Areas.Movimientos.Traslados.IViewModels
{
    public interface IListaPosicionesTraslado321ViewModel
    {
        ICommand FiltroPosicionCommand { get; set; }
        ICommand OnBackButtonPressedCommnad { get; set; }
    }
}
