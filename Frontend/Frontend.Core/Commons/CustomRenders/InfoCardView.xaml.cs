
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Commons.CustomRenders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InfoCardView : ContentView
    {
        public static readonly BindableProperty MessageProperty = BindableProperty.Create<Label, string>(
          x => x.Text, string.Empty,
          defaultBindingMode: BindingMode.TwoWay,
          propertyChanged: MessagePropertyChanged
      );


        public string Message
        {
            get { return base.GetValue(MessageProperty).ToString(); }
            set { base.SetValue(MessageProperty, value); }
        }

        private static void MessagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (InfoCardView)bindable;
            control.MessageLabel.Text = newValue as string;
        }

        public InfoCardView ()
		{
			InitializeComponent ();
		}
	}
}