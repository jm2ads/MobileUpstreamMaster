using System.Windows.Input;

namespace Frontend.Core.Areas.Inventarios.IViewModels
{
    public interface IAprobacionDetalleInventarioSapViewModel
    {
        ICommand ComentarioCommand { get; set; }
        ICommand GetInventarioSapCommand { get; set; }

    }
}
