using System.Reflection;

using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleMaterialTraslado311View : ContentPage
    {
        private readonly IDetalleMaterialTraslado311ViewModel detalleMaterialTraslado311ViewModel;

        public DetalleMaterialTraslado311View(IDetalleMaterialTraslado311ViewModel detalleMaterialTraslado311ViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IDetalleMaterialTraslado311ViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.detalleMaterialTraslado311ViewModel = detalleMaterialTraslado311ViewModel;
        }
    }
}