using System.Windows.Input;

namespace Frontend.Core.Areas.Movimientos.Salidas.IViewModels
{
    public interface IDetalleSalidaViewModel
    {
        ICommand FiltroPosicionCommand { get; set; }
    }
}
