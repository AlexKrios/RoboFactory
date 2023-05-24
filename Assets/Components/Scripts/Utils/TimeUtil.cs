using System;
using JetBrains.Annotations;

namespace RoboFactory.Utils
{
    [UsedImplicitly]
    public class TimeUtil
    {
        public static string DateCraftTimer(int time)
        {
            var result = TimeSpan.FromSeconds(time);
            return time switch
            {
                < 3599 => result.ToString("mm':'ss"),
                >= 3600 and < 86400 => result.ToString("hh':'mm':'ss"),
                _ => string.Empty
            };
        }

        public static int GetTime(long timeUtc)
        {
            var timeEnd = new DateTime(timeUtc);
            var currentTime = new DateTime(DateTime.Now.ToFileTimeUtc());

            if (currentTime < timeEnd)
                return (int)(timeEnd - currentTime).TotalSeconds;

            return 0;
        }
    }
}
