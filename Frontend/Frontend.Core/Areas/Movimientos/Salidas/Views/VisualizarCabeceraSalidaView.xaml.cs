using System.Reflection;

using Frontend.Core.Areas.Movimientos.Salidas.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisualizarCabeceraSalidaView : ContentPage
    {
        private readonly IVisualizarCabeceraSalidaViewModel visualizarCabeceraSalidaViewModel;

        public VisualizarCabeceraSalidaView(IVisualizarCabeceraSalidaViewModel visualizarCabeceraSalidaViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IVisualizarCabeceraSalidaViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.visualizarCabeceraSalidaViewModel = visualizarCabeceraSalidaViewModel;
        }
    }
}