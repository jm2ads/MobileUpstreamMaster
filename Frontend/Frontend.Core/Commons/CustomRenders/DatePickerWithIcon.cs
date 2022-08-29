using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Frontend.Core.Commons.CustomRenders
{
    public class DatePickerWithIcon : DatePicker
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(DatePickerWithIcon), string.Empty);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
    }
}
