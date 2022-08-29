using System.Windows.Input;

namespace Frontend.Core.Areas.CambiosUbicacion.Modals.IViewModels
{
    public interface ISeleccionarAlmacenesViewModel
    {
        ICommand FiltroAlmacenCommand { get; set; }
    }
}
