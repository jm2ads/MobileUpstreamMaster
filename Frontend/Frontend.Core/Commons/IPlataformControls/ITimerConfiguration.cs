using Frontend.Core.Commons.Timer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Core.Commons.IPlataformControls
{
    public interface ITimerConfiguration
    {
        TimerMode GetTimeFormat();
    }
}
