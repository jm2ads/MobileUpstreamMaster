using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Frontend.iOS.Implementations;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.Core.Commons.Timer;

[assembly: Xamarin.Forms.Dependency(typeof(IOSTimer))]
namespace Frontend.iOS.Implementations
{
    public class IOSTimer : ITimerConfiguration
    {

        public TimerMode GetTimeFormat()
        {
            // pendiente a cerrar...

            //NSDateFormatter* formatter = [[NSDateFormatter alloc] init];
            //[formatter setLocale:[NSLocale currentLocale]];
            //[formatter setDateStyle:NSDateFormatterNoStyle];
            //[formatter setTimeStyle:NSDateFormatterShortStyle];
            //NSString* dateString = [formatter stringFromDate:[NSDate date]];
            //NSRange amRange = [dateString rangeOfString:[formatter AMSymbol]];
            //NSRange pmRange = [dateString rangeOfString:[formatter PMSymbol]];
            //BOOL is24h = (amRange.location == NSNotFound && pmRange.location == NSNotFound);

            //string dateFormat = NSDateFormatter.GetDateFormatFromTemplate("j", 0, NSLocale.CurrentLocale);
            //bool is24HourFormat = dateFormat.IndexOf("a") == -1;

            return TimerMode.TwelveHours;
        }
    }
}