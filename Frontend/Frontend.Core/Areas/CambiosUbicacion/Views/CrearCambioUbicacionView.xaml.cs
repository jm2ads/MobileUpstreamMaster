using Frontend.Core.Areas.CambiosUbicacion.IViewModels;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearCambioUbicacionView : ContentPage
    {
        private readonly ICrearCambioUbicacionViewModel crearCambioUbicacionViewModel;

        public CrearCambioUbicacionView(ICrearCambioUbicacionViewModel crearCambioUbicacionViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(this.GetType()).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.crearCambioUbicacionViewModel = crearCambioUbicacionViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.crearCambioUbicacionViewModel.ActualizarAlmacenes();
        }
    }
}