using Android.Content;
using Frontend.Core.Commons.CustomRenders;
using Frontend.Droid.Implementations;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ButtonWithoutShadow), typeof(AndroidButtonWithoutShadow))]
namespace Frontend.Droid.Implementations
{
    public class AndroidButtonWithoutShadow : ButtonRenderer
    {
        public AndroidButtonWithoutShadow(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Elevation = 0;
            }

        }
    }
}