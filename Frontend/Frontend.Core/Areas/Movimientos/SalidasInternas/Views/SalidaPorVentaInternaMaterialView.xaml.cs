using Frontend.Core.Areas.Movimientos.SalidasInternas.IViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Areas.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SalidaPorVentaInternaMaterialView : ContentPage
	{
        private readonly ISalidaPorVentaInternaMaterialViewModel salidaPorVentaInternaMaterialViewModel;

        public SalidaPorVentaInternaMaterialView(ISalidaPorVentaInternaMaterialViewModel salidaPorVentaInternaMaterialViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(SalidaPorVentaInternaMaterialView)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.salidaPorVentaInternaMaterialViewModel = salidaPorVentaInternaMaterialViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(600);
            autocompleteCodigo.Focus();
        }
    }
}