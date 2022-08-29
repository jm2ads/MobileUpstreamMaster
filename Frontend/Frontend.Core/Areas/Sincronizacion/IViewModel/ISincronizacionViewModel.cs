using System.Windows.Input;

namespace Frontend.Core.Areas.Sincronizacion.IViewModel
{
    public interface ISincronizacionViewModel
    {
        ICommand SyncCommand { get; set; }

        ICommand SyncParcialCommand { get; set; }
    }
}
