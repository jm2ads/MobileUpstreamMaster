using System.Reflection;

using Frontend.Core.Areas.Movimientos.Salidas.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;


namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CabeceraSalidaView : ContentPage
    {
        private readonly ICabeceraSalidaViewModel cabeceraSalidaViewModel;

        public CabeceraSalidaView(ICabeceraSalidaViewModel cabeceraSalidaViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(ICabeceraSalidaViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.cabeceraSalidaViewModel = cabeceraSalidaViewModel;
        }
    }
}