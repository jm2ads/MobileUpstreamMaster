using System.Windows.Input;

namespace Frontend.Core.Areas.Inventarios.IViewModels
{
    public interface IRecuentoDetalleInventarioViewModel
    {
        ICommand FiltroPosicionCommand { get; set; }
    }
}
