using System;
using Frontend.Commons.Bootstrapper;
using Frontend.Core.Areas.Login.IViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        private readonly ILoginViewModel vm;
        public LoginView()
        {
            InitializeComponent();
            BindingContext = vm = ContainerManager.Resolve<ILoginViewModel>();
            PassEntry.TextChanged += PassEntryOnTextChanged;
            PassEntry.Completed += PassEntry_Completed;
        }

        private void PassEntry_Completed(object sender, EventArgs e)
        {
            vm.ValidatePasswordInput();
        }

        private void PassEntryOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            vm.ValidatePasswordInput();
        }
    }
}