﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Frontend.Core.Areas.Inventarios.IViewModels
{
    public interface IListaInventarioProvisorioViewModel
    {
        ICommand GetInventariosProvisoriosCommand { get; set; }
    }
}
