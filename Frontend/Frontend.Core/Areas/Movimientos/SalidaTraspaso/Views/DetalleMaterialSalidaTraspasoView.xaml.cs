using System.Reflection;

using Frontend.Core.Areas.Movimientos.SalidaTraspaso.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleMaterialSalidaTraspasoView : ContentPage
    {
        private readonly IDetalleMaterialSalidaTraspasoViewModel detalleMaterialSalidaTraspasoViewModel;

        public DetalleMaterialSalidaTraspasoView(IDetalleMaterialSalidaTraspasoViewModel detalleMaterialSalidaTraspasoViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IDetalleMaterialSalidaTraspasoViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.detalleMaterialSalidaTraspasoViewModel = detalleMaterialSalidaTraspasoViewModel;
        }
    }
}