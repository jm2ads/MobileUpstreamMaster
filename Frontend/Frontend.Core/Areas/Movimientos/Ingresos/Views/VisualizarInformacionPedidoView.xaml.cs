using Frontend.Core.Areas.Movimientos.Ingresos.IViewModels;
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
	public partial class VisualizarInformacionPedidoView : ContentPage
	{
        private readonly IVisualizarInformacionPedidoViewModel visualizarInformacionPedidoViewModel;

        public VisualizarInformacionPedidoView (IVisualizarInformacionPedidoViewModel visualizarInformacionPedidoViewModel)
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IVisualizarInformacionPedidoViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.visualizarInformacionPedidoViewModel = visualizarInformacionPedidoViewModel;
        }
	}
}