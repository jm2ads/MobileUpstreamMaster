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
	public partial class CabeceraDePedidoView : ContentPage
	{
        private readonly ICabeceraDePedidoViewModel cabeceraDePedidoViewModel;

        public CabeceraDePedidoView (ICabeceraDePedidoViewModel cabeceraDePedidoViewModel)
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(ICabeceraDePedidoViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.cabeceraDePedidoViewModel = cabeceraDePedidoViewModel;
        }
	}
}