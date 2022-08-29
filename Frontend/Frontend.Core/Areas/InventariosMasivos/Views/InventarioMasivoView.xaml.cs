
using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.InventariosMasivos.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventarioMasivoView : TabbedPage
    {
        private readonly IInventarioMasivoViewModel inventarioMasivoViewModel;

        public InventarioMasivoView(IInventarioMasivoViewModel inventarioMasivoViewModel)
        {
            InitializeComponent();

            BindingContext = this.inventarioMasivoViewModel = inventarioMasivoViewModel;

            InformacionInventarioMasivoView informacionInventarioMasivoView = ContainerManager.Resolve(typeof(InformacionInventarioMasivoView)) as InformacionInventarioMasivoView;
            Children.Add(informacionInventarioMasivoView);

            ListadoInventarioMasivoView listadoInventarioMasivoView = ContainerManager.Resolve(typeof(ListadoInventarioMasivoView)) as ListadoInventarioMasivoView;
            Children.Add(listadoInventarioMasivoView);
        }
    }
}