using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.Areas.Inventarios.ViewModels;
using Frontend.Core.Commons.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AprobacionInventarioView : TabbedPage
    {
        private readonly IAprobacionInventarioViewModel aprobacionInventarioViewModel;

        public AprobacionInventarioView ()
        {
            InitializeComponent();
            BindingContext = this.aprobacionInventarioViewModel = ContainerManager.Resolve<IAprobacionInventarioViewModel>();

            var cm = ContainerManager.Container;
            AprobacionInventarioSapView sapPage = cm.Resolve(typeof(AprobacionInventarioSapView), "AprobacionInventarioSapView", null) as AprobacionInventarioSapView;
            Children.Add(sapPage);

            AprobacionInventarioProvisorioView provisoriosPage = cm.Resolve(typeof(AprobacionInventarioProvisorioView), "AprobacionInventarioProvisorioView", null) as AprobacionInventarioProvisorioView;
            Children.Add(provisoriosPage);
        }
    }
}