using Frontend.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Frontend.Core.Commons.CustomRenders
{
    public class PickerWithIcon : Picker
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(PickerWithIcon), string.Empty);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly BindableProperty DefaultIndexProperty =
            BindableProperty.Create(nameof(DefaultIndex), typeof(int), typeof(PickerWithIcon), -1);

        public int DefaultIndex
        {
            get { return (int)GetValue(DefaultIndexProperty); }
            set { SetValue(DefaultIndexProperty, value); }
        }

        public static readonly BindableProperty CustomStyleProperty =
           BindableProperty.Create(nameof(CustomStyle), typeof(int), typeof(PickerWithIcon), CustomStyleEnum.Black.GetHashCode());

        public int CustomStyle
        {
            get { return (int)GetValue(CustomStyleProperty); }
            set { SetValue(CustomStyleProperty, value); }
        }
    }
}
