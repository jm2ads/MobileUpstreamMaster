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
	public partial class DetalleDeMaterialPedidoView : ContentPage
    {
        private readonly IDetalleDeMaterialPedidoViewModel detalleDeMaterialPedidoViewModel;

        public DetalleDeMaterialPedidoView(IDetalleDeMaterialPedidoViewModel detalleDeMaterialPedidoViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IDetalleDeMaterialPedidoViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.detalleDeMaterialPedidoViewModel = detalleDeMaterialPedidoViewModel;
        }
    }
}