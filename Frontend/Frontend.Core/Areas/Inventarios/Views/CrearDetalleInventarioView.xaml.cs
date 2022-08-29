using Frontend.Commons.Bootstrapper;
using Frontend.Core.IViewModels;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.StyleSheets;

namespace Frontend.Core.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CrearDetalleInventarioView : ContentPage
	{
        private readonly ICrearDetalleInventarioViewModel crearDetalleInventarioViewModel;

        public CrearDetalleInventarioView ()
		{
			InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(ICrearDetalleInventarioViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.crearDetalleInventarioViewModel = ContainerManager.Resolve<ICrearDetalleInventarioViewModel>();
        }

        private void ImageButton_Clicked(object sender, System.EventArgs e)
        {
            //ASOSA btnExpandCollapse
            if (lblTextoLargo.IsVisible)
            {
                lblTextoLargo.IsVisible = false;
                btnExpandCollapse.Source = "expand.png";
            }
            else
            {
                lblTextoLargo.IsVisible = true;
                btnExpandCollapse.Source = "collapse.png";
            }
        
        }
    }
}