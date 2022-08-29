using System.Reflection;

using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;
using System.Threading.Tasks;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Traslado311PorMaterialView : ContentPage
    {
        private readonly ITraslado311PorMaterialViewModel traslado311PorMaterialViewModel;

        public Traslado311PorMaterialView(ITraslado311PorMaterialViewModel traslado311PorMaterialViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(ITraslado311PorMaterialViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.traslado311PorMaterialViewModel = traslado311PorMaterialViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(600);
            autocompleteCodigo.Focus();
        }
    }
}