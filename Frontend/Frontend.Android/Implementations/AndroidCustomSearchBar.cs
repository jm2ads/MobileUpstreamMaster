using Android.Content;
using Android.Widget;
using Frontend.Core.Commons.CustomRenders;
using Frontend.Droid.Implementations;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomSearchBar), typeof(AndroidCustomSearchBar))]
namespace Frontend.Droid.Implementations
{
    public class AndroidCustomSearchBar : SearchBarRenderer
    {
        public AndroidCustomSearchBar(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var searchView = Control;
                searchView.Iconified = true;
                searchView.SetIconifiedByDefault(false);
                // (Resource.Id.search_mag_icon); is wrong / Xammie bug
                int searchIconId = Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                var icon = searchView.FindViewById(searchIconId);
                (icon as ImageView).SetImageResource(Resource.Drawable.round_search_white_24);
                int searchPlateId = searchView.Context.Resources.GetIdentifier("android:id/search_plate", null, null);
                Android.Views.View searchPlateView = searchView.FindViewById(searchPlateId);
                searchPlateView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}