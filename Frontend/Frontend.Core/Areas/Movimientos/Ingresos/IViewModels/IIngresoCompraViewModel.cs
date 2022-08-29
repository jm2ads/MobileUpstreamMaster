using System.Windows.Input;

namespace Frontend.Core.Areas.Movimientos.Ingresos.IViewModels
{
    public interface IIngresoCompraViewModel
    {
        ICommand UploadCommand { get; set; }
    }
}
