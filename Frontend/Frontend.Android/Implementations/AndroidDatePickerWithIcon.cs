using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Frontend.Core.Commons.CustomRenders;
using Frontend.Droid.Implementations;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DatePickerWithIcon), typeof(AndroidDatePickerWithIcon))]
namespace Frontend.Droid.Implementations
{
    public class AndroidDatePickerWithIcon : DatePickerRenderer
    {
        DatePickerWithIcon element;

        public AndroidDatePickerWithIcon(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            element = (DatePickerWithIcon)this.Element;

            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
            {
                Control.Foreground = AddPickerStyles(element.Image);
                Control.SetPadding(Control.PaddingLeft, Control.PaddingTop, Control.Foreground.MinimumWidth, Control.PaddingBottom);
            }

        }

        public LayerDrawable AddPickerStyles(string imagePath)
        {
            Drawable[] layers = { Control.Background, GetDrawable(imagePath) };
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

            return layerDrawable;
        }

        private BitmapDrawable GetDrawable(string imagePath)
        {
            int resID = Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            var result = new BitmapDrawable(Resources, bitmap);
            result.Gravity = Android.Views.GravityFlags.Right;

            return result;
        }
    }
}