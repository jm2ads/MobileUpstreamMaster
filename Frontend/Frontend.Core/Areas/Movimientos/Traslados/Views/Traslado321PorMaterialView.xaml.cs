using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
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
    public partial class Traslado321PorMaterialView : ContentPage
    {
        private readonly ITraslado321PorMaterialViewModel traslado321PorMaterialViewModel;

        public Traslado321PorMaterialView(ITraslado321PorMaterialViewModel traslado321PorMaterialViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(ITraslado321PorMaterialViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.traslado321PorMaterialViewModel = traslado321PorMaterialViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(600);
            autocompleteCodigo.Focus();
        }
    }
}