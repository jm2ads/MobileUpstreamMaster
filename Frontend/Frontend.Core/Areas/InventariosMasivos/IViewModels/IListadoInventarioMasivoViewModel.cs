using System.Windows.Input;

namespace Frontend.Core.Areas.InventariosMasivos.IViewModels
{
    public interface IListadoInventarioMasivoViewModel
    {
        ICommand RefreshListCommnad { get; set; }
    }
}
