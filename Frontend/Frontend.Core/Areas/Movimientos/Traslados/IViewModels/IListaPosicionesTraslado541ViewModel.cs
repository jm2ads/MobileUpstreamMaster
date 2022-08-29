using System.Windows.Input;

namespace Frontend.Core.Areas.Movimientos.Traslados.IViewModels
{
    public interface IListaPosicionesTraslado541ViewModel
    {
        ICommand FiltroPosicionCommand { get; set; }
        ICommand OnBackButtonPressedCommnad { get; set; }
    }
}
