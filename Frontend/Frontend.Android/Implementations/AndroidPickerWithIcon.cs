using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Frontend.Commons.Enums;
using Frontend.Core.Commons.CustomRenders;
using Frontend.Droid.Implementations;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PickerRenderer = Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer;

[assembly: ExportRenderer(typeof(PickerWithIcon), typeof(AndroidPickerWithIcon))]
namespace Frontend.Droid.Implementations
{
    public class AndroidPickerWithIcon : PickerRenderer
    {
        PickerWithIcon element;

        public AndroidPickerWithIcon(Context context) : base(context)
        {
            
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            element = (PickerWithIcon)this.Element;

            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
            {
                if (element.CustomStyle == CustomStyleEnum.White.GetHashCode())
                {
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                        Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.White);
                    else
                        Control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.SrcAtop);
                    Control.SetHintTextColor(Android.Graphics.Color.Rgb(255, 255, 255));
                    if (element.DefaultIndex != -1)
                    {
                        element.Image = "baseline_clear_white_24";
                    }
                }
                else
                {
                    if (element.DefaultIndex != -1)
                    {
                        element.Image = "baseline_clear_black_24";
                    }
                }
                Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resources.GetIdentifier(element.Image, "drawable", this.Context.PackageName), 0);
                Control.SetOnTouchListener(new OnDrawableTouchListener());
            }

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var pickerWithIcon = sender as PickerWithIcon;
            if (e.PropertyName == "SelectedIndex" && pickerWithIcon.SelectedIndex != -1)
            {
                if (element.CustomStyle == CustomStyleEnum.White.GetHashCode())
                    Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable. baseline_clear_white_24, 0);
                else
                    Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.baseline_clear_black_24, 0);
            }
            else if (e.PropertyName == "SelectedIndex" && pickerWithIcon.SelectedIndex == pickerWithIcon.DefaultIndex)
            {
                if (element.CustomStyle == CustomStyleEnum.White.GetHashCode())
                    Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.baseline_expand_more_white_24, 0);
                else
                    Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.ic_keyboard_arrow_down_black_24dp, 0);
            }
        }
    }
}