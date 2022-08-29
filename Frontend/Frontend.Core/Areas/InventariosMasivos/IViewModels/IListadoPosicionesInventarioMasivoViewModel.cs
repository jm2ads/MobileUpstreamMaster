using System.Windows.Input;

namespace Frontend.Core.Areas.InventariosMasivos.IViewModels
{
    public interface IListadoPosicionesInventarioMasivoViewModel
    {
        ICommand FiltroPosicionCommand { get; set; }
        ICommand OnBackButtonPressedCommnad { get; set; }
    }
}
