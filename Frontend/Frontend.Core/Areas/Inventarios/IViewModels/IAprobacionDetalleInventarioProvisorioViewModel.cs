using System.Windows.Input;

namespace Frontend.Core.Areas.Inventarios.IViewModels
{
    public interface IAprobacionDetalleInventarioProvisorioViewModel
    {
        ICommand FiltroPosicionCommand { get; set; }
        ICommand RefreshCommand { get; set; }
        ICommand ComentarioCommand { get; set; }
        ICommand GetInventarioProvisorioCommand { get; set; }

    }
}
