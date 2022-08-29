using System.Reflection;

using Frontend.Core.Areas.Movimientos.Devoluciones.IViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DevolucionMaterialView : ContentPage
    {
        private readonly IDevolucionMaterialViewModel devolucionMaterialViewModel;

        public DevolucionMaterialView(IDevolucionMaterialViewModel devolucionMaterialViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IDevolucionMaterialViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.devolucionMaterialViewModel = devolucionMaterialViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(600);
            autocompleteCodigo.Focus();
        }
    }
}