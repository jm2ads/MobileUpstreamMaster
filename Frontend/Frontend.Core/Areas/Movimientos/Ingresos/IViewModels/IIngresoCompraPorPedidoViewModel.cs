using System.Windows.Input;

namespace Frontend.Core.Areas.Movimientos.Ingresos.IViewModels
{
    public interface IIngresoCompraPorPedidoViewModel
    {
        ICommand GetAllPedidosCommand { get; set; }
    }
}
