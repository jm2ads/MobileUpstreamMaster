using Frontend.Core.Commons.CustomRenders;
using Frontend.iOS.Implementations;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DatePickerWithIcon), typeof(IOSDatePickerWithIcon))]
namespace Frontend.iOS.Implementations
{
    public class IOSDatePickerWithIcon : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            var element = (DatePickerWithIcon)this.Element;

            if (this.Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
            {
                var downarrow = UIImage.FromBundle(element.Image);
                Control.RightViewMode = UITextFieldViewMode.Always;
                Control.RightView = new UIImageView(downarrow);
            }
        }
    }

}