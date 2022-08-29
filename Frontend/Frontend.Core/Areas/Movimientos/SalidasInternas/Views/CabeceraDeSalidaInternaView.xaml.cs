using Frontend.Core.Areas.Movimientos.SalidasInternas.IViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Areas.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CabeceraDeSalidaInternaView : ContentPage
    {
        private readonly ICabeceraDeSalidaInternaViewModel cabeceraDeSalidaInternaViewModel;

        public CabeceraDeSalidaInternaView(ICabeceraDeSalidaInternaViewModel cabeceraDeSalidaInternaViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(ICabeceraDeSalidaInternaViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.cabeceraDeSalidaInternaViewModel = cabeceraDeSalidaInternaViewModel;
        }
    }
}