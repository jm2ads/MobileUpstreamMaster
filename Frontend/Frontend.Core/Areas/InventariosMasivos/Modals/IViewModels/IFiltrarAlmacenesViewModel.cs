using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.InventariosMasivos.IViewModels
{
    public interface IFiltrarAlmacenesViewModel
    {
        ICommand FiltroAlmacenCommand { get; set; }
    }
}
