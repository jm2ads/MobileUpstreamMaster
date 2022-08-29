using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.Log.IViewModels;
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
    public partial class LogView : TabbedPage
    {
        private readonly ILogViewModel logViewModel;

        public LogView(ILogViewModel logViewModel)
        {
            InitializeComponent();
            BindingContext = this.logViewModel = logViewModel;

            InventariosLogView inventariosLogView = ContainerManager.Resolve(typeof(InventariosLogView)) as InventariosLogView;
            Children.Add(inventariosLogView);

            MovimientosLogView movimientosLogView = ContainerManager.Resolve(typeof(MovimientosLogView)) as MovimientosLogView;
            Children.Add(movimientosLogView);
            InitializeComponent();
        }
    }
}