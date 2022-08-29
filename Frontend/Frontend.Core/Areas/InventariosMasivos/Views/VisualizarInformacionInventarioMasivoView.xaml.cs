using Frontend.Core.Areas.InventariosMasivos.IViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisualizarInformacionInventarioMasivoView : ContentPage
    {
        private readonly IVisualizarInformacionInventarioMasivoViewModel visualizarInformacionInventarioMasivoViewModel;
        public VisualizarInformacionInventarioMasivoView(IVisualizarInformacionInventarioMasivoViewModel visualizarInformacionInventarioMasivoViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IVisualizarInformacionInventarioMasivoViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.visualizarInformacionInventarioMasivoViewModel = visualizarInformacionInventarioMasivoViewModel;
        }
    }
}