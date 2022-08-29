using System.Reflection;

using Frontend.Core.Areas.Movimientos.Ingresos.IViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Areas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngresoCompraPorMaterialView : ContentPage
    {
        private readonly IIngresoCompraPorMaterialViewModel ingresoCompraPorMaterialViewModel;

        public IngresoCompraPorMaterialView(IIngresoCompraPorMaterialViewModel ingresoCompraPorMaterialViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IIngresoCompraPorMaterialViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.ingresoCompraPorMaterialViewModel = ingresoCompraPorMaterialViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(600);
            autocompleteCodigo.Focus();
        }
    }
}