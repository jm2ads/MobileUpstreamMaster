//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("Frontend.Core.Areas.Movimientos.Reservas.Views.BusquedaMaterialView.xaml", "Areas/Movimientos/Reservas/Views/BusquedaMaterialView.xaml", typeof(global::Frontend.Core.Views.BusquedaMaterialView))]

namespace Frontend.Core.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Areas\\Movimientos\\Reservas\\Views\\BusquedaMaterialView.xaml")]
    public partial class BusquedaMaterialView : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Frontend.Core.Commons.CustomRenders.PickerWithIcon pickerFiltro;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Frontend.Core.Commons.CustomRenders.AutoCompleteEntry autocompleteCodigo;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Frontend.Core.Commons.CustomRenders.AutoCompleteEntry autocompleteDescripcion;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button btnCrear;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(BusquedaMaterialView));
            pickerFiltro = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Frontend.Core.Commons.CustomRenders.PickerWithIcon>(this, "pickerFiltro");
            autocompleteCodigo = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Frontend.Core.Commons.CustomRenders.AutoCompleteEntry>(this, "autocompleteCodigo");
            autocompleteDescripcion = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Frontend.Core.Commons.CustomRenders.AutoCompleteEntry>(this, "autocompleteDescripcion");
            btnCrear = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "btnCrear");
        }
    }
}
