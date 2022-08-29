using System.Reflection;

using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;
using System.Threading.Tasks;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Traslado541PorMaterialView : ContentPage
    {
        private readonly ITraslado541PorMaterialViewModel traslado541PorMaterialViewModel;

        public Traslado541PorMaterialView(ITraslado541PorMaterialViewModel traslado541PorMaterialViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(ITraslado541PorMaterialViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.traslado541PorMaterialViewModel = traslado541PorMaterialViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(600);
            autocompleteCodigo.Focus();
        }
    }
}