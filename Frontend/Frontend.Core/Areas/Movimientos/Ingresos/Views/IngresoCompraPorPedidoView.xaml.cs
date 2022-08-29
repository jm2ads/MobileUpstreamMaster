using Frontend.Core.Areas.Movimientos.Ingresos.IViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Areas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngresoCompraPorPedidoView : ContentPage
    {
        private readonly IIngresoCompraPorPedidoViewModel ingresoCompraPorPedidoViewModel;

        public IngresoCompraPorPedidoView(IIngresoCompraPorPedidoViewModel ingresoCompraPorPedidoViewModel)
        {
            InitializeComponent();
            BindingContext = this.ingresoCompraPorPedidoViewModel = ingresoCompraPorPedidoViewModel;
        }
    }
}