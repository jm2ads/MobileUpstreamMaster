using Frontend.Core.Areas.Inventarios.IViewModels;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecuentoInventarioView : ContentPage
	{
        private readonly IRecuentoInventarioViewModel recuentoInventarioViewModel;

        public RecuentoInventarioView (IRecuentoInventarioViewModel recuentoInventarioViewModel)
		{
			InitializeComponent ();
            BindingContext = this.recuentoInventarioViewModel = recuentoInventarioViewModel;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IInformacionInventarioViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            recuentoInventarioViewModel.GetAllInventarioCommand.Execute(null);
        }
    }
}