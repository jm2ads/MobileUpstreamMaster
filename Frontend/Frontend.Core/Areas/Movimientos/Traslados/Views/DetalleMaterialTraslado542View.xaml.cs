using System.Reflection;

using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleMaterialTraslado542View : ContentPage
    {
        private readonly IDetalleMaterialTraslado542ViewModel detalleMaterialTrasladoViewModel;

        public DetalleMaterialTraslado542View(IDetalleMaterialTraslado542ViewModel detalleMaterialTrasladoViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IDetalleMaterialTraslado542ViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.detalleMaterialTrasladoViewModel = detalleMaterialTrasladoViewModel;
        }
    }
}