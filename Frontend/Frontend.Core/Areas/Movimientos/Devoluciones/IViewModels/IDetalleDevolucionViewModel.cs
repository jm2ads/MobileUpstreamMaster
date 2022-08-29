using System.Windows.Input;

namespace Frontend.Core.Areas.Movimientos.Devoluciones.IViewModels
{
    public interface IDetalleDevolucionViewModel
    {
        ICommand FiltroPosicionCommand { get; set; }
    }
}
