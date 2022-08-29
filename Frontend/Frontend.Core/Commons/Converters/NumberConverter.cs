using System;
using System.Globalization;
using Xamarin.Forms;

namespace Frontend.Core.Commons.Converters
{
    public class NumberConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals(0))
            {
                return "";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}