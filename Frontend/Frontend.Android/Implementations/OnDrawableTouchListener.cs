using Android.Views;
using Android.Widget;
using Frontend.Core.Commons.CustomRenders;

namespace Frontend.Droid.Implementations
{
    public class OnDrawableTouchListener : Java.Lang.Object, Android.Views.View.IOnTouchListener
    {
        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            if (v is EditText && e.Action == MotionEventActions.Up && ((AndroidPickerWithIcon)v.Parent).Element.SelectedIndex != -1)
            {
                EditText editText = (EditText)v;

                if (editText.GetCompoundDrawables()[2] != null
                    && e.GetX() >= (editText.Right - editText.GetCompoundDrawables()[2].Bounds.Width()))
                {
                    ((AndroidPickerWithIcon)editText.Parent).Element.SelectedIndex = ((PickerWithIcon)((AndroidPickerWithIcon)editText.Parent).Element).DefaultIndex;
                    return true;
                }
            }

            return false;
        }
    }
}