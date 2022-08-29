using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Frontend.Core.Areas.InventariosAprobacionMasiva.IViewModels
{
    public interface IListadoDeMaterialesAprobacionViewModel
    {
        ICommand FiltroMaterialCommand { get; set; }
    }
}
