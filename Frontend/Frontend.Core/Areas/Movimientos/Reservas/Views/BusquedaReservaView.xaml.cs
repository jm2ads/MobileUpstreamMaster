
using Frontend.Core.Areas.Movimientos.Reservas.IViewModels;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusquedaReservaView : ContentPage
    {
        private readonly IBusquedaReservaViewModel busquedaReservaViewModel;

        public BusquedaReservaView(IBusquedaReservaViewModel busquedaReservaViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IBusquedaReservaViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.busquedaReservaViewModel = busquedaReservaViewModel;
        }
    }
}