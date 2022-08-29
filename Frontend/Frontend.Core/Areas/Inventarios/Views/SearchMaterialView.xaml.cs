using System.Reflection;

using Frontend.Core.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchMaterialView : ContentPage
	{
        private readonly ISearchMaterialViewModel searchMaterialViewModel;

        public SearchMaterialView(ISearchMaterialViewModel searchMaterialViewModel)
        {
            InitializeComponent();
            BindingContext = this.searchMaterialViewModel = searchMaterialViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(ISearchMaterialViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            autocompleteCodigo.Focus();
        }
    }
}