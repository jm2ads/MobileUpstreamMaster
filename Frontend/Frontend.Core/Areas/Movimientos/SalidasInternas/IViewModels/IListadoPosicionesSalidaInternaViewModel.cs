using System.Windows.Input;

namespace Frontend.Core.Areas.Movimientos.SalidasInternas.IViewModels
{
    public interface IListadoPosicionesSalidaInternaViewModel
    {
        ICommand FiltroPosicionCommand { get; set; }
    }
}
