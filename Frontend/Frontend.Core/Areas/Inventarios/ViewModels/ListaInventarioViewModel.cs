using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Core.Areas.Inventarios.ViewModels
{
    public class ListaInventarioViewModel : BaseViewModel, IListaInventarioViewModel
    {
        public ListaInventarioViewModel()
        {
            Title = "Listado de Inventarios";
        }
    }
}
