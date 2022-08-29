
using Frontend.Core.Areas.Movimientos.Reservas.IViewModels;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusquedaMaterialView : ContentPage
    {
        private readonly IBusquedaMaterialViewModel busquedaMaterialViewModel;

        public BusquedaMaterialView(IBusquedaMaterialViewModel busquedaMaterialViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IBusquedaMaterialViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.busquedaMaterialViewModel = busquedaMaterialViewModel;
        }
    }
}