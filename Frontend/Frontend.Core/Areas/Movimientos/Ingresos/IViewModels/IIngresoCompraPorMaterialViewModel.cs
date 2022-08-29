using System.Windows.Input;

namespace Frontend.Core.Areas.Movimientos.Ingresos.IViewModels
{
    public interface IIngresoCompraPorMaterialViewModel
    {
        ICommand GetAllMaterialCommand { get; set; }
    }
}
