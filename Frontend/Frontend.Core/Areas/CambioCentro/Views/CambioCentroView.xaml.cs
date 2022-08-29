
using Frontend.Core.IViewModels;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CambioCentroView : ContentPage
    {
        private readonly ICambioCentroViewModel cambioCentroViewModel;

        public CambioCentroView(ICambioCentroViewModel cambioCentroViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(ICambioCentroViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.cambioCentroViewModel = cambioCentroViewModel;
        }
    }
}