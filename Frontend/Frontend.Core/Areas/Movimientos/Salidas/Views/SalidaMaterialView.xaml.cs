using System.Reflection;

using Frontend.Core.Areas.Movimientos.Salidas.IViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SalidaMaterialView : ContentPage
	{
        private readonly ISalidaMaterialViewModel salidaMaterialViewModel;

        public SalidaMaterialView (ISalidaMaterialViewModel salidaMaterialViewModel)
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(ISalidaMaterialViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.salidaMaterialViewModel = salidaMaterialViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(600);
            autocompleteCodigo.Focus();
        }
    }
}