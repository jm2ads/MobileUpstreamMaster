using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaInventarioView : TabbedPage
    {
        private readonly IListaInventarioViewModel listaInventarioViewModel;

        public ListaInventarioView()
        {
            InitializeComponent();
            BindingContext = this.listaInventarioViewModel = ContainerManager.Resolve<IListaInventarioViewModel>();

            var cm = ContainerManager.Container;
            ListaInventarioSapView sapPage = cm.Resolve(typeof(ListaInventarioSapView), "ListaInventarioSapView", null) as ListaInventarioSapView; 
            Children.Add(sapPage);

            ListaInventarioProvisorioView provisoriosPage = cm.Resolve(typeof(ListaInventarioProvisorioView), "ListaInventarioProvisorioView", null) as ListaInventarioProvisorioView;
            Children.Add(provisoriosPage);

            ListaInventarioRechazadoView rechazadoPage = cm.Resolve(typeof(ListaInventarioRechazadoView), "ListaInventarioRechazadoView", null) as ListaInventarioRechazadoView;
            Children.Add(rechazadoPage);
        }
    }
}