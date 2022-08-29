using System.Diagnostics;

namespace Frontend.Commons.Commons.Timer
{
    public static class IdleTimer
    {
        private static Stopwatch stopWatch;
        public static Stopwatch GetInstance()
        {
            if (stopWatch == null)
                stopWatch = new Stopwatch();
            return stopWatch;
        }
    }
}
