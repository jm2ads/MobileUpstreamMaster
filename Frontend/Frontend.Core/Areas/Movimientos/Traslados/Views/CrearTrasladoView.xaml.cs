using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CrearTrasladoView: ContentPage
	{		
        private readonly ICrearTrasladoViewModel crearTrasladoViewModel;

        public CrearTrasladoView(ICrearTrasladoViewModel crearTrasladoViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(ICrearTrasladoViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.crearTrasladoViewModel = crearTrasladoViewModel;
        }

    }
}