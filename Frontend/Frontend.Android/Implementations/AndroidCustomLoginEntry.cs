using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Frontend.Core.Commons.CustomRenders;
using Frontend.Droid.Implementations;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomLoginEntry), typeof(AndroidCustomLoginEntry))]
namespace Frontend.Droid.Implementations

{
    [Obsolete]
    public class AndroidCustomLoginEntry : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.White);
            else
                Control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.SrcAtop);
        }
    }
}