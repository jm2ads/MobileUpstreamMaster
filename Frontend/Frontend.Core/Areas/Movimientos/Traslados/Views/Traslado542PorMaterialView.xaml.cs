using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;
using System.Threading.Tasks;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Traslado542PorMaterialView : ContentPage
    {

        private readonly ITraslado542PorMaterialViewModel trasladoPorMaterialViewModel;

        public Traslado542PorMaterialView(ITraslado542PorMaterialViewModel trasladoPorMaterialViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(ITraslado542PorMaterialViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.trasladoPorMaterialViewModel = trasladoPorMaterialViewModel;
        }
        
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(600);
            autocompleteCodigo.Focus();
        }
    }
}