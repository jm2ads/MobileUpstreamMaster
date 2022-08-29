using System.Reflection;

using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleMaterialTraslado309View : ContentPage
    {
        private readonly IDetalleMaterialTraslado309ViewModel detalleMaterialTraslado309ViewModel;

        public DetalleMaterialTraslado309View(IDetalleMaterialTraslado309ViewModel detalleMaterialTraslado309ViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IDetalleMaterialTraslado309ViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.detalleMaterialTraslado309ViewModel = detalleMaterialTraslado309ViewModel;
        }
    }
}