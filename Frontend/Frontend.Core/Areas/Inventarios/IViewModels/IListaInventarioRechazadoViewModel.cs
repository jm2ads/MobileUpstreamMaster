using System.Windows.Input;

namespace Frontend.Core.Areas.Inventarios.IViewModels
{
    public interface IListaInventarioRechazadoViewModel
    {
        ICommand GetInventariosRechazadosCommand { get; set; }
        ICommand VerComentarioCommnad { get; set; }
    }
}
