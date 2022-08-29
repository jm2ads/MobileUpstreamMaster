
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Commons.CustomRenders
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SpinnerView : PopupPage
	{
		public SpinnerView (string message = null)
		{
			InitializeComponent ();
            CloseWhenBackgroundIsClicked = false;
            BackgroundColor = Color.FromRgba(0,0,0,0.8);
            BackgroundImage = null;
            Message.Text = message;
        }

	    protected override bool OnBackButtonPressed()
	    {
	        return true;
	    }
	}
}