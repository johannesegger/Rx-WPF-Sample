using System;

namespace Rx.Test
{
    public static class DateTimeOffsetExtensions
    {
        public static DateTimeOffset Add(this DateTimeOffset o, int hours, int minutes, int seconds)
        {
            return o.Add(new TimeSpan(hours, minutes, seconds));
        }
    }
}