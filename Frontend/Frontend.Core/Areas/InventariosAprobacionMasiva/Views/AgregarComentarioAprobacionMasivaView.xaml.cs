using Frontend.Core.Areas.InventariosAprobacionMasiva.IViewModels;
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
    public partial class AgregarComentarioAprobacionMasivaView : ContentPage
    {

        private readonly IAgregarComentarioAprobacionMasivaViewModel agregarComentarioAprobacionMasivaViewModel;

        public AgregarComentarioAprobacionMasivaView(IAgregarComentarioAprobacionMasivaViewModel agregarComentarioAprobacionMasivaViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IAgregarComentarioAprobacionMasivaViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.agregarComentarioAprobacionMasivaViewModel = agregarComentarioAprobacionMasivaViewModel;
        }
    }
}