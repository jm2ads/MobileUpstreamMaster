using System.Windows.Input;

namespace Frontend.Core.Areas.Movimientos.Traslados.IViewModels
{
    public interface IListaPosicionesTraslado309ViewModel
    {
        ICommand FiltroPosicionCommand { get; set; }
        ICommand OnBackButtonPressedCommnad { get; set; }
    }
}
