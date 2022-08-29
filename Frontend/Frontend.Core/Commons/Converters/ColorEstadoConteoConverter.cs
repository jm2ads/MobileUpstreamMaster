using Frontend.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace Frontend.Core.Commons.Converters
{
    public class ColorEstadoConteoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (int)value == 0)
            {
                return Color.White;
            }

            Dictionary<EstadoConteoEnum, Color> ColorDictonary = new Dictionary<EstadoConteoEnum, Color>()
            {
                { EstadoConteoEnum.Completo, (Color)Application.Current.Resources["GreenColor"] },
                { EstadoConteoEnum.Parcial, (Color)Application.Current.Resources["YellowColor"] },
                { EstadoConteoEnum.Erroneo, (Color)Application.Current.Resources["RedColor"] }
            };
            var enumValue = (EstadoConteoEnum)value;
            return ColorDictonary[enumValue];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
