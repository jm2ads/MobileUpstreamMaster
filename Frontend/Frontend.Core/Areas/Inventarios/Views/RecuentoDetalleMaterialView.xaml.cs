using Frontend.Core.Areas.Inventarios.IViewModels;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecuentoDetalleMaterialView : ContentPage
    {
        private readonly IRecuentoDetalleMaterialViewModel recuentoDetalleMaterialViewModel;

        public RecuentoDetalleMaterialView(IRecuentoDetalleMaterialViewModel recuentoDetalleMaterialViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IInformacionInventarioViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.recuentoDetalleMaterialViewModel = recuentoDetalleMaterialViewModel;
        }
    }
}