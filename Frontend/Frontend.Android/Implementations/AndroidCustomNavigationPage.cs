using Android.Content;
using Frontend.Core.Commons.CustomRenders;
using Frontend.Droid.Implementations;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using AView = Android.Views.View;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(AndroidCustomNavigationPage))]
namespace Frontend.Droid.Implementations
{
    public class AndroidCustomNavigationPage : NavigationPageRenderer
    {
        public AndroidCustomNavigationPage(Context context) : base(context)
        {

        }
        IPageController PageController => Element as IPageController;
        CustomNavigationPage CustomNavigationPage => Element as CustomNavigationPage;

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            CustomNavigationPage.BarBackgroundColor = Color.Transparent;

            int containerHeight = b - t;

            PageController.ContainerArea = new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(containerHeight));

            for (var i = 0; i < ChildCount; i++)
            {
                AView child = GetChildAt(i);

                if (child is Android.Support.V7.Widget.Toolbar)
                {
                    continue;
                }

                child.Layout(0, 0, r, b);
            }
        }

    }
}