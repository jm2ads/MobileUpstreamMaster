using Frontend.Core.Areas.Inventarios.IViewModels;
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
	public partial class AgregarComentarioView : ContentPage
    {
        private readonly IAgregarComentarioViewModel agregarComentarioViewModel;

        public AgregarComentarioView(IAgregarComentarioViewModel agregarComentarioViewModel)
        {
            InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(IInformacionInventarioViewModel)).Assembly, "Frontend.Core.Assets.styles.css"));
            BindingContext = this.agregarComentarioViewModel = agregarComentarioViewModel;
        }
    }
}