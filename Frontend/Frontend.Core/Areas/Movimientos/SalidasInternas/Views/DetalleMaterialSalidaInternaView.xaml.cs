using System.Reflection;

using Frontend.Core.Areas.Movimientos.SalidasInternas.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleMaterialSalidaInternaView : ContentPage
    {
        private readonly IDetalleMaterialSalidaInternaViewModel detalleMaterialSalidaInternaViewModel;

        public DetalleMaterialSalidaInternaView(IDetalleMaterialSalidaInternaViewModel detalleMaterialSalidaInternaViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IDetalleMaterialSalidaInternaViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.detalleMaterialSalidaInternaViewModel = detalleMaterialSalidaInternaViewModel;
        }
    }
}