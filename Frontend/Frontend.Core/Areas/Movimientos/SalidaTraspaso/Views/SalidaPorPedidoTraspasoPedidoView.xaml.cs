using Frontend.Core.Areas.Movimientos.SalidaTraspaso.IViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Areas.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SalidaPorPedidoTraspasoPedidoView : ContentPage
	{
        private readonly ISalidaPorPedidoTraspasoPedidoViewModel salidaPorPedidoTraspasoPedidoViewModel;

        public SalidaPorPedidoTraspasoPedidoView(ISalidaPorPedidoTraspasoPedidoViewModel salidaPorPedidoTraspasoPedidoViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(ISalidaPorPedidoTraspasoPedidoViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.salidaPorPedidoTraspasoPedidoViewModel = salidaPorPedidoTraspasoPedidoViewModel;
        }
    }
}