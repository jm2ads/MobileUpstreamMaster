using System.Reflection;

using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;
using System.Threading.Tasks;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Traslado309PorMaterialView : ContentPage
    {
        private readonly ITraslado309PorMaterialViewModel traslado309PorMaterialViewModel;

        public Traslado309PorMaterialView(ITraslado309PorMaterialViewModel traslado309PorMaterialViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(ITraslado309PorMaterialViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.traslado309PorMaterialViewModel = traslado309PorMaterialViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(600);
            autocompleteCodigo.Focus();
        }
    }
}