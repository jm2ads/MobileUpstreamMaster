using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleMaterialTraslado321View : ContentPage
    {
        private readonly IDetalleMaterialTraslado321ViewModel detalleMaterialTraslado321ViewModel;

        public DetalleMaterialTraslado321View(IDetalleMaterialTraslado321ViewModel detalleMaterialTraslado321ViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IDetalleMaterialTraslado321ViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.detalleMaterialTraslado321ViewModel = detalleMaterialTraslado321ViewModel;
        }
    }
}