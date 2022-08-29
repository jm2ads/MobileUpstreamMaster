using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.Movimientos.Reservas.IViewModels;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservaView : TabbedPage
    {
        private readonly IReservaViewModel reservaViewModel;

        public ReservaView(IReservaViewModel reservaViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IReservaViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.reservaViewModel = reservaViewModel;
            
            BusquedaReservaView busquedaReservaView = ContainerManager.Resolve(typeof(BusquedaReservaView)) as BusquedaReservaView;
            Children.Add(busquedaReservaView);

            BusquedaMaterialView busquedaMaterialView = ContainerManager.Resolve(typeof(BusquedaMaterialView)) as BusquedaMaterialView;
            Children.Add(busquedaMaterialView);
        }
    }
}